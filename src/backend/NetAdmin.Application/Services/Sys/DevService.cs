using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Dev;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IDevService" />
public class DevService : ServiceBase<DevService>, IDevService
{
    private const string _REPLACE_TO_EMPTY = "//~";

    private readonly IApiService _apiService;
    private readonly JsonOptions _jsonOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DevService" /> class.
    /// </summary>
    public DevService(IApiService apiService, IOptions<JsonOptions> jsonOptions)
    {
        _apiService  = apiService;
        _jsonOptions = jsonOptions.Value;
    }

    /// <inheritdoc />
    public async Task GenerateCsCode(GenerateCsCodeReq req)
    {
        var projectDirs = Directory.GetDirectories(req.ProjectPath);

        // 模块类型（Sys、Biz、等）
        var moduleType = Enum.GetName(req.Type)!;

        // 业务逻辑层目录
        var appDir = projectDirs.First(x => x.EndsWith(nameof(Application), true, CultureInfo.InvariantCulture));

        // 数据契约层目录
        var dataDir = projectDirs.First(x => x.EndsWith(nameof(Domain), true, CultureInfo.InvariantCulture));

        // Api接口层目录
        var hostDir = projectDirs.First(x => x.EndsWith("Host", true, CultureInfo.InvariantCulture));

        // 业务逻辑层 - 模块目录
        var modulesDir = Path.Combine(appDir, nameof(Modules), moduleType);
        if (!Directory.Exists(modulesDir)) {
            Directory.CreateDirectory(modulesDir);
        }

        // 业务逻辑层 - 服务目录
        var servicesDir           = Path.Combine(appDir,      nameof(Services), moduleType);
        var servicesDependencyDir = Path.Combine(servicesDir, nameof(Dependency));
        if (!Directory.Exists(servicesDependencyDir)) {
            Directory.CreateDirectory(servicesDependencyDir);
        }

        // 数据契约层 - DTO目录
        var dtoDir = Path.Combine(dataDir, nameof(Domain.Dto), moduleType, req.ModuleName);
        if (!Directory.Exists(dtoDir)) {
            Directory.CreateDirectory(dtoDir);
        }

        // 数据契约层 - Entity目录
        var entityDir = Path.Combine(dataDir, nameof(Domain.DbMaps), moduleType);
        if (!Directory.Exists(entityDir)) {
            Directory.CreateDirectory(entityDir);
        }

        // Api接口层 - Controller目录
        var controllerDir = Path.Combine(hostDir, "Controllers", moduleType);
        if (!Directory.Exists(controllerDir)) {
            Directory.CreateDirectory(controllerDir);
        }

        // iModule
        await WriteCodeFile(req, Path.Combine(appDir,     nameof(Modules), nameof(Tpl), "IExampleModule.cs")
                     ,           Path.Combine(modulesDir, $"I{req.ModuleName}Module.cs"));

        // iService
        await WriteCodeFile(
            req, Path.Combine(appDir, nameof(Services), nameof(Tpl), nameof(Dependency), "IExampleService.cs")
     ,           Path.Combine(servicesDependencyDir, $"I{req.ModuleName}Service.cs"));

        // service
        await WriteCodeFile(req, Path.Combine(appDir,      nameof(Services), nameof(Tpl), "ExampleService.cs")
                     ,           Path.Combine(servicesDir, $"{req.ModuleName}Service.cs"));

        // controller
        await WriteCodeFile(req, Path.Combine(hostDir,       "Controllers", nameof(Tpl), "ExampleController.cs")
                     ,           Path.Combine(controllerDir, $"{req.ModuleName}Controller.cs"));

        // createReq
        await WriteCodeFile(
            req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "CreateExampleReq.cs")
     ,           Path.Combine(dtoDir,  $"Create{req.ModuleName}Req.cs"));

