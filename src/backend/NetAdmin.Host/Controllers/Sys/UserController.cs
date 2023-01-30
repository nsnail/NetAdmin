using Furion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     用户服务
/// </summary>
public class UserController : ControllerBase<IUserService>, IUserModule
{
    private readonly IConfigCache _configCache;
    private readonly IUserCache   _userCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    public UserController(IUserService service, IUserCache userCache, IConfigCache configCache) //
        : base(service)
    {
        _userCache   = userCache;
        _configCache = configCache;
    }

    /// <summary>
    ///     批量删除用户
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     检查手机号是否可用
    /// </summary>
    [AllowAnonymous]
    public async Task<bool> CheckMobileAvaliable(CheckMobileAvaliableReq req)
    {
        return await Service.CheckMobileAvaliable(req);
    }

    /// <summary>
    ///     检查用户名是否可用
    /// </summary>
    [AllowAnonymous]
    public async Task<bool> CheckUserNameAvaliable(CheckUserNameAvaliableReq req)
    {
        return await Service.CheckUserNameAvaliable(req);
    }

    /// <summary>
    ///     创建用户
    /// </summary>
    [Transaction]
    public async Task<QueryUserRsp> Create(CreateUserReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除用户
    /// </summary>
    [NonAction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     密码登录
    /// </summary>
    [AllowAnonymous]
    public async Task<LoginRsp> PwdLogin(PwdLoginReq req)
    {
        var ret = await Service.PwdLogin(req);

        SetHeaderToken(ret);
        return ret;
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    public async Task<IEnumerable<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     查询用户档案
    /// </summary>
    public async Task<IEnumerable<QueryUserProfileRsp>> QueryProfile(QueryReq<QueryUserProfileReq> req)
    {
        return await Service.QueryProfile(req);
    }

    /// <summary>
    ///     注册用户
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task Register(RegisterReq req)
    {
        var config = await _configCache.GetLatestConfig();

        await Service.Register(req with {
                                            DeptId = config.UserRegisterDeptId
                                          , PositionIds = new[] { config.UserRegisterPosId }
                                          , RoleIds = new[] { config.UserRegisterRoleId }
                                          , Profile = new CreateUserProfileReq()
                                          , Activated = !config.UserRegisterConfirm
                                          , Mobile = req.VerifySmsCodeReq.DestMobile
                                        });
    }

    /// <summary>
    ///     重设密码
    /// </summary>
    [AllowAnonymous]
    public async Task ResetPassword(ResetPasswordReq req)
    {
        await Service.ResetPassword(req);
    }

    /// <summary>
    ///     短信登录
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public async Task<LoginRsp> SmsLogin(SmsLoginReq req)
    {
        var ret = await Service.SmsLogin(req);
        SetHeaderToken(ret);
        return ret;
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    [Transaction]
    public async Task<QueryUserRsp> Update(UpdateUserReq req)
    {
        return await Service.Update(req);
    }

    /// <summary>
    ///     当前用户信息
    /// </summary>
    public async Task<QueryUserRsp> UserInfo()
    {
        return await _userCache.UserInfo();
    }

    private static void SetHeaderToken(LoginRsp ret)
    {
        // 设置响应报文头
        App.HttpContext.Response.Headers[Chars.FLG_ACCESS_TOKEN]   = ret.AccessToken;
        App.HttpContext.Response.Headers[Chars.FLG_X_ACCESS_TOKEN] = ret.RefreshToken;
    }
}