using NetAdmin.Domain.Dto.Sys.SiteMsgRole;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ISiteMsgRoleCache" />
public sealed class SiteMsgRoleCache(IDistributedCache cache, ISiteMsgRoleService service)
    : DistributedCache<ISiteMsgRoleService>(cache, service), IScoped, ISiteMsgRoleCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QuerySiteMsgRoleReq> req) {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgRoleReq> req) {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRoleRsp> CreateAsync(CreateSiteMsgRoleReq req) {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRoleRsp> EditAsync(EditSiteMsgRoleReq req) {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgRoleReq> req) {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRoleRsp> GetAsync(QuerySiteMsgRoleReq req) {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgRoleRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgRoleReq> req) {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySiteMsgRoleRsp>> QueryAsync(QueryReq<QuerySiteMsgRoleReq> req) {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QuerySiteMsgRoleReq> req) {
        return Service.SumAsync(req);
    }
}