        // updateReq
        await WriteCodeFile(
            req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "UpdateExampleReq.cs")
     ,           Path.Combine(dtoDir,  $"Update{req.ModuleName}Req.cs"));

        // queryReq
        await WriteCodeFile(req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "QueryExampleReq.cs")
                     ,           Path.Combine(dtoDir,  $"Query{req.ModuleName}Req.cs"));

        // queryRsp
        await WriteCodeFile(req, Path.Combine(dataDir, nameof(Domain.Dto), nameof(Tpl), "Example", "QueryExampleRsp.cs")
                     ,           Path.Combine(dtoDir,  $"Query{req.ModuleName}Rsp.cs"));

        // entity
        await WriteCodeFile(req, Path.Combine(dataDir,   nameof(Domain.DbMaps), nameof(Tpl), "TbTplExample.cs")
                     ,           Path.Combine(entityDir, $"Tb{moduleType}{req.ModuleName}.cs"));
    }

    /// <inheritdoc />
    public async Task GenerateIconCode(GenerateIconCodeReq req)
    {
        var tplSvg = await File.ReadAllTextAsync(
            Path.Combine(req.ProjectPath, "src", "assets", "icons", "tpl", "Svg.vue"));
        var tplExport
            = await File.ReadAllTextAsync(Path.Combine(req.ProjectPath, "src", "assets", "icons", "tpl", "export.js"));

        var vueContent = tplSvg.Replace("$svgCode$", req.SvgCode).Replace(_REPLACE_TO_EMPTY, string.Empty);

        var dir = Path.Combine(req.ProjectPath, "src", "assets", "icons");
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }

        var vueFile = Path.Combine(dir, $"{req.IconName}.vue");
        await File.WriteAllTextAsync(vueFile, vueContent);

        var indexjsFile = Path.Combine(dir, "index.js");

        await File.AppendAllTextAsync(
            indexjsFile
          , $"{Environment.NewLine}{tplExport.Replace("$iconName$", req.IconName).Replace(_REPLACE_TO_EMPTY, string.Empty)}");

        // 修改iconSelect.js
        var iconSelectFile    = Path.Combine(req.ProjectPath, "src", "config", "iconSelect.js");
        var iconSelectContent = await File.ReadAllTextAsync(iconSelectFile);
        iconSelectContent = iconSelectContent.Replace("export default", "exportDefault:").Replace("'", "\"");
        iconSelectContent = Regex.Replace(iconSelectContent, "([a-zA-Z]+):", "\"$1\":");
        iconSelectContent = "{" + iconSelectContent + "}";
        var iconExportJsInfo = iconSelectContent.Object<IconExportJsInfo>(_jsonOptions.JsonSerializerOptions);
        iconExportJsInfo.ExportDefault.Icons.Last()
                        .Icons.Add($"sc-icon-{req.IconName.ToLower(CultureInfo.InvariantCulture)}");
        var newContent = iconExportJsInfo.Json(_jsonOptions.JsonSerializerOptions)
                                         .TrimStart('{')
                                         .TrimEnd('}')
                                         .Replace("\"exportDefault\":", "export default");

        await File.WriteAllTextAsync(iconSelectFile, newContent);
    }

    /// <inheritdoc />
    public async Task GenerateJsCode(string projectPath)
    {
        // 模板文件
        var tplOuter = await File.ReadAllTextAsync(Path.Combine(projectPath, "src", "api", "tpl", "outer.js"));
        var tplInner = await File.ReadAllTextAsync(Path.Combine(projectPath, "src", "api", "tpl", "inner.js"));

        IEnumerable<string> Select(QueryApiRsp item)
        {
            return item.Children.Select(x => tplInner.Replace("$actionDesc$", x.Summary)
                                                     .Replace( //
                                                         "$actionName$"
                                                       , Regex.Replace(x.Name, @"\.(\w)"
                                                                     , y => y.Groups[1]
                                                                             .Value.ToUpper(
                                                                                 CultureInfo.InvariantCulture)))
                                                     .Replace("$actionPath$", x.Id)
                                                     .Replace( //
                                                         "$actionMethod$"
                                                 , x.Method?.ToLower(CultureInfo.InvariantCulture))
                                                     .Replace(_REPLACE_TO_EMPTY, string.Empty)); //
        }

        foreach (var item in _apiService.ReflectionList(false)) {
            var dir = Path.Combine(projectPath, "src", "api", item.Namespace);
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
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

    private static async Task WriteCodeFile(GenerateCsCodeReq req, string tplFile, string writeFile)
    {
        var tplContent = await File.ReadAllTextAsync(tplFile);
        tplContent = tplContent.Replace(nameof(Tpl), Enum.GetName(req.Type));
        tplContent = tplContent.Replace("示例",        req.ModuleRemark);
        tplContent = tplContent.Replace("Example",   req.ModuleName);
        tplContent = tplContent.Replace("NetAdmin",  nameof(NetAdmin));

        await File.WriteAllTextAsync(writeFile, tplContent);
    }
}