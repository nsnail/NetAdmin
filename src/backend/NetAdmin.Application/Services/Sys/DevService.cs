using System.Globalization;
using System.Text.RegularExpressions;
using NetAdmin.Application.Services.Sys.Dependency;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

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
}