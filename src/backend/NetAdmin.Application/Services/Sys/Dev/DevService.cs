using System.Globalization;
using System.Text.RegularExpressions;
using NetAdmin.Application.Services.Sys.Dependency;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys.Dev;

/// <inheritdoc cref="IDevService" />
public class DevService : ServiceBase<DevService>, IDevService
{
    private const string _TMP_EXPORT = """
export {{ default as {0} }} from './{0}.vue'
""";

    private const string _TMP_JSAPI_INNER = """
    /**
     * {0}
     */
    {1} :{{
        url: `${{config.API_URL}}/{2}`,
        name: `{0}`,
        {3}:async function(data, config={{}}) {{
            return await http.{3}(this.url,data, config)
        }}
    }},

""";

    private const string _TMP_JSAPI_OUTER = """
/**
 *  {0}
 *  @module @/{1}
 */

import config from "@/config"
import http from "@/utils/request"

export default {{

{2}

}}

""";

    private const string _TMP_SVG = """
<template>
    {0}
</template>
""";

    private readonly IApiService _apiService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DevService" /> class.
    /// </summary>
    public DevService(IApiService apiService)
    {
        _apiService = apiService;
    }

    /// <inheritdoc />
    public async ValueTask GenerateCsCode(GenerateCsCodeReq req)
    {
        var projectDirs = Directory.GetDirectories(req.ProjectPath);
        var moduleType  = Enum.GetName(req.Type)!;

        // 业务逻辑层目录
        var appDir = projectDirs.First(x => x.EndsWith(nameof(Application), true, CultureInfo.InvariantCulture));

        // 数据契约层目录
        var dataDir = projectDirs.First(x => x.EndsWith(nameof(DataContract), true, CultureInfo.InvariantCulture));

        // Api接口层目录
        var hostDir = projectDirs.First(x => x.EndsWith("Host", true, CultureInfo.InvariantCulture));

        // 模板目录
        var tempDir = Path.Combine(req.ProjectPath, @"../../assets/code-gen/templates/backend");

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
        var dtoDir = Path.Combine(dataDir, nameof(DataContract.Dto), moduleType, req.ModuleName);
        if (!Directory.Exists(dtoDir)) {
            Directory.CreateDirectory(dtoDir);
        }

        // 数据契约层 - Entity目录
        var entityDir = Path.Combine(dataDir, nameof(DataContract.DbMaps));
        if (!Directory.Exists(entityDir)) {
            Directory.CreateDirectory(entityDir);
        }

        // Api接口层 - Controller目录
        var controllerDir = Path.Combine(hostDir, "WebApi", moduleType);
        if (!Directory.Exists(controllerDir)) {
            Directory.CreateDirectory(controllerDir);
        }

        // iModule
        await WriteCodeFile(req, Path.Combine(tempDir,    "App/IModule.txt")
                     ,           Path.Combine(modulesDir, $"I{req.ModuleName}Module.cs"));

        // iService
        await WriteCodeFile(req, Path.Combine(tempDir,               "App/IService.txt")
                     ,           Path.Combine(servicesDependencyDir, $"I{req.ModuleName}Service.cs"));

        // service
        await WriteCodeFile(req, Path.Combine(tempDir,     "App/Service.txt")
                     ,           Path.Combine(servicesDir, $"{req.ModuleName}Service.cs"));

        // controller
        await WriteCodeFile(req, Path.Combine(tempDir,       "Host/Controller.txt")
                     ,           Path.Combine(controllerDir, $"{req.ModuleName}Controller.cs"));

        // createReq
        await WriteCodeFile(req, Path.Combine(tempDir, "Data/CreateReq.txt")
                     ,           Path.Combine(dtoDir,  $"Create{req.ModuleName}Req.cs"));

        // updateReq
        await WriteCodeFile(req, Path.Combine(tempDir, "Data/UpdateReq.txt")
                     ,           Path.Combine(dtoDir,  $"Update{req.ModuleName}Req.cs"));

        // queryReq
        await WriteCodeFile(req, Path.Combine(tempDir, "Data/QueryReq.txt")
                     ,           Path.Combine(dtoDir,  $"Query{req.ModuleName}Req.cs"));

        // queryRsp
        await WriteCodeFile(req, Path.Combine(tempDir, "Data/QueryRsp.txt")
                     ,           Path.Combine(dtoDir,  $"Query{req.ModuleName}Rsp.cs"));

        // entity
        await WriteCodeFile(req, Path.Combine(tempDir,   "Data/Entity.txt")
                     ,           Path.Combine(entityDir, $"Tb{moduleType}{req.ModuleName}.cs"));
    }

