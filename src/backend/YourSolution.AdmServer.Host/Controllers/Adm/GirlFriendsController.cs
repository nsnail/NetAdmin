using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using YourSolution.AdmServer.Application.Modules.Adm;
using YourSolution.AdmServer.Application.Services.Adm.Dependency;
using YourSolution.AdmServer.Cache.Adm.Dependency;
using YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

namespace YourSolution.AdmServer.Host.Controllers.Adm;

/// <summary>
///     女朋友服务
/// </summary>
[ApiDescriptionSettings(nameof(Adm), Module = nameof(Adm))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class GirlFriendsController(IGirlFriendsCache cache) : ControllerBase<IGirlFriendsCache, IGirlFriendsService>(cache), IGirlFriendsModule
{
    /// <summary>
    ///     批量删除女朋友
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     女朋友计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     女朋友分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建女朋友
    /// </summary>
    [Transaction]
    public Task<QueryGirlFriendsRsp> CreateAsync(CreateGirlFriendsReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除女朋友
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑女朋友
    /// </summary>
    [Transaction]
    public Task<QueryGirlFriendsRsp> EditAsync(EditGirlFriendsReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出女朋友
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个女朋友
    /// </summary>
    public Task<QueryGirlFriendsRsp> GetAsync(QueryGirlFriendsReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询女朋友
    /// </summary>
    public Task<PagedQueryRsp<QueryGirlFriendsRsp>> PagedQueryAsync(PagedQueryReq<QueryGirlFriendsReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询女朋友
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryGirlFriendsRsp>> QueryAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        return Cache.QueryAsync(req);
    }
}