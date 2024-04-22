using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsgUser;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ISiteMsgUserCache" />
public sealed class SiteMsgUserCache(IDistributedCache cache, ISiteMsgUserService service)
    : DistributedCache<ISiteMsgUserService>(cache, service), IScoped, ISiteMsgUserCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgUserRsp> CreateAsync(CreateSiteMsgUserReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgUserRsp> GetAsync(QuerySiteMsgUserReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySiteMsgUserRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgUserReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySiteMsgUserRsp>> QueryAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgUserRsp> UpdateAsync(UpdateSiteMsgUserReq req)
    {
        return Service.UpdateAsync(req);
    }
}