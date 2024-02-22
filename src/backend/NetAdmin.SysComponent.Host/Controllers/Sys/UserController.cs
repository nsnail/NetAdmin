using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     用户服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class UserController(IUserCache cache, IConfigCache configCache)
    : ControllerBase<IUserCache, IUserService>(cache), IUserModule
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
    ///     检查手机号是否可用
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
    ///     用户是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个用户
    /// </summary>
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        return Cache.GetAsync(req);
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
    [Transaction]
    [AllowAnonymous]
    public async Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
    {
        var config = await configCache.GetLatestConfigAsync().ConfigureAwait(false);

        return await Cache.RegisterAsync(req with {
                                                      DeptId = config.UserRegisterDeptId
                                                    , RoleIds = new[] { config.UserRegisterRoleId }
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
    public Task<uint> ResetPasswordAsync(ResetPasswordReq req)
    {
        return Cache.ResetPasswordAsync(req);
    }

    /// <summary>
    ///     更新用户头像
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
    public Task SetEnabledAsync(SetUserEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }

    /// <summary>
    ///     设置手机号
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
    public Task<uint> SetPasswordAsync(SetPasswordReq req)
    {
        return Cache.SetPasswordAsync(req);
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    [Transaction]
    public Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        return Cache.UpdateAsync(req);
    }

    /// <summary>
    ///     当前用户信息
    /// </summary>
    public Task<UserInfoRsp> UserInfoAsync()
    {
        return Cache.UserInfoAsync();
    }
}