using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NetAdmin.Application.Repositories;
using NetAdmin.DataContract.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Api;
using NetAdmin.Infrastructure.Utils;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="IApiService" />
public class ApiService : RepositoryService<TbSysApi, IApiService>, IApiService, IDynamicApiController
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
    ///     Initializes a new instance of the <see cref="ApiService" /> class.
    /// </summary>
    public ApiService(Repository<TbSysApi>                rpo, XmlCommentHelper xmlCommentHelper
                    , IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) //
        : base(rpo)
    {
        _xmlCommentHelper                   = xmlCommentHelper;
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryApiRsp> Create(CreateApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task GenerateJsCode([Required] string diskPath)
    {
        static IEnumerable<string> Select(TbSysApi item)
        {
            return item.Children.Select(x => string.Format(              //
                                            CultureInfo.InvariantCulture //
                                          , _TMP_JSAPI_INNER             //
                                          , x.Summary                    //
                                          , Regex.Replace(x.Name, @"\.(\w)"
                                                        , xx => xx.Groups[1]
                                                                  .Value.ToUpper(CultureInfo.InvariantCulture)) //
                                          , x.Id                                                                //
                                          , x.Method?.ToLower(CultureInfo.InvariantCulture)));                  //
        }

        foreach (var item in await ReflectionList()) {
            var dir = Path.Combine(diskPath, item.Namespace);
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

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQuery(PagedQueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<List<QueryApiRsp>> Query(QueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryApiRsp>> ReflectionList()
    {
        var regex = new Regex(@"\.(\w+)\.Implements", RegexOptions.Compiled);

        QueryApiRsp SelectQueryApiRsp(IGrouping<TypeInfo, ControllerActionDescriptor> group)
        {
            var first = group.First()!;
            return new QueryApiRsp {
                                       Summary = _xmlCommentHelper.GetComments(group.Key)
                                     , Name    = first.ControllerName
                                     , Id = Regex.Replace( //
                                           first.AttributeRouteInfo!.Template!, $"/{first.ActionName}$", string.Empty)
                                     , Children  = GetChildren(group)
                                     , Namespace = regex.Match(group.Key.Namespace!).Groups[1].Value
                                   };
        }

        var actionGroup //
            = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>()
                                                 .GroupBy(x => x.ControllerTypeInfo);

        var ret = actionGroup.Select(SelectQueryApiRsp);

        return Task.FromResult(ret);
    }

    /// <inheritdoc />
    [Transaction]
    public async Task Sync()
    {
        await Rpo.DeleteAsync(a => true);

        var list = await ReflectionList();

        EnableCascadeSave = true;
        foreach (var item in list) {
            var entity = item.Adapt<TbSysApi>();
            await Rpo.InsertAsync(entity);
        }
    }

    /// <inheritdoc />
    [NonAction]
    public Task<NopReq> Update(NopReq req)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<QueryApiRsp> GetChildren(IEnumerable<ControllerActionDescriptor> actionDescriptors)
    {
        return actionDescriptors //
            .Select(x => new QueryApiRsp {
                                             Summary = _xmlCommentHelper.GetComments(x.MethodInfo)
                                           , Name    = x.ActionName
                                           , Id      = x.AttributeRouteInfo!.Template
                                           , Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                                                       .FirstOrDefault()
                                                       ?.HttpMethods.First()
                                         });
    }
}