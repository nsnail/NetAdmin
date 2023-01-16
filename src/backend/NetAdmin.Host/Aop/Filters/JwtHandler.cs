using Furion;
using Furion.Authorization;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.Aop.Filters;

/// <inheritdoc />
[SuppressSniffer]
public class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="JwtHandler" /> class.
    /// </summary>
    public JwtHandler() { }

    /// <inheritdoc />
    public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 无法从token中获取contextuser，拒绝访问
        var user = App.GetService<ContextUser>();
        if (user is null) {
            return false;
        }

        // 数据库不存在contextuser，或用户已被禁用，拒绝访问
        var userCache = App.GetRequiredService<IUserCache>();
        userCache.Service.User = user;
        var userInfo = await userCache.UserInfo();
        if (userInfo is null) {
            return false;
        }

        var path = httpContext.Request.Path.Value!.TrimStart('/');
        foreach (var role in userInfo.Roles) {
            // 忽略权限控制，直接通过
            if (role.IgnorePermissionControl) {
                return true;
            }

            // 获取所属角色接口权限 进行核对
            if (role.ApiIds.Contains(path)) {
                return true;
            }
        }

        return false;
    }
}