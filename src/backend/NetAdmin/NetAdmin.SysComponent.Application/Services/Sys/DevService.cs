using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDevService" />
public sealed class DevService(IApiService apiService) : ServiceBase<DevService>, IDevService
{
    private const string _REPLACE_TO_EMPTY = "//~";

    private static readonly string _clientProjectPath = Path.Combine( //
        Environment.CurrentDirectory, "../../frontend/admin");

    private static readonly string[] _projectDirs = Directory.GetDirectories(Path.Combine(Environment.CurrentDirectory, "../"))
                                                             .Concat(Directory.GetDirectories(
                                                                         Path.Combine(Environment.CurrentDirectory, "../NetAdmin")))
                                                             .ToArray();

    private static readonly Regex _regex  = new(@"\.(\w)");
    private static readonly Regex _regex2 = new("([a-zA-Z]+):");

    /// <inheritdoc />
    public async Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        req.ThrowIfInvalid();
        int index;
        var typeAbbr = req.Type[(index = req.Type.LastIndexOf('.') + 1)..(index + 3)];

        // 模板层目录
        var tplHostDir  = GetDir("NetAdmin.Host");
        var tplCacheDir = GetDir("NetAdmin.Cache");
        var tplDataDir  = GetDir("NetAdmin.Domain");
        var tplAppDir   = GetDir("NetAdmin.Application");

        // 主机层目录
        var hostControllerDir = Path.Combine(GetDir($"{req.Type}.Host"), "Controllers", typeAbbr);

        // 缓存层目录
        var cacheDir           = Path.Combine(GetDir($"{req.Type}.Cache"), typeAbbr);
        var cacheDependencyDir = Path.Combine(cacheDir,                    "Dependency");

        // 业务逻辑层目录
        var appDir                   = GetDir($"{req.Type}.Application");
        var appModulesDir            = Path.Combine(appDir,         "Modules",  typeAbbr);
        var appServicesDir           = Path.Combine(appDir,         "Services", typeAbbr);
        var appServicesDependencyDir = Path.Combine(appServicesDir, "Dependency");

        // 数据契约层目录
        string dataDir;
        try {
            dataDir = GetDir($"{req.Type}.Domain");
        }
        catch (InvalidOperationException) {
            dataDir = tplDataDir;
        }

        var dtoDir    = Path.Combine(dataDir, "Dto",    typeAbbr, req.ModuleName);
        var entityDir = Path.Combine(dataDir, "DbMaps", typeAbbr);

        // 创建缺少的目录
        CreateDir(hostControllerDir, cacheDir, cacheDependencyDir, appDir, appModulesDir, appServicesDir, appServicesDependencyDir, dataDir, dtoDir
,                                    entityDir);

        // Controller
        await WriteCodeFileAsync(req, Path.Combine(tplHostDir,        "Controllers", "Tpl", "ExampleController.cs")
                          ,           Path.Combine(hostControllerDir, $"{req.ModuleName}Controller.cs"), typeAbbr)
            .ConfigureAwait(false);

        // CreateReq
        await WriteCodeFileAsync(req, Path.Combine(tplDataDir, "Dto", "Tpl", "Example", "CreateExampleReq.cs")
                          ,           Path.Combine(dtoDir,     $"Create{req.ModuleName}Req.cs"), typeAbbr)
            .ConfigureAwait(false);

        // EditReq
        await WriteCodeFileAsync(req, Path.Combine(tplDataDir, "Dto", "Tpl", "Example", "EditExampleReq.cs")
                          ,           Path.Combine(dtoDir,     $"Edit{req.ModuleName}Req.cs"), typeAbbr)
            .ConfigureAwait(false);

        // QueryReq
        await WriteCodeFileAsync(req, Path.Combine(tplDataDir, "Dto", "Tpl", "Example", "QueryExampleReq.cs")
                          ,           Path.Combine(dtoDir,     $"Query{req.ModuleName}Req.cs"), typeAbbr)
            .ConfigureAwait(false);

