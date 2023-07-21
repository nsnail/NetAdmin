using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Dev;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDevService" />
public sealed class DevService : ServiceBase<DevService>, IDevService
{
    private const string _REPLACE_TO_EMPTY = "//~";

    private static readonly string _clientProjectPath = Path.Combine( //
        Environment.CurrentDirectory, "../../frontend/admin");

    private readonly IApiService _apiService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DevService" /> class.
    /// </summary>
    public DevService(IApiService apiService)
    {
        _apiService = apiService;
    }

    /// <summary>
    ///     生成后端代码
    /// </summary>
    public async Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        var projectDirs = Directory.GetDirectories(Path.Combine(Environment.CurrentDirectory, "../"));

        // 模块类型（Sys、Biz、等）
        var moduleType = Enum.GetName(req.Type)!;

        // 模板层目录
        var tplAppDir = projectDirs.First(x => x.EndsWith($"{nameof(SysComponent)}.{nameof(Application)}", true
                                                        , CultureInfo.InvariantCulture));
        var tplHostDir
            = projectDirs.First(x => x.EndsWith($"{nameof(SysComponent)}.{nameof(Host)}", true
                                              , CultureInfo.InvariantCulture));

        // 业务逻辑层目录
        var appDir = projectDirs.First(x => x.EndsWith($"{moduleType}.{nameof(Application)}", true
                                                     , CultureInfo.InvariantCulture));

        // 数据契约层目录
        var dataDir = projectDirs.First(x => x.EndsWith($"{nameof(NetAdmin)}.{nameof(Domain)}", true
                                                      , CultureInfo.InvariantCulture));

        // Api接口层目录
        var hostDir = projectDirs.First(x => x.EndsWith($"{moduleType}.Host", true, CultureInfo.InvariantCulture));

        // 业务逻辑层 - 模块目录
        var modulesDir = Path.Combine(appDir, nameof(Modules), moduleType[..3]);
        if (!Directory.Exists(modulesDir)) {
            _ = Directory.CreateDirectory(modulesDir);
        }

        // 业务逻辑层 - 服务目录
        var servicesDir           = Path.Combine(appDir,      nameof(Services), moduleType[..3]);
        var servicesDependencyDir = Path.Combine(servicesDir, nameof(Dependency));
        if (!Directory.Exists(servicesDependencyDir)) {
            _ = Directory.CreateDirectory(servicesDependencyDir);
        }

        // 数据契约层 - DTO目录
        var dtoDir = Path.Combine(dataDir, nameof(Domain.Dto), moduleType[..3], req.ModuleName);
        if (!Directory.Exists(dtoDir)) {
            _ = Directory.CreateDirectory(dtoDir);
        }

        // 数据契约层 - Entity目录
        var entityDir = Path.Combine(dataDir, nameof(Domain.DbMaps), moduleType[..3]);
        if (!Directory.Exists(entityDir)) {
            _ = Directory.CreateDirectory(entityDir);
        }

        // Api接口层 - Controller目录
        var controllerDir = Path.Combine(hostDir, "Controllers", moduleType[..3]);
        if (!Directory.Exists(controllerDir)) {
            _ = Directory.CreateDirectory(controllerDir);
        }

        // iModule
        await WriteCodeFileAsync(req, Path.Combine(tplAppDir,  nameof(Modules), nameof(Tpl), "IExampleModule.cs")
                          ,           Path.Combine(modulesDir, $"I{req.ModuleName}Module.cs"));

        // iService
        await WriteCodeFileAsync(
            req, Path.Combine(tplAppDir, nameof(Services), nameof(Tpl), nameof(Dependency), "IExampleService.cs")
     ,           Path.Combine(servicesDependencyDir, $"I{req.ModuleName}Service.cs"));

        // service
        await WriteCodeFileAsync(req, Path.Combine(tplAppDir,   nameof(Services), nameof(Tpl), "ExampleService.cs")
                          ,           Path.Combine(servicesDir, $"{req.ModuleName}Service.cs"));

        // controller
        await WriteCodeFileAsync(req, Path.Combine(tplHostDir,    "Controllers", nameof(Tpl), "ExampleController.cs")
                          ,           Path.Combine(controllerDir, $"{req.ModuleName}Controller.cs"));

