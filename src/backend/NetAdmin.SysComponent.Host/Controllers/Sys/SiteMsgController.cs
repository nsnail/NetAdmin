using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     站内信服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class SiteMsgController(ISiteMsgCache cache) : ControllerBase<ISiteMsgCache, ISiteMsgService>(cache)
                                                           , ISiteMsgModule
{
    /// <summary>
    ///     批量删除站内信
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建站内信
    /// </summary>
    [Transaction]
    public Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除站内信
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     站内信是否存在
    /// </summary>
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个站内信
    /// </summary>
    public Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询站内信
    /// </summary>
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询站内信
    /// </summary>
    public Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     未读数量
    /// </summary>
    public Task<long> UnreadCountAsync()
    {
        return Cache.UnreadCountAsync();
    }

    /// <summary>
    ///     更新站内信
    /// </summary>
    [Transaction]
    public Task<QuerySiteMsgRsp> UpdateAsync(UpdateSiteMsgReq req)
    {
        return Cache.UpdateAsync(req);
    }
}