        // QueryRsp
        await WriteCodeFileAsync(req, Path.Combine(tplDataDir, "Dto", "Tpl", "Example", "QueryExampleRsp.cs")
                          ,           Path.Combine(dtoDir,     $"Query{req.ModuleName}Rsp.cs"), typeAbbr)
            .ConfigureAwait(false);

        // ICache
        await WriteCodeFileAsync(req, Path.Combine(tplCacheDir,        "Tpl", "Dependency", "IExampleCache.cs")
                          ,           Path.Combine(cacheDependencyDir, $"I{req.ModuleName}Cache.cs"), typeAbbr)
            .ConfigureAwait(false);

        // Cache
        await WriteCodeFileAsync(req, Path.Combine(tplCacheDir, "Tpl", "ExampleCache.cs"), Path.Combine(cacheDir, $"{req.ModuleName}Cache.cs")
                          ,           typeAbbr)
            .ConfigureAwait(false);

        // IModule
        await WriteCodeFileAsync(req, Path.Combine(tplAppDir,     "Modules", "Tpl", "IExampleModule.cs")
                          ,           Path.Combine(appModulesDir, $"I{req.ModuleName}Module.cs"), typeAbbr)
            .ConfigureAwait(false);

        // IService
        await WriteCodeFileAsync(req, Path.Combine(tplAppDir,                "Services", "Tpl", "Dependency", "IExampleService.cs")
                          ,           Path.Combine(appServicesDependencyDir, $"I{req.ModuleName}Service.cs"), typeAbbr)
            .ConfigureAwait(false);

        // Service
        await WriteCodeFileAsync(req, Path.Combine(tplAppDir,      "Services", "Tpl", "ExampleService.cs")
                          ,           Path.Combine(appServicesDir, $"{req.ModuleName}Service.cs"), typeAbbr)
            .ConfigureAwait(false);

        // Entity
        await WriteCodeFileAsync(req, Path.Combine(tplDataDir, "DbMaps", "Tpl", "Tpl_Example.cs")
                          ,           Path.Combine(entityDir,  $"{typeAbbr}_{req.ModuleName}.cs"), typeAbbr)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        req.ThrowIfInvalid();
        var tplSvg = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "assets", "icons", "tpl", "Svg.vue")).ConfigureAwait(false);
        var tplExport = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "assets", "icons", "tpl", "export.js"))
                                  .ConfigureAwait(false);

        var vueContent = tplSvg.Replace("$svgCode$", req.SvgCode).Replace(_REPLACE_TO_EMPTY, string.Empty);

        var dir = Path.Combine(_clientProjectPath, "src", "assets", "icons");
        if (!Directory.Exists(dir)) {
            _ = Directory.CreateDirectory(dir);
        }

        var vueFile = Path.Combine(dir, $"{req.IconName}.vue");
        await File.WriteAllTextAsync(vueFile, vueContent).ConfigureAwait(false);

        var indexJsFile = Path.Combine(dir, "index.js");

        await File.AppendAllTextAsync(
                      indexJsFile, Environment.NewLine + tplExport.Replace("$iconName$", req.IconName).Replace(_REPLACE_TO_EMPTY, string.Empty))
                  .ConfigureAwait(false);

        // 修改iconSelect.js
        var iconSelectFile    = Path.Combine(_clientProjectPath, "src", "config", "iconSelect.js");
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

    private static void CreateDir(params string[] dirs)
    {
        foreach (var dir in dirs) {
            if (!Directory.Exists(dir)) {
                _ = Directory.CreateDirectory(dir!);
            }
        }
    }

    private static string GetDir(string key)
    {
        return _projectDirs.First(x => x.EndsWith(key, true, CultureInfo.InvariantCulture));
    }

    private static async Task WriteCodeFileAsync(GenerateCsCodeReq req, string tplFile, string writeFile, string moduleAbbr)
    {
        var tplContent = await File.ReadAllTextAsync(tplFile).ConfigureAwait(false);
        tplContent = tplContent.Replace("Tpl", moduleAbbr).Replace("示例", req.ModuleRemark).Replace("Example", req.ModuleName);

        await File.WriteAllTextAsync(writeFile, tplContent).ConfigureAwait(false);
    }
}