using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ISiteMsgCache" />
public sealed class SiteMsgCache(IDistributedCache cache, ISiteMsgService service)
    : DistributedCache<ISiteMsgService>(cache, service), IScoped, ISiteMsgCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req)
    {
        return Service.GetMineAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return Service.PagedQueryMineAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task SetSiteMsgStatusAsync(UpdateSiteMsgFlagReq req)
    {
        return Service.SetSiteMsgStatusAsync(req);
    }

    /// <inheritdoc />
    public Task<long> UnreadCountAsync()
    {
        return Service.UnreadCountAsync();
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgRsp> UpdateAsync(UpdateSiteMsgReq req)
    {
        return Service.UpdateAsync(req);
    }
}