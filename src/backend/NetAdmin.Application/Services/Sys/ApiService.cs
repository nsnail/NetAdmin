using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.Api;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IApiService" />
public class ApiService : RepositoryService<TbSysApi, IApiService>, IApiService
{
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
    public Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> Create(CreateApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQuery(PagedQueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询接口
    /// </summary>
    public async Task<List<QueryApiRsp>> Query(QueryReq<QueryApiReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryApiRsp>());
    }

    /// <inheritdoc />
    public IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true)
    {
        var regex = new Regex(@"\.(\w+)$", RegexOptions.Compiled);

        QueryApiRsp SelectQueryApiRsp(IGrouping<TypeInfo, ControllerActionDescriptor> group)
        {
            var first = group.First()!;
            return new QueryApiRsp {
                                       Summary = _xmlCommentHelper.GetComments(group.Key)
                                     , Name    = first.ControllerName
                                     , Id = Regex.Replace( //
                                           first.AttributeRouteInfo!.Template!, $"/{first.ActionName}$", string.Empty)
                                     , Children = GetChildren(group)
                                     , Namespace = regex.Match(group.Key.Namespace!)
                                                        .Groups[1]
                                                        .Value.ToLower(CultureInfo.InvariantCulture)
                                   };
        }

        var actionDescriptors //
            = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>();

        if (excludeAnonymous) {
            actionDescriptors = actionDescriptors.Where(x => x.EndpointMetadata.All(
                                                            y => y.GetType() != typeof(AllowAnonymousAttribute)));
        }

        var actionGroup //
            = actionDescriptors.GroupBy(x => x.ControllerTypeInfo);

        var ret = actionGroup.Select(SelectQueryApiRsp);

        return ret;
    }

    /// <inheritdoc />
    public async Task Sync()
    {
        await Rpo.DeleteAsync(a => true);

        var list = ReflectionList(false);

        EnableCascadeSave = true;
        foreach (var item in list) {
            var entity = item.Adapt<TbSysApi>();
            await Rpo.InsertAsync(entity);
        }
    }

    /// <inheritdoc />
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