        // createReq
        await WriteCodeFileAsync(
            req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "CreateExampleReq.cs")
     ,           Path.Combine(dtoDir,  $"Create{req.ModuleName}Req.cs"));

        // updateReq
        await WriteCodeFileAsync(
            req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "UpdateExampleReq.cs")
     ,           Path.Combine(dtoDir,  $"Update{req.ModuleName}Req.cs"));

        // queryReq
        await WriteCodeFileAsync(
            req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "QueryExampleReq.cs")
     ,           Path.Combine(dtoDir,  $"Query{req.ModuleName}Req.cs"));

        // queryRsp
        await WriteCodeFileAsync(
            req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "QueryExampleRsp.cs")
     ,           Path.Combine(dtoDir,  $"Query{req.ModuleName}Rsp.cs"));

        // entity
        await WriteCodeFileAsync(req, Path.Combine(dataDir,   nameof(Domain.DbMaps), nameof(Tpl), "Tpl_Example.cs")
                          ,           Path.Combine(entityDir, $"{moduleType[..3]}_{req.ModuleName}.cs"));
    }

    /// <summary>
    ///     生成图标代码
    /// </summary>
    public async Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        var tplSvg = await File.ReadAllTextAsync(
            Path.Combine(_clientProjectPath, "src", "assets", "icons", "tpl", "Svg.vue"));
        var tplExport
            = await File.ReadAllTextAsync(
                Path.Combine(_clientProjectPath, "src", "assets", "icons", "tpl", "export.js"));

        var vueContent = tplSvg.Replace("$svgCode$", req.SvgCode).Replace(_REPLACE_TO_EMPTY, string.Empty);

        var dir = Path.Combine(_clientProjectPath, "src", "assets", "icons");
        if (!Directory.Exists(dir)) {
            _ = Directory.CreateDirectory(dir);
        }

        var vueFile = Path.Combine(dir, $"{req.IconName}.vue");
        await File.WriteAllTextAsync(vueFile, vueContent);

        var indexJsFile = Path.Combine(dir, "index.js");

        await File.AppendAllTextAsync(
            indexJsFile
          , Environment.NewLine +
            tplExport.Replace("$iconName$", req.IconName).Replace(_REPLACE_TO_EMPTY, string.Empty));

        // 修改iconSelect.js
        var iconSelectFile    = Path.Combine(_clientProjectPath, "src", "config", "iconSelect.js");
        var iconSelectContent = await File.ReadAllTextAsync(iconSelectFile);
        iconSelectContent = iconSelectContent.Replace("export default", "exportDefault:").Replace("'", "\"");
        iconSelectContent = Regex.Replace(iconSelectContent, "([a-zA-Z]+):", "\"$1\":");
        iconSelectContent = "{" + iconSelectContent + "}";
        var iconExportJsInfo = iconSelectContent.ToObject<IconExportJsInfo>();
        iconExportJsInfo.ExportDefault.Icons.Last().Icons.Add($"sc-icon-{req.IconName.ToLowerInvariant()}");
        var newContent = iconExportJsInfo.ToJson().TrimStart('{')[..^1].Replace("\"exportDefault\":", "export default");

        await File.WriteAllTextAsync(iconSelectFile, newContent);
    }

    /// <summary>
    ///     生成接口代码
    /// </summary>
    public async Task GenerateJsCodeAsync()
    {
        // 模板文件
        var tplOuter = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "api", "tpl", "outer.js"));
        var tplInner = await File.ReadAllTextAsync(Path.Combine(_clientProjectPath, "src", "api", "tpl", "inner.js"));

        IEnumerable<string> Select(QueryApiRsp item)
        {
            return item.Children.Select(x => tplInner.Replace("$actionDesc$", x.Summary)
                                                     .Replace( //
                                                         "$actionName$"
                                                       , Regex.Replace(x.Name, @"\.(\w)"
                                                                     , y => y.Groups[1].Value.ToUpperInvariant()))
                                                     .Replace("$actionPath$", x.Id)
                                                     .Replace( //
                                                         "$actionMethod$",       x.Method?.ToLowerInvariant())
                                                     .Replace(_REPLACE_TO_EMPTY, string.Empty)); //
        }

        foreach (var item in _apiService.ReflectionList(false)) {
            var dir = Path.Combine(_clientProjectPath, "src", "api", item.Namespace);
            if (!Directory.Exists(dir)) {
                _ = Directory.CreateDirectory(dir);
            }

            var file = Path.Combine(dir, $"{item.Name.Replace(".", string.Empty)}.js");

            var content = tplOuter.Replace("$controllerDesc$", item.Summary)
                                  .Replace("$controllerPath$", item.Id)
                                  .Replace( //
                                      "$inner$"
                       , string.Join(Environment.NewLine + Environment.NewLine, Select(item)))
                                  .Replace(_REPLACE_TO_EMPTY, string.Empty);

            await File.WriteAllTextAsync(file, content);
        }
    }

    private static async Task WriteCodeFileAsync(GenerateCsCodeReq req, string tplFile, string writeFile)
    {
        var tplContent = await File.ReadAllTextAsync(tplFile);
        tplContent = tplContent.Replace(nameof(Tpl), Enum.GetName(req.Type)![..3])
                               .Replace("示例",                    req.ModuleRemark)
                               .Replace("Example",               req.ModuleName)
                               .Replace("NetAdmin.SysComponent", nameof(SysComponent));

        await File.WriteAllTextAsync(writeFile, tplContent);
    }
}