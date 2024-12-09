using NetAdmin.Domain.Dto.Sys.Menu;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IMenuCache" />
public sealed class MenuCache(IDistributedCache cache, IMenuService service) //
    : DistributedCache<IMenuService>(cache, service), IScoped, IMenuCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryMenuReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryMenuReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryMenuRsp> EditAsync(EditMenuReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryMenuReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        return Service.UserMenusAsync();
    }
}