using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDevService" />
public sealed class DevService(IApiService apiService) : ServiceBase<DevService>, IDevService
{
    private const string _REPLACE_TO_EMPTY = "//~";

    private static readonly string _clientProjectPath = Path.Combine( //
        Environment.CurrentDirectory, "../../frontend/admin");

    private static readonly Regex _regex  = new(@"\.(\w)");
    private static readonly Regex _regex2 = new("([a-zA-Z]+):");
    private static readonly Regex _regex3 = new(@"`\d+");
    private static readonly Regex _regex4 = new(@"^.+?[/\\]dist[/\\]");

    /// <inheritdoc />
    public async Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        req.ThrowIfInvalid();

        // 生成 dbMaps
        await GenerateDomainEntityFileAsync(req, out var project, out var prefix).ConfigureAwait(false);

        // 生成 dto
        await GenerateDomainQueryRspFileAsync(req, project, prefix).ConfigureAwait(false);
        await GenerateDomainCreateReqFileAsync(req, project, prefix).ConfigureAwait(false);
        await GenerateDomainQueryReqFileAsync(req, project, prefix).ConfigureAwait(false);
        await GenerateDomainEditReqFileAsync(req, project, prefix).ConfigureAwait(false);
        if (req.Interfaces.Contains(nameof(IFieldEnabled))) {
            await GenerateDomainSetEnabledReqFileAsync(req, project, prefix).ConfigureAwait(false);
        }

        // 生成 app
        await GenerateAppModuleFileAsync(req, project, prefix).ConfigureAwait(false);
        await GenerateAppIServiceFileAsync(req, project, prefix).ConfigureAwait(false);
        await GenerateAppServiceFileAsync(req, project, prefix).ConfigureAwait(false);

        // 生成 cache
        await GenerateICacheFileAsync(req, project, prefix).ConfigureAwait(false);
        await GenerateCacheFileAsync(req, project, prefix).ConfigureAwait(false);

