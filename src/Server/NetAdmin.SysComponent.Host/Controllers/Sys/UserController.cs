using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     用户服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class UserController : ControllerBase<IUserService>, IUserModule
{
    private readonly IConfigService _configService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    public UserController(IUserService service, IConfigService configService) //
        : base(service)
    {
        _configService = configService;
    }

    /// <summary>
    ///     批量删除用户
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     检查手机号是否可用
    /// </summary>
    [AllowAnonymous]
    public Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        return Service.CheckMobileAvailableAsync(req);
    }

    /// <summary>
    ///     检查用户名是否可用
    /// </summary>
    [AllowAnonymous]
    public Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        return Service.CheckUserNameAvailableAsync(req);
    }

    /// <summary>
    ///     创建用户
    /// </summary>
    [Transaction]
    public Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除用户
    /// </summary>
    [NonAction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     用户是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        return Service.GetAsync(req);
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     密码登录
    /// </summary>
    [AllowAnonymous]
    public async Task<LoginRsp> PwdLoginAsync(PwdLoginReq req)
    {
        var ret = await Service.PwdLoginAsync(req);

        SetHeaderToken(ret);
        return ret;
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    public Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     查询用户档案
    /// </summary>
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return Service.QueryProfileAsync(req);
    }

    /// <summary>
    ///     注册用户
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task RegisterAsync(RegisterReq req)
    {
        var config = await _configService.GetLatestConfigAsync();

        await Service.RegisterAsync(req with {
                                                 DeptId = config.UserRegisterDeptId
                                               , PositionIds = new[] { config.UserRegisterPosId }
                                               , RoleIds = new[] { config.UserRegisterRoleId }
                                               , Profile = new CreateUserProfileReq()
                                               , Enabled = !config.UserRegisterConfirm
                                               , Mobile = req.VerifySmsCodeReq.DestMobile
                                             });
    }

    /// <summary>
    ///     重设密码
    /// </summary>
    [AllowAnonymous]
    public Task ResetPasswordAsync(ResetPasswordReq req)
    {
        return Service.ResetPasswordAsync(req);
    }

    /// <summary>
    ///     短信登录
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public async Task<LoginRsp> SmsLoginAsync(SmsLoginReq req)
    {
        var ret = await Service.SmsLoginAsync(req);
        SetHeaderToken(ret);
        return ret;
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    [Transaction]
    public Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        return Service.UpdateAsync(req);
    }

    /// <summary>
    ///     当前用户信息
    /// </summary>
    public Task<QueryUserRsp> UserInfoAsync()
    {
        return Service.UserInfoAsync();
    }

    private static void SetHeaderToken(LoginRsp ret)
    {
        // 设置响应报文头
        App.HttpContext.Response.Headers[Chars.FLG_ACCESS_TOKEN]   = ret.AccessToken;
        App.HttpContext.Response.Headers[Chars.FLG_X_ACCESS_TOKEN] = ret.RefreshToken;
    }
}