using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Api;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IApiService" />
public sealed class ApiService(
    BasicRepository<Sys_Api, string>    rpo                                 //
  , XmlCommentReader                    xmlCommentReader                    //
  , IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) //
    : RedisService<Sys_Api, string, IApiService>(rpo), IApiService
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_Api>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_Api).GetProperty(y)!.GetValue(x.Key)!.ToString())
                            , x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> EditAsync(EditApiReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryApiReq, ExportApiRsp>(QueryInternal, req, Ln.接口导出);
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryApiRsp>> PlainQueryAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryApiRsp>>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryApiRsp>>();
    }

    /// <inheritdoc />
    public IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true)
    {
        var regex = new Regex(@"\.(\w+)$", RegexOptions.Compiled);

        var actionDescriptors //
            = actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>();

        if (excludeAnonymous) {
            actionDescriptors = actionDescriptors.Where(x => x.EndpointMetadata.All(y => y is AllowAnonymousAttribute));
        }

        var actionGroup //
            = actionDescriptors.GroupBy(x => x.ControllerTypeInfo);

        return actionGroup.Select(SelectQueryApiRsp);

        QueryApiRsp SelectQueryApiRsp(IGrouping<TypeInfo, ControllerActionDescriptor> group)
        {
            var first = group.First()!;

            var id = Regex.Replace( //
                first.AttributeRouteInfo!.Template!, $"/{first.ActionName}$", string.Empty);
            return new QueryApiRsp {
                                       Summary   = xmlCommentReader.GetComments(group.Key)
                                     , Name      = first.ControllerName
                                     , Id        = id
                                     , Children  = GetChildren(group)
                                     , Namespace = regex.Match(group.Key.Namespace!).Groups[1].Value.ToLowerInvariant()
                                     , PathCrc32 = id.Crc32()
                                   };
        }
    }

    /// <inheritdoc />
    public async Task SyncAsync()
    {
        await using var locker = await GetLockerOnceAsync(nameof(SyncAsync)).ConfigureAwait(false);
        _ = await Rpo.DeleteAsync(_ => true).ConfigureAwait(false);

        var list = ReflectionList(false);

        EnableCascadeSave = true;
        foreach (var item in list) {
            var entity = item.Adapt<Sys_Api>();
            _ = await Rpo.InsertAsync(entity).ConfigureAwait(false);
        }
    }

    private IEnumerable<QueryApiRsp> GetChildren(IEnumerable<ControllerActionDescriptor> actionDescriptors)
    {
        return actionDescriptors //
            .Select(x => {
                var id = x.AttributeRouteInfo!.Template;
                return new QueryApiRsp {
                                           Summary   = xmlCommentReader.GetComments(x.MethodInfo)
                                         , Name      = x.ActionName
                                         , Id        = id
                                         , Method    = x.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First()
                                         , PathCrc32 = id.Crc32()
                                       };
            });
    }

    private ISelect<Sys_Api> QueryInternal(QueryReq<QueryApiReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}