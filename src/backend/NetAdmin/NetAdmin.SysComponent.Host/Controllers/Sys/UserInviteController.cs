using NetAdmin.Domain.Dto.Sys.UserInvite;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     用户邀请服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class UserInviteController(IUserInviteCache cache) : ControllerBase<IUserInviteCache, IUserInviteService>(cache), IUserInviteModule
{
    /// <summary>
    ///     批量删除用户邀请
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     用户邀请计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     用户邀请分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建用户邀请
    /// </summary>
    [Transaction]
    public Task<QueryUserInviteRsp> CreateAsync(CreateUserInviteReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除用户邀请
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑用户邀请
    /// </summary>
    [Transaction]
    public Task<QueryUserInviteRsp> EditAsync(EditUserInviteReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出用户邀请
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个用户邀请
    /// </summary>
    public Task<QueryUserInviteRsp> GetAsync(QueryUserInviteReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询用户邀请
    /// </summary>
    public Task<PagedQueryRsp<QueryUserInviteRsp>> PagedQueryAsync(PagedQueryReq<QueryUserInviteReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询用户邀请
    /// </summary>
    public Task<IEnumerable<QueryUserInviteRsp>> QueryAsync(QueryReq<QueryUserInviteReq> req)
    {
        return Cache.QueryAsync(req);
    }
}