    /// <inheritdoc />
    public async ValueTask GenerateIconCode(GenerateIconCodeReq req)
    {
        var vueContent = string.Format(CultureInfo.InvariantCulture, _TMP_SVG, req.SvgCode);
        var dir        = Path.Combine(req.ProjectPath, "src", "assets", "icons");
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }

        var vueFile = Path.Combine(dir, $"{req.IconName}.vue");
        await File.WriteAllTextAsync(vueFile, vueContent);

        var indexjsFile = Path.Combine(dir, "index.js");
        await File.AppendAllTextAsync(
            indexjsFile
          , $"{Environment.NewLine}{string.Format(CultureInfo.InvariantCulture, _TMP_EXPORT, req.IconName)}");

        // 修改iconSelect.js
        var iconSelectFile    = Path.Combine(req.ProjectPath, "src", "config", "iconSelect.js");
        var iconSelectContent = await File.ReadAllTextAsync(iconSelectFile);
        iconSelectContent = iconSelectContent.Replace("export default", "exportDefault:").Replace("'", "\"");
        iconSelectContent = Regex.Replace(iconSelectContent, "([a-zA-Z]+):", "\"$1\":");
        iconSelectContent = "{" + iconSelectContent + "}";
        var iconExportJsInfo = iconSelectContent.Object<IconExportJsInfo>();
        iconExportJsInfo.ExportDefault.Icons.Last()
                        .Icons.Add($"sc-icon-{req.IconName.ToLower(CultureInfo.InvariantCulture)}");
        var newContent = iconExportJsInfo.Json(true)
                                         .TrimStart('{')
                                         .TrimEnd('}')
                                         .Replace("\"exportDefault\":", "export default");

        await File.WriteAllTextAsync(iconSelectFile, newContent);
    }

    /// <inheritdoc />
    public async ValueTask GenerateJsCode(string projectPath)
    {
        static IEnumerable<string> Select(QueryApiRsp item)
        {
            return item.Children.Select(x => string.Format(              //
                                            CultureInfo.InvariantCulture //
                                          , _TMP_JSAPI_INNER             //
                                          , x.Summary                    //
                                          , Regex.Replace(x.Name, @"\.(\w)"
                                                        , y => y.Groups[1]
                                                                .Value.ToUpper(CultureInfo.InvariantCulture)) //
                                          , x.Id                                                              //
                                          , x.Method?.ToLower(CultureInfo.InvariantCulture)));                //
        }

        foreach (var item in _apiService.ReflectionList(false)) {
            var dir = Path.Combine(projectPath, "src", "api", item.Namespace);
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            var file = Path.Combine(dir, $"{item.Name.Replace(".", string.Empty)}.js");
            var content = string.Format(                                               //
                CultureInfo.InvariantCulture                                           //
              , _TMP_JSAPI_OUTER                                                       //
              , item.Summary                                                           //
              , item.Id                                                                //
              , string.Join(Environment.NewLine + Environment.NewLine, Select(item))); //
            await File.WriteAllTextAsync(file, content);
        }
    }

    private static async Task WriteCodeFile(GenerateCsCodeReq req, string templateFile, string writeFile)
    {
        var content = string.Format( //
            CultureInfo.InvariantCulture, await File.ReadAllTextAsync(templateFile), nameof(NetAdmin), req.Type
          , req.ModuleName, req.ModuleRemark);

        await File.WriteAllTextAsync(writeFile, content);
    }
}