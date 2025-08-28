using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.User;
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
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     用户邀请计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryUserInviteReq> req) {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     用户邀请分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserInviteReq> req) {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建用户邀请
    /// </summary>
    [Transaction]
    public Task<QueryUserInviteRsp> CreateAsync(CreateUserInviteReq req) {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     创建粉丝账号
    /// </summary>
    public Task<QueryUserRsp> CreateFansAccountAsync(CreateFansAccountReq req) {
        return Cache.CreateFansAccountAsync(req);
    }

    /// <summary>
    ///     删除用户邀请
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req) {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑用户邀请
    /// </summary>
    [Transaction]
    public Task<QueryUserInviteRsp> EditAsync(EditUserInviteReq req) {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出用户邀请
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserInviteReq> req) {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个用户邀请
    /// </summary>
    public Task<QueryUserInviteRsp> GetAsync(QueryUserInviteReq req) {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取自己是否允许自助充值
    /// </summary>
    public Task<bool> GetSelfDepositAllowedAsync() {
        return Cache.GetSelfDepositAllowedAsync();
    }

    /// <summary>
    ///     分页查询用户邀请
    /// </summary>
    public Task<PagedQueryRsp<QueryUserInviteRsp>> PagedQueryAsync(PagedQueryReq<QueryUserInviteReq> req) {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询用户邀请
    /// </summary>
    public Task<IEnumerable<QueryUserInviteRsp>> QueryAsync(QueryReq<QueryUserInviteReq> req) {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     查询可分配的角色
    /// </summary>
    public Task<IEnumerable<QueryDicContentRsp>> QueryRolesAllowApplyAsync() {
        return Cache.QueryRolesAllowApplyAsync();
    }

    /// <summary>
    ///     设置返佣比率
    /// </summary>
    public Task<int> SetCommissionRatioAsync(SetCommissionRatioReq req) {
        return Cache.SetCommissionRatioAsync(req);
    }

    /// <summary>
    ///     设置粉丝是否启用
    /// </summary>
    public Task<int> SetEnabledAsync(SetUserInviteEnabledReq req) {
        return Cache.SetEnabledAsync(req);
    }

    /// <summary>
    ///     修改粉丝角色
    /// </summary>
    [Transaction]
    public Task<int> SetFansRoleAsync(SetFansRoleReq req) {
        return Cache.SetFansRoleAsync(req);
    }

    /// <summary>
    ///     设置上级
    /// </summary>
    [Transaction]
    public Task<int> SetInviterAsync(SetInviterReq req) {
        return Cache.SetInviterAsync(req);
    }

    /// <summary>
    ///     设置允许自助充值
    /// </summary>
    public Task<int> SetSelfDepositAllowedAsync(SetSelfDepositAllowedReq req) {
        return Cache.SetSelfDepositAllowedAsync(req);
    }

    /// <summary>
    ///     用户邀请求和
    /// </summary>
    [NonAction]
    public Task<decimal> SumAsync(QueryReq<QueryUserInviteReq> req) {
        return Cache.SumAsync(req);
    }
}