        // 生成 host
        await GenerateHostControllerFileAsync(req, project, prefix).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        req.ThrowIfInvalid();
        var tplSvg = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "assets", "icon", "tpl", "svg.vue")).ConfigureAwait(false);
        var tplExport = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "assets", "icon", "tpl", "export.js"))
                                  .ConfigureAwait(false);

        var vueContent = tplSvg.Replace("$svgCode$", req.SvgCode).Replace(_REPLACE_TO_EMPTY, string.Empty);

        var dir = Path.Combine(_clientProjectPath, "src", "assets", "icon");
        if (!Directory.Exists(dir)) {
            _ = Directory.CreateDirectory(dir);
        }

        var vueFile = Path.Combine(dir, $"{req.IconName}.vue");
        await File.WriteAllTextAsync(vueFile, vueContent).ConfigureAwait(false);

        var indexJsFile = Path.Combine(dir, "index.js");

        await File.AppendAllTextAsync(
                      indexJsFile, Environment.NewLine + tplExport.Replace("$iconName$", req.IconName).Replace(_REPLACE_TO_EMPTY, string.Empty))
                  .ConfigureAwait(false);

        // 修改icon-select.js
        var iconSelectFile    = Path.Combine(_clientProjectPath, "src", "config", "icon-select.js");
        var iconSelectContent = await File.ReadAllTextAsync(iconSelectFile).ConfigureAwait(false);
        iconSelectContent = iconSelectContent.Replace("export default", "exportDefault:").Replace("'", "\"");
        iconSelectContent = _regex2.Replace(iconSelectContent, "\"$1\":");
        iconSelectContent = "{" + iconSelectContent + "}";
        var iconExportJsInfo = iconSelectContent.ToObject<IconExportJsInfo>();
        iconExportJsInfo.ExportDefault.Icons.Last().Icons.Add($"sc-icon-{req.IconName}");
        var newContent = iconExportJsInfo.ToJson().TrimStart('{')[..^1].Replace("\"exportDefault\":", "export default");

        await File.WriteAllTextAsync(iconSelectFile, newContent).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task GenerateJsCodeAsync()
    {
        // 模板文件
        var tplOuter = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "api", "tpl", "outer.js")).ConfigureAwait(false);
        var tplInner = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "api", "tpl", "inner.js")).ConfigureAwait(false);

        foreach (var item in apiService.ReflectionList(false)) {
            var dir = Path.Combine(_clientProjectPath, "src", "api", item.Namespace);
            if (!Directory.Exists(dir)) {
                _ = Directory.CreateDirectory(dir);
            }

            var file = Path.Combine(dir, $"{item.Name.Replace(".", string.Empty)}.js");

            var content = tplOuter.Replace("$controllerDesc$", item.Summary)
                                  .Replace("$controllerPath$", item.Id)
                                  .Replace( //
                                      "$inner$",              string.Join(Environment.NewLine + Environment.NewLine, Select(item)))
                                  .Replace(_REPLACE_TO_EMPTY, string.Empty);

            await File.WriteAllTextAsync(file, content).ConfigureAwait(false);
        }

        // ReSharper disable once SeparateLocalFunctionsWithJumpStatement
        IEnumerable<string> Select(QueryApiRsp item)
        {
            return item.Children.Select(x => tplInner.Replace("$actionDesc$", x.Summary)
                                                     .Replace( //
                                                         "$actionName$",      _regex.Replace(x.Name, y => y.Groups[1].Value.ToUpperInvariant()))
                                                     .Replace("$actionPath$", x.Id)
                                                     .Replace( //
                                                         "$actionMethod$",       x.Method?.ToLowerInvariant())
                                                     .Replace(_REPLACE_TO_EMPTY, string.Empty)); //
        }
    }

    /// <inheritdoc />
    public Task<IEnumerable<Tuple<string, string>>> GetDomainProjectsAsync()
    {
        return Task.FromResult(Directory.EnumerateFiles("../", "*.Domain.csproj", SearchOption.AllDirectories)
                                        .Select(x => new Tuple<string, string>(Path.GetFileName(x), Path.GetFullPath(x))));
    }

    /// <inheritdoc />
    public IEnumerable<string> GetDotnetDataTypes(GetDotnetDataTypesReq req)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.ExportedTypes.Select(y => _regex3.Replace(y.Name, string.Empty)))
                        .Distinct()
                        .Where(x => x.StartsWith(req.StartWith, true, CultureInfo.InvariantCulture))
                        .Order()
                        .Take(Numbers.MAX_LIMIT_QUERY_PAGE_SIZE);
    }

    /// <inheritdoc />
    public IEnumerable<Tuple<string, string>> GetEntityBaseClasses()
    {
        return typeof(EntityBase<>).Assembly.GetExportedTypes()
                                   .Where(x => x.IsAbstract && x.IsClass && x.Name.EndsWith("Entity", StringComparison.InvariantCulture))
                                   .Select(x => new Tuple<string, string>(x.Name, x.FullName));
    }

    /// <inheritdoc />
    public IEnumerable<Tuple<string, string>> GetFieldInterfaces()
    {
        return typeof(IFieldEnabled).Assembly.GetExportedTypes()
                                    .Where(x => x.IsInterface && x.Name.StartsWith("IField", StringComparison.InvariantCulture))
                                    .Select(x => new Tuple<string, string>(x.Name, x.FullName));
    }

    private static async Task GenerateAppIServiceFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var templatePath = Path.Combine( //
            _regex4.Match(Environment.ProcessPath!).Value
          , "../src/backend/NetAdmin/NetAdmin.SysComponent.Application/Services/Sys/Dependency/ICodeTemplateService.cs");
        var templateContent = await File.ReadAllTextAsync(templatePath).ConfigureAwait(false);
        var appProject      = project.Replace(".Domain", ".Application");
        var dir = Path.Combine(templatePath, prefix == "Sys" ? "../../../../../NetAdmin.SysComponent.Application" : $"../../../../../../{appProject}"
                             , "Services", prefix, "Dependency");
        _ = Directory.CreateDirectory(dir);

        if (prefix != "Sys") {
            templateContent = $"""
                               using NetAdmin.Application.Services;
                               using {appProject}.Modules.{prefix};


                               """ + templateContent;
            templateContent = templateContent.Replace( //
                "NetAdmin.SysComponent.Application.Services.Sys.Dependency", $"{appProject}.Services.{prefix}.Dependency");
        }

        templateContent = templateContent.Replace("CodeTemplate", req.EntityName).Replace("代码模板", req.Summary);

        await WriteCsCodeAsync(Path.Combine(dir, $"I{req.EntityName}Service.cs"), templateContent).ConfigureAwait(false);
    }

    private static async Task GenerateAppModuleFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var templatePath = Path.Combine( //
            _regex4.Match(Environment.ProcessPath!).Value
          , "../src/backend/NetAdmin/NetAdmin.SysComponent.Application/Modules/Sys/ICodeTemplateModule.cs");
        var templateContent = await File.ReadAllTextAsync(templatePath).ConfigureAwait(false);
        var appProject      = project.Replace(".Domain", ".Application");
        var dir = Path.Combine(templatePath, prefix == "Sys" ? "../../../../NetAdmin.SysComponent.Application" : $"../../../../../{appProject}"
                             , "Modules", prefix);
        _ = Directory.CreateDirectory(dir);
        if (prefix != "Sys") {
            templateContent = """
                              using NetAdmin.Application.Modules;
                              using NetAdmin.Domain.Dto.Dependency;

                              """ + templateContent;
            templateContent = templateContent.Replace("NetAdmin.SysComponent.Application.Modules.Sys", $"{appProject}.Modules.{prefix}")
                                             .Replace("NetAdmin.Domain.Dto.Sys", $"{project}.Dto.{prefix}");
        }

        templateContent = templateContent.Replace("CodeTemplate", req.EntityName).Replace("代码模板", req.Summary);

        if (req.Interfaces.Any(x => x == nameof(IFieldEnabled))) {
            templateContent = templateContent[..^1] + $$"""

                                                        {
                                                            /// <summary>
                                                            ///     设置{{req.Summary}}启用状态
                                                            /// </summary>
                                                            Task<int> SetEnabledAsync(Set{{req.EntityName}}EnabledReq req);
                                                        }
                                                        """;
        }

        await WriteCsCodeAsync(Path.Combine(dir, $"I{req.EntityName}Module.cs"), templateContent).ConfigureAwait(false);
    }

    private static async Task GenerateAppServiceFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var templatePath = Path.Combine( //
            _regex4.Match(Environment.ProcessPath!).Value
          , "../src/backend/NetAdmin/NetAdmin.SysComponent.Application/Services/Sys/CodeTemplateService.cs");
        var templateContent = await File.ReadAllTextAsync(templatePath).ConfigureAwait(false);
        var appProject      = project.Replace(".Domain", ".Application");
        var dir = Path.Combine(templatePath, prefix == "Sys" ? "../../../../NetAdmin.SysComponent.Application" : $"../../../../../{appProject}"
                             , "Services", prefix);
        _ = Directory.CreateDirectory(dir);

        if (prefix != "Sys") {
            templateContent = $"""
                               using NetAdmin.Application.Repositories;
                               using NetAdmin.Application.Services;
                               using NetAdmin.Domain.Dto.Dependency;
                               using {appProject}.Services.{prefix}.Dependency;
                               using {project}.DbMaps.{prefix};

                               """ + templateContent;

            templateContent = templateContent.Replace("NetAdmin.SysComponent.Application.Services.Sys", $"{appProject}.Services.{prefix}")
                                             .Replace("NetAdmin.Domain.Dto.Sys", $"{project}.Dto.{prefix}");
        }

        templateContent = templateContent.Replace("CodeTemplate", req.EntityName).Replace("代码模板", req.Summary).Replace("Sys_", $"{prefix}_");

        if (req.Interfaces.Contains(nameof(IFieldEnabled))) {
            templateContent = templateContent[..^1] + $$"""

                                                            /// <inheritdoc />
                                                            public Task<int> SetEnabledAsync(Set{{req.EntityName}}EnabledReq req)
                                                            {
                                                                req.ThrowIfInvalid();
                                                                return UpdateAsync(req, [nameof(req.Enabled)]);
                                                            }
                                                        }
                                                        """;
        }

        await WriteCsCodeAsync(Path.Combine(dir, $"{req.EntityName}Service.cs"), templateContent).ConfigureAwait(false);
    }

    private static async Task GenerateCacheFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var templatePath = Path.Combine( //
            _regex4.Match(Environment.ProcessPath!).Value, "../src/backend/NetAdmin/NetAdmin.SysComponent.Cache/Sys/CodeTemplateCache.cs");
        var templateContent = await File.ReadAllTextAsync(templatePath).ConfigureAwait(false);
        var cacheProject = project.Replace(".Domain", ".Cache");
        var appProject = project.Replace(".Domain", ".Application");
        var dir = Path.Combine(templatePath, prefix == "Sys" ? "../../../NetAdmin.SysComponent.Cache" : $"../../../../{cacheProject}", prefix);
        _ = Directory.CreateDirectory(dir);

        if (prefix != "Sys") {
            templateContent = $"""
                               using NetAdmin.Cache;
                               using {appProject}.Services.{prefix}.Dependency;
                               using NetAdmin.Domain.Dto.Dependency;
                               using {cacheProject}.{prefix}.Dependency;

                               """ + templateContent;
            templateContent = templateContent.Replace("NetAdmin.SysComponent.Cache.Sys", $"{cacheProject}.{prefix}")
                                             .Replace("NetAdmin.Domain.Dto.Sys", $"{project}.Dto.{prefix}");
        }

        templateContent = templateContent.Replace("CodeTemplate", req.EntityName).Replace("代码模板", req.Summary);

        if (req.Interfaces.Contains(nameof(IFieldEnabled))) {
            templateContent = templateContent[..^1] + $$"""

                                                            /// <inheritdoc />
                                                            public Task<int> SetEnabledAsync(Set{{req.EntityName}}EnabledReq req)
                                                            {
                                                                return Service.SetEnabledAsync(req);
                                                            }
                                                        }
                                                        """;
        }

        await WriteCsCodeAsync(Path.Combine(dir, $"{req.EntityName}Cache.cs"), templateContent).ConfigureAwait(false);
    }

    private static Task GenerateDomainCreateReqFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var sb = new StringBuilder();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"using {project}.DbMaps.{prefix};");
        if (!req.Interfaces.NullOrEmpty()) {
            _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency.Fields;");
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"namespace {project}.Dto.{prefix}.{req.EntityName};");
        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"""
                                                         /// <summary>
                                                         ///     请求：创建{req.Summary}
                                                         /// </summary>
                                                         """);
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"public record Create{req.EntityName}Req : {prefix}_{req.EntityName}");
        _ = sb.Append('{');
        if (req.Interfaces?.Contains(nameof(IFieldEnabled)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldEnabled.Enabled" />
                                  public override bool Enabled { get; init; } = true;
                              """);
        }

        if (req.BaseClass != nameof(SimpleEntity) && req.FieldList.Single(x => x.IsPrimary).Type == "string") {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="EntityBase{T}.Id" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  [Required]
                                  public override string Id { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldSort)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldSort.Sort" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override long Sort { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldSummary)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldSummary.Summary" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override string Summary { get; set; }
                              """);
        }

        foreach (var field in req.FieldList) {
            var    condition      = field.IsStruct ? "Never" : "WhenWritingNull";
            var    nul            = field.IsStruct && field.IsNullable ? "?" : null;
            var    isEnum         = S<IConstantService>().GetEnums().FirstOrDefault(x => x.Key == field.Type);
            string enumConstraint = null;
            if (!isEnum.Key.NullOrEmpty()) {
                enumConstraint = $"""

                                      [EnumDataType(typeof({field.Type}))]
                                  """;
            }

            _ = sb.AppendLine(CultureInfo.InvariantCulture, $$"""

                                                                  /// <inheritdoc cref="{{prefix}}_{{req.EntityName}}.{{field.Name}}" />
                                                                  [JsonIgnore(Condition = JsonIgnoreCondition.{{condition}})]{{enumConstraint}}
                                                                  public override {{field.Type}}{{nul}} {{field.Name}} { get; init; }
                                                              """);
        }

        _ = sb.Append('}');

        var outPath = Path.Combine(Path.GetDirectoryName(req.Project)!, "Dto", prefix, req.EntityName);
        _ = Directory.CreateDirectory(outPath);
        return WriteCsCodeAsync(Path.Combine(outPath, $"Create{req.EntityName}Req.cs"), sb.ToString());
    }

    private static Task GenerateDomainEditReqFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var sb = new StringBuilder();
        if (!req.Interfaces.NullOrEmpty()) {
            _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency.Fields;");
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"namespace {project}.Dto.{prefix}.{req.EntityName};");
        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"""
                                                         /// <summary>
                                                         ///     请求：编辑{req.Summary}
                                                         /// </summary>
                                                         """);
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"public sealed record Edit{req.EntityName}Req : Create{req.EntityName}Req");
        _ = sb.Append('{');

        if (req.BaseClass                                   == nameof(VersionEntity) || req.BaseClass == nameof(LiteVersionEntity) ||
            req.Interfaces?.Contains(nameof(IFieldVersion)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldVersion.Version" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override long Version { get; init; }
                              """);
        }

        _ = sb.Append('}');

        var outPath = Path.Combine(Path.GetDirectoryName(req.Project)!, "Dto", prefix, req.EntityName);
        _ = Directory.CreateDirectory(outPath);
        return WriteCsCodeAsync(Path.Combine(outPath, $"Edit{req.EntityName}Req.cs"), sb.ToString());
    }

    private static Task GenerateDomainEntityFileAsync(GenerateCsCodeReq req, out string project, out string prefix)
    {
        project = $"{Path.GetFileNameWithoutExtension(req.Project)}";
        prefix  = project == "NetAdmin.Domain" ? "Sys" : project[(project.IndexOf('.') + 1)..(project.IndexOf('.') + 4)];
        var ns        = project + $".DbMaps.{prefix}";
        var className = $"{prefix}_{req.EntityName}";
        var sb        = new StringBuilder();

        _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency;");
        _ = sb.AppendLine("using NetAdmin.Domain.Attributes;");
        if (!req.Interfaces.NullOrEmpty()) {
            _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency.Fields;");
            if (req.Interfaces.Contains(nameof(IFieldOwner))) {
                _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Sys;");
            }
        }

        _ = sb.AppendLine();
        _ = sb.Append(CultureInfo.InvariantCulture, $"namespace {ns};");
        _ = sb.AppendLine();
        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"""
                                                         /// <summary>
                                                         ///     {req.Summary}表
                                                         /// </summary>
                                                         """);
        _ = sb.Append( //
            CultureInfo.InvariantCulture, $"[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof({className}))]");

        _ = sb.AppendLine();
        _ = sb.Append(CultureInfo.InvariantCulture, $"public record {className} : {req.BaseClass}");
        var idType = req.FieldList.Single(x => x.IsPrimary).Type;
        if (!idType.Equals("long", StringComparison.OrdinalIgnoreCase) && !idType.Equals("Int64", StringComparison.OrdinalIgnoreCase)) {
            _ = sb.Append(CultureInfo.InvariantCulture, $"<{idType}>");
        }

        if (!req.Interfaces.NullOrEmpty()) {
            _ = sb.Append(", ");
            _ = sb.Append(req.Interfaces.Join(", "));
        }

        _ = sb.AppendLine();
        _ = sb.Append('{');

        if (req.Interfaces?.Contains(nameof(IFieldEnabled)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     是否启用
                                  /// </summary>
                                  /// <example>true</example>
                                  [Column]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual bool Enabled { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldCreatedTime)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     创建时间
                                  /// </summary>
                                  /// <example>2025-07-03 17:56:44</example>
                                  [Column(ServerTime = DateTimeKind.Local, CanUpdate = false, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual DateTime CreatedTime { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldModifiedTime)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     修改时间
                                  /// </summary>
                                  /// <example>2025-07-03 17:56:44</example>
                                  [Column(ServerTime = DateTimeKind.Local, CanInsert = false, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual DateTime? ModifiedTime { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldCreatedClientIp)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     创建者客户端IP
                                  /// </summary>
                                  /// <example>127.0.0.1</example>
                                  [Column]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual int? CreatedClientIp { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldModifiedClientIp)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     修改者客户端IP
                                  /// </summary>
                                  /// <example>127.0.0.1</example>
                                  [Column]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual int? ModifiedClientIp { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldCreatedClientUserAgent)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     创建者客户端用户代理
                                  /// </summary>
                                 /// <example>Mozilla/5.0</example>
                                  [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_1022)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual string CreatedUserAgent { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldModifiedClientUserAgent)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     修改者客户端用户代理
                                  /// </summary>
                                  /// <example>Mozilla/5.0</example>
                                  [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_1022)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual string ModifiedUserAgent { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldCreatedUser)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     创建者编号
                                  /// </summary>
                                 /// <example>370942943322181</example>
                                  [Column(CanUpdate = false, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual long? CreatedUserId { get; init; }

                                  /// <summary>
                                  ///     创建者用户名
                                  /// </summary>
                                  /// <example>root</example>
                                  [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual string CreatedUserName { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldModifiedUser)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     修改者编号
                                  /// </summary>
                                 /// <example>370942943322181</example>
                                  [Column(CanUpdate = false, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual long? ModifiedUserId { get; init; }

                                  /// <summary>
                                  ///     修改者用户名
                                  /// </summary>
                                  /// <example>root</example>
                                  [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31, CanUpdate = false, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual string ModifiedUserName { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldOwner)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     归属用户
                                  /// </summary>
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  [Navigate(nameof(OwnerId))]
                                  public Sys_User Owner { get; init; }

                                  /// <summary>
                                  ///     归属部门编号
                                  /// </summary>
                                  /// <example>370942943322181</example>
                                  [Column]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual long? OwnerDeptId { get; init; }

                                  /// <summary>
                                  ///     归属用户编号
                                  /// </summary>
                                  /// <example>370942943322181</example>
                                  [Column]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual long? OwnerId { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldSort)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     排序值，越大越前
                                  /// </summary>
                                  /// <example>100</example>
                                  [Column]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual long Sort { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldSummary)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     备注
                                  /// </summary>
                                  /// <example>备注文字</example>
                                  [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual string Summary { get; set; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldVersion)) == true) {
            _ = sb.AppendLine("""

                                  /// <summary>
                                  ///     数据版本
                                  /// </summary>
                                  /// <example>100</example>
                                  [Column(IsVersion = true, Position = -1)]
                                  [CsvIgnore]
                                  [JsonIgnore]
                                  public virtual long Version { get; init; }
                              """);
        }

        foreach (var field in req.FieldList) {
            var    column    = new StringBuilder();
            string snowflake = null;
            if (!field.DbType.NullOrEmpty()) {
                _ = column.Append(",DbType = Chars." + field.DbType);
            }

            if (field.IsPrimary) {
                _ = column.Append(",IsIdentity = false, IsPrimary = true, Position = 1");
                if (field.DbType.NullOrEmpty()) {
                    snowflake = """

                                    [Snowflake]
                                """;
                }
            }

            if (column.Length > 0) {
                _ = column[0] = '(';
                _ = column.Append(')');
            }

            var decoration = field.IsPrimary ? "override" : "virtual";
            var nul        = field.IsStruct && field.IsNullable ? "?" : string.Empty;
            _ = sb.AppendLine(CultureInfo.InvariantCulture, $$"""

                                                                  /// <summary>
                                                                  ///     {{field.Summary}}
                                                                  /// </summary>
                                                                  /// <example>{{field.Example}}</example>
                                                                  [Column{{column}}]
                                                                  [CsvIgnore]
                                                                  [JsonIgnore]{{snowflake}}
                                                                  public {{decoration}} {{field.Type}}{{nul}} {{field.Name}} { get; init; }
                                                              """);
        }

        _ = sb.Append('}');

        var outPath = Path.Combine(Path.GetDirectoryName(req.Project)!, "DbMaps", prefix);
        _ = Directory.CreateDirectory(outPath);
        return WriteCsCodeAsync(Path.Combine(outPath, $"{className}.cs"), sb.ToString());
    }

    private static Task GenerateDomainQueryReqFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var sb = new StringBuilder();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"using {project}.DbMaps.{prefix};");
        _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency;");

        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"namespace {project}.Dto.{prefix}.{req.EntityName};");
        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"""
                                                         /// <summary>
                                                         ///     请求：查询{req.Summary}
                                                         /// </summary>
                                                         """);
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"public sealed record Query{req.EntityName}Req : {prefix}_{req.EntityName}");
        _ = sb.Append('{');

        if (req.BaseClass != nameof(SimpleEntity)) {
            var id        = req.FieldList.Single(x => x.IsPrimary);
            var condition = id.IsStruct ? "Never" : "WhenWritingNull";
            _ = sb.AppendLine(CultureInfo.InvariantCulture, $$"""

                                                                  /// <inheritdoc cref="EntityBase{T}.Id" />
                                                                  [JsonIgnore(Condition = JsonIgnoreCondition.{{condition}})]
                                                                  public override {{id.Type}} Id { get; init; }
                                                              """);
        }

        _ = sb.Append('}');

        var outPath = Path.Combine(Path.GetDirectoryName(req.Project)!, "Dto", prefix, req.EntityName);
        _ = Directory.CreateDirectory(outPath);
        return WriteCsCodeAsync(Path.Combine(outPath, $"Query{req.EntityName}Req.cs"), sb.ToString());
    }

    private static Task GenerateDomainQueryRspFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var isSealed = " sealed ";
        var sb       = new StringBuilder();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"using {project}.DbMaps.{prefix};");
        _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency;");
        if (!req.Interfaces.NullOrEmpty()) {
            _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency.Fields;");
            if (req.Interfaces.Contains(nameof(IFieldOwner))) {
                _        = sb.AppendLine("using NetAdmin.Domain.Dto.Sys.User;");
                isSealed = " ";
            }
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"namespace {project}.Dto.{prefix}.{req.EntityName};");
        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"""
                                                         /// <summary>
                                                         ///     响应：查询{req.Summary}
                                                         /// </summary>
                                                         """);
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"public{isSealed}record Query{req.EntityName}Rsp : {prefix}_{req.EntityName}");
        _ = sb.Append('{');
        if (req.Interfaces?.Contains(nameof(IFieldEnabled)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldEnabled.Enabled" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override bool Enabled { get; init; }
                              """);
        }

        if (req.BaseClass != nameof(SimpleEntity)) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override DateTime CreatedTime { get; init; }
                              """);
        }

        if ((req.BaseClass != nameof(SimpleEntity) && req.BaseClass != nameof(ImmutableEntity) && req.BaseClass != nameof(LiteImmutableEntity)) ||
            req.Interfaces?.Contains(nameof(IFieldModifiedTime)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override DateTime? ModifiedTime { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldCreatedClientIp)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldCreatedClientIp.CreatedClientIp" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override int? CreatedClientIp { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldModifiedClientIp)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldModifiedClientIp.ModifiedClientIp" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override int? ModifiedClientIp { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldCreatedClientUserAgent)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldCreatedClientUserAgent.CreatedUserAgent" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override string CreatedUserAgent { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldModifiedClientUserAgent)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldModifiedClientUserAgent}.ModifiedUserAgent" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override string ModifiedUserAgent { get; init; }
                              """);
        }

        if (new[] { nameof(VersionEntity), nameof(MutableEntity), nameof(ImmutableEntity) }.Contains(req.BaseClass) ||
            req.Interfaces?.Contains(nameof(IFieldCreatedUser)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldCreatedUser.CreatedUserId" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override long? CreatedUserId { get; init; }

                                  /// <inheritdoc cref="IFieldCreatedUser.CreatedUserName" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override string CreatedUserName { get; init; }
                              """);
        }

        if (new[] { nameof(VersionEntity), nameof(MutableEntity) }.Contains(req.BaseClass) ||
            req.Interfaces?.Contains(nameof(IFieldModifiedUser)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserId" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override long? ModifiedUserId { get; init; }

                                  /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserName" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override string ModifiedUserName { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldOwner)) == true) {
            _ = sb.AppendLine(CultureInfo.InvariantCulture, $$"""

                                                                  /// <inheritdoc cref="{{prefix}}_{{req.EntityName}}.Owner" />
                                                                  public new virtual QueryUserRsp Owner { get; init; }

                                                                  /// <inheritdoc cref="IFieldOwner.OwnerDeptId" />
                                                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                                                  public override long? OwnerDeptId { get; init; }

                                                                  /// <inheritdoc cref="IFieldOwner.OwnerId" />
                                                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                                                  public override long? OwnerId { get; init; }
                                                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldSort)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldSort.Sort" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override long Sort { get; init; }
                              """);
        }

        if (req.Interfaces?.Contains(nameof(IFieldSummary)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldSummary.Summary" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
                                  public override string Summary { get; set; }
                              """);
        }

        if (req.BaseClass                                   == nameof(VersionEntity) || req.BaseClass == nameof(LiteVersionEntity) ||
            req.Interfaces?.Contains(nameof(IFieldVersion)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldVersion.Version" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override long Version { get; init; }
                              """);
        }

        foreach (var field in req.FieldList) {
            var nul       = field.IsStruct && field.IsNullable ? "?" : string.Empty;
            var condition = field.IsStruct ? "Never" : "WhenWritingNull";
            _ = sb.AppendLine(CultureInfo.InvariantCulture, $$"""

                                                                  /// <inheritdoc cref="{{prefix}}_{{req.EntityName}}.{{field.Name}}" />
                                                                  [JsonIgnore(Condition = JsonIgnoreCondition.{{condition}})]
                                                                  public override {{field.Type}}{{nul}} {{field.Name}} { get; init; }
                                                              """);
        }

        _ = sb.Append('}');

        var outPath = Path.Combine(Path.GetDirectoryName(req.Project)!, "Dto", prefix, req.EntityName);
        _ = Directory.CreateDirectory(outPath);
        return WriteCsCodeAsync(Path.Combine(outPath, $"Query{req.EntityName}Rsp.cs"), sb.ToString());
    }

    private static Task GenerateDomainSetEnabledReqFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var sb = new StringBuilder();
        if (!req.Interfaces.NullOrEmpty()) {
            _ = sb.AppendLine("using NetAdmin.Domain.DbMaps.Dependency.Fields;");
        }

        if (prefix != "Sys") {
            _ = sb.AppendLine(CultureInfo.InvariantCulture, $"using {project}.DbMaps.{prefix};");
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"namespace {project}.Dto.{prefix}.{req.EntityName};");
        _ = sb.AppendLine();
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"""
                                                         /// <summary>
                                                         ///     请求：设置{req.Summary}启用状态
                                                         /// </summary>
                                                         """);
        _ = sb.AppendLine(CultureInfo.InvariantCulture, $"public sealed record Set{req.EntityName}EnabledReq : {prefix}_{req.EntityName}");
        _ = sb.Append('{');

        _ = sb.AppendLine(CultureInfo.InvariantCulture, $$"""

                                                              /// <inheritdoc cref="IFieldEnabled.Enabled" />
                                                              [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                                              public override bool Enabled { get; init; }

                                                              /// <inheritdoc cref="{{prefix}}_{{req.EntityName}}.Id" />
                                                              [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                                              public override long Id { get; init; }
                                                          """);

        if (req.BaseClass                                   == nameof(VersionEntity) || req.BaseClass == nameof(LiteVersionEntity) ||
            req.Interfaces?.Contains(nameof(IFieldVersion)) == true) {
            _ = sb.AppendLine("""

                                  /// <inheritdoc cref="IFieldVersion.Version" />
                                  [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
                                  public override long Version { get; init; }
                              """);
        }

        _ = sb.Append('}');

        var outPath = Path.Combine(Path.GetDirectoryName(req.Project)!, "Dto", prefix, req.EntityName);
        _ = Directory.CreateDirectory(outPath);
        return WriteCsCodeAsync(Path.Combine(outPath, $"Set{req.EntityName}EnabledReq.cs"), sb.ToString());
    }

    private static async Task GenerateHostControllerFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var templatePath = Path.Combine( //
            _regex4.Match(Environment.ProcessPath!).Value
          , "../src/backend/NetAdmin/NetAdmin.SysComponent.Host/Controllers/Sys/CodeTemplateController.cs");
        var templateContent = await File.ReadAllTextAsync(templatePath).ConfigureAwait(false);
        var hostProject     = project.Replace(".Domain", ".Host");
        var cacheProject    = project.Replace(".Domain", ".Cache");
        var appProject      = project.Replace(".Domain", ".Application");
        var dir = Path.Combine(
            templatePath, prefix == "Sys" ? "../../../../NetAdmin.SysComponent.Host/Controllers" : $"../../../../../{hostProject}/Controllers"
          , prefix);
        _ = Directory.CreateDirectory(dir);

        if (prefix != "Sys") {
            templateContent = $"""
                               using {appProject}.Services.{prefix}.Dependency;
                               using NetAdmin.Domain.Dto.Dependency;
                               using NetAdmin.Host.Attributes;
                               using NetAdmin.Host.Controllers;
                               using {appProject}.Modules.{prefix};
                               using {cacheProject}.{prefix}.Dependency;

                               """ + templateContent;
            templateContent = templateContent.Replace("NetAdmin.SysComponent.Host.Controllers.Sys", $"{hostProject}.Controllers.{prefix}")
                                             .Replace("NetAdmin.Domain.Dto.Sys", $"{project}.Dto.{prefix}")
                                             .Replace("nameof(Sys)",             $"nameof({prefix})");
        }

        templateContent = templateContent.Replace("CodeTemplate", req.EntityName).Replace("代码模板", req.Summary);
        if (req.Interfaces.Contains(nameof(IFieldEnabled))) {
            templateContent = templateContent[..^1] + $$"""

                                                            /// <summary>
                                                            ///     设置{{req.Summary}}启用状态
                                                            /// </summary>
                                                            public Task<int> SetEnabledAsync(Set{{req.EntityName}}EnabledReq req)
                                                            {
                                                                return Cache.SetEnabledAsync(req);
                                                            }
                                                        }
                                                        """;
        }

        await WriteCsCodeAsync(Path.Combine(dir, $"{req.EntityName}Controller.cs"), templateContent).ConfigureAwait(false);
    }

    private static async Task GenerateICacheFileAsync(GenerateCsCodeReq req, string project, string prefix)
    {
        var templatePath = Path.Combine( //
            _regex4.Match(Environment.ProcessPath!).Value
          , "../src/backend/NetAdmin/NetAdmin.SysComponent.Cache/Sys/Dependency/ICodeTemplateCache.cs");
        var templateContent = await File.ReadAllTextAsync(templatePath).ConfigureAwait(false);
        var cacheProject    = project.Replace(".Domain", ".Cache");
        var appProject      = project.Replace(".Domain", ".Application");
        var dir = Path.Combine(templatePath, prefix == "Sys" ? "../../../../NetAdmin.SysComponent.Cache" : $"../../../../../{cacheProject}", prefix
                             , "Dependency");
        _ = Directory.CreateDirectory(dir);

        if (prefix != "Sys") {
            templateContent = $"""
                               using NetAdmin.Cache;
                               using {appProject}.Modules.{prefix};
                               using {appProject}.Services.{prefix}.Dependency;


                               """ + templateContent;
            templateContent = templateContent.Replace( //
                "NetAdmin.SysComponent.Cache.Sys.Dependency", $"{cacheProject}.{prefix}.Dependency");
        }

        templateContent = templateContent.Replace("CodeTemplate", req.EntityName).Replace("代码模板", req.Summary);

        await WriteCsCodeAsync(Path.Combine(dir, $"I{req.EntityName}Cache.cs"), templateContent).ConfigureAwait(false);
    }

    private static Task WriteCsCodeAsync(string path, string content)
    {
        content = content.Replace("\r", string.Empty);
        var sb     = new StringBuilder();
        var usings = new List<string>();

        foreach (var line in content.Split('\n')) {
            if (line.StartsWith("using ", StringComparison.Ordinal)) {
                usings.Add(line);
            }
            else {
                _ = sb.AppendLine();
                _ = sb.Append(line);
            }
        }

        usings.Sort();

        content = string.Join('\n', usings) + sb;
        return File.WriteAllTextAsync(path, usings.Count == 0 ? content.Trim() : content);
    }
}