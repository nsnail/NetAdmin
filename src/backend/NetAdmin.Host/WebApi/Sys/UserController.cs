using Furion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     用户服务
/// </summary>
public class UserController : ControllerBase<IUserService>, IUserModule
{
    private readonly IUserCache _userCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    public UserController(IUserService service, IUserCache userCache) //
        : base(service)
    {
        _userCache = userCache;
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
    ///     用户登录
    /// </summary>
    [AllowAnonymous]
    public async Task<LoginRsp> Login(LoginReq req)
    {
        var ret = await Service.Login(req);

        // 设置响应报文头
        App.HttpContext.Response.Headers[Chars.FLG_ACCESS_TOKEN]   = ret.AccessToken;
        App.HttpContext.Response.Headers[Chars.FLG_X_ACCESS_TOKEN] = ret.RefreshToken;
        return ret;
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    public async Task<IEnumerable<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        return await Service.Query(req);
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
}