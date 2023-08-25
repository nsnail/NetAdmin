using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     字典目录缓存
/// </summary>
public sealed class DicCatalogCache : DistributedCache<IDicCatalogService>, IScoped, IDicCatalogCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicCatalogCache" /> class.
    /// </summary>
    public DicCatalogCache(IDistributedCache cache, IDicCatalogService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> CreateAsync(CreateDicCatalogReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> GetAsync(QueryDicCatalogReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryDicCatalogRsp> UpdateAsync(UpdateDicCatalogReq req)
    {
        return Service.UpdateAsync(req);
    }
}