using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NetAdmin.DataContract.Dto.Sys.Endpoint;
using NetAdmin.Infrastructure.Utils;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IEndPointApi" />
public class EndPointApi : ApiBase<IEndPointApi>, IEndPointApi
{
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

    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly XmlCommentHelper                    _xmlCommentHelper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EndPointApi" /> class.
    /// </summary>
    public EndPointApi( //
        XmlCommentHelper xmlCommentHelper, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        _xmlCommentHelper                   = xmlCommentHelper;
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    /// <inheritdoc />
    public async Task GenerateJsCode([Required] string diskPath)
    {
        static IEnumerable<string> Select(QueryEndpointRsp item)
        {
            return item.Children.Select(x => string.Format(              //
                                            CultureInfo.InvariantCulture //
                                          , _TMP_JSAPI_INNER             //
                                          , x.Summary                    //
                                          , Regex.Replace(x.Name, @"\.(\w)"
                                                        , xx => xx.Groups[1]
                                                                  .Value.ToUpper(CultureInfo.InvariantCulture)) //
                                          , x.Path                                                              //
                                          , x.Method?.ToLower(CultureInfo.InvariantCulture)));                  //
        }

        foreach (var item in await List()) {
            var dir = Path.Combine(diskPath, item.Type);
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            var file = Path.Combine(dir, $"{item.Name.Replace(".", string.Empty)}.js");
            var content = string.Format(                                               //
                CultureInfo.InvariantCulture                                           //
              , _TMP_JSAPI_OUTER                                                       //
              , item.Summary                                                           //
              , item.Path                                                              //
              , string.Join(Environment.NewLine + Environment.NewLine, Select(item))); //
            await File.WriteAllTextAsync(file, content);
        }
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryEndpointRsp>> List()
    {
        var regex = new Regex(@"\.(\w+)\.Implements", RegexOptions.Compiled);

        QueryEndpointRsp SelectQueryEndpointRsp(IGrouping<TypeInfo, ControllerActionDescriptor> group)
        {
            var first = group.First()!;
            return new QueryEndpointRsp {
                                            Summary = _xmlCommentHelper.GetComments(group.Key)
                                          , Name    = first.ControllerName
                                          , Path = Regex.Replace( //
                                                first.AttributeRouteInfo!.Template!, $"/{first.ActionName}$"
                                              , string.Empty)
                                          , Children = GetChildren(group)
                                          , Type     = regex.Match(group.Key.Namespace!).Groups[1].Value
                                        };
        }

        var actionGroup //
            = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>()
                                                 .GroupBy(x => x.ControllerTypeInfo);

        var ret = actionGroup.Select(SelectQueryEndpointRsp);

        return Task.FromResult(ret);
    }

    private IEnumerable<QueryEndpointRsp> GetChildren(IEnumerable<ControllerActionDescriptor> actionDescriptors)
    {
        return actionDescriptors //
            .Select(x => new QueryEndpointRsp {
                                                  Summary = _xmlCommentHelper.GetComments(x.MethodInfo)
                                                , Name    = x.ActionName
                                                , Path    = x.AttributeRouteInfo!.Template
                                                , Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                                                            .FirstOrDefault()
                                                            ?.HttpMethods.First()
                                              });
    }
}