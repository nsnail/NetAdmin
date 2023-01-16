using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.User;
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
    ///     创建用户
    /// </summary>
    [Transaction]
    public ValueTask<QueryUserRsp> Create(CreateUserReq req)
    {
        return Service.Create(req);
    }

    /// <summary>
    ///     删除用户
    /// </summary>
    [NonAction]
    public ValueTask<int> Delete(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     用户登录
    /// </summary>
    [AllowAnonymous]
    public ValueTask<LoginRsp> Login(LoginReq req)
    {
        return Service.Login(req);
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public ValueTask<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        return Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    [NonAction]
    public ValueTask<List<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    [Transaction]
    public ValueTask<QueryUserRsp> Update(UpdateUserReq req)
    {
        return Service.Update(req);
    }

    /// <summary>
    ///     当前用户信息
    /// </summary>
    public async ValueTask<QueryUserRsp> UserInfo()
    {
        return await _userCache.UserInfo();
    }
}