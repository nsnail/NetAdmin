using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ISiteMsgFlagCache" />
public sealed class SiteMsgFlagCache(IDistributedCache cache, ISiteMsgFlagService service)
    : DistributedCache<ISiteMsgFlagService>(cache, service), IScoped, ISiteMsgFlagCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> CreateAsync(CreateSiteMsgFlagReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> GetAsync(QuerySiteMsgFlagReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgFlagRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySiteMsgFlagRsp>> QueryAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> UpdateAsync(UpdateSiteMsgFlagReq req)
    {
        return Service.UpdateAsync(req);
    }
}