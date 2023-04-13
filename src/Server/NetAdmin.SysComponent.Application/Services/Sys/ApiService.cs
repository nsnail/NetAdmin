using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IApiService" />
public sealed class ApiService : RepositoryService<Sys_Api, IApiService>, IApiService
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly XmlCommentReader                    _xmlCommentReader;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiService" /> class.
    /// </summary>
    public ApiService(Repository<Sys_Api>                 rpo, XmlCommentReader xmlCommentReader
                    , IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) //
        : base(rpo)
    {
        _xmlCommentReader                   = xmlCommentReader;
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询接口
    /// </summary>
    public async Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryApiRsp>>();
    }

    /// <inheritdoc />
    public IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true)
    {
        var regex = new Regex(@"\.(\w+)$", RegexOptions.Compiled);

        QueryApiRsp SelectQueryApiRsp(IGrouping<TypeInfo, ControllerActionDescriptor> group)
        {
            var first = group.First()!;
            return new QueryApiRsp {
                                       Summary = _xmlCommentReader.GetComments(group.Key)
                                     , Name    = first.ControllerName
                                     , Id = Regex.Replace( //
                                           first.AttributeRouteInfo!.Template!, $"/{first.ActionName}$", string.Empty)
                                     , Children  = GetChildren(group)
                                     , Namespace = regex.Match(group.Key.Namespace!).Groups[1].Value.ToLowerInvariant()
                                   };
        }

        var actionDescriptors //
            = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>();

        if (excludeAnonymous) {
            actionDescriptors = actionDescriptors.Where(x => x.EndpointMetadata.All(y => y is AllowAnonymousAttribute));
        }

        var actionGroup //
            = actionDescriptors.GroupBy(x => x.ControllerTypeInfo);

        return actionGroup.Select(SelectQueryApiRsp);
    }

    /// <inheritdoc />
    public async Task SyncAsync()
    {
        _ = await Rpo.DeleteAsync(_ => true);

        var list = ReflectionList(false);

        EnableCascadeSave = true;
        foreach (var item in list) {
            var entity = item.Adapt<Sys_Api>();
            _ = await Rpo.InsertAsync(entity);
        }
    }

    /// <inheritdoc />
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<QueryApiRsp> GetChildren(IEnumerable<ControllerActionDescriptor> actionDescriptors)
    {
        return actionDescriptors //
            .Select(x => new QueryApiRsp {
                                             Summary = _xmlCommentReader.GetComments(x.MethodInfo)
                                           , Name    = x.ActionName
                                           , Id      = x.AttributeRouteInfo!.Template
                                           , Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                                                       .FirstOrDefault()
                                                       ?.HttpMethods.First()
                                         });
    }
}