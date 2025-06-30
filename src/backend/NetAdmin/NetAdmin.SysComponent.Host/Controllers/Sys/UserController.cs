using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     用户服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class UserController(IUserCache cache, IConfigCache configCache) : ControllerBase<IUserCache, IUserService>(cache), IUserModule
{
    /// <summary>
    ///     批量删除用户
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     检查邀请码是否正确
    /// </summary>
    [AllowAnonymous]
    public Task<bool> CheckInviterAvailableAsync(CheckInviterAvailableReq req)
    {
        return Cache.CheckInviterAvailableAsync(req);
    }

    /// <summary>
    ///     检查手机号码是否可用
    /// </summary>
    [AllowAnonymous]
    public Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        return Cache.CheckMobileAvailableAsync(req);
    }

    /// <summary>
    ///     检查用户名是否可用
    /// </summary>
    [AllowAnonymous]
    public Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        return Cache.CheckUserNameAvailableAsync(req);
    }

    /// <summary>
    ///     用户计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryUserReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     用户分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建用户
    /// </summary>
    [Transaction]
    public Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除用户
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑用户
    /// </summary>
    [Transaction]
    public Task<QueryUserRsp> EditAsync(EditUserReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出用户
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个用户
    /// </summary>
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取当前用户应用配置
    /// </summary>
    public Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync()
    {
        return Cache.GetSessionUserAppConfigAsync();
    }

    /// <summary>
    ///     密码登录
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public async Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        var ret = await Cache.LoginByPwdAsync(req).ConfigureAwait(false);
        ret.SetToRspHeader();
        return ret;
    }

    /// <summary>
    ///     短信登录
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public async Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        var ret = await Cache.LoginBySmsAsync(req).ConfigureAwait(false);
        ret.SetToRspHeader();
        return ret;
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    public Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     查询用户档案
    /// </summary>
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return Cache.QueryProfileAsync(req);
    }

    /// <summary>
    ///     注册用户
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public async Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
    {
        var config = await configCache.GetLatestConfigAsync().ConfigureAwait(false);

        return await Cache.RegisterAsync(req with {
                                                      DeptId = config.UserRegisterDeptId
                                                    , RoleIds = [config.UserRegisterRoleId]
                                                    , Profile = new CreateUserProfileReq()
                                                    , Enabled = !config.UserRegisterConfirm
                                                    , Mobile = req.VerifySmsCodeReq.DestDevice
                                                  })
                          .ConfigureAwait(false);
    }

    /// <summary>
    ///     重设密码
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public Task<int> ResetPasswordAsync(ResetPasswordReq req)
    {
        return Cache.ResetPasswordAsync(req);
    }

    /// <summary>
    ///     设置用户头像
    /// </summary>
    [Transaction]
    public Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req)
    {
        return Cache.SetAvatarAsync(req);
    }

    /// <summary>
    ///     设置邮箱
    /// </summary>
    [Transaction]
    public Task<UserInfoRsp> SetEmailAsync(SetEmailReq req)
    {
        return Cache.SetEmailAsync(req);
    }

    /// <summary>
    ///     启用/禁用用户
    /// </summary>
    [Transaction]
    public Task<int> SetEnabledAsync(SetUserEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }

    /// <summary>
    ///     设置手机号码
    /// </summary>
    [Transaction]
    public Task<UserInfoRsp> SetMobileAsync(SetMobileReq req)
    {
        return Cache.SetMobileAsync(req);
    }

    /// <summary>
    ///     设置密码
    /// </summary>
    [Transaction]
    public Task<int> SetPasswordAsync(SetPasswordReq req)
    {
        return Cache.SetPasswordAsync(req);
    }

    /// <summary>
    ///     设置当前用户应用配置
    /// </summary>
    public Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req)
    {
        return Cache.SetSessionUserAppConfigAsync(req);
    }

    /// <summary>
    ///     当前用户信息
    /// </summary>
    public Task<UserInfoRsp> UserInfoAsync()
    {
        return Cache.UserInfoAsync();
    }
}