using NetAdmin.Domain.Contexts;
using NetAdmin.SysComponent.Cache.Sys;

namespace NetAdmin.BizServer.Host.Filters;

/// <inheritdoc />
[SuppressSniffer]
public sealed class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="JwtHandler" /> class.
    /// </summary>
    public JwtHandler() { }

    /// <summary>
    ///     验证管道
    /// </summary>
    public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 无法从token中获取context user，拒绝访问
        if (App.GetService<ContextUserToken>() == null) {
            return false;
        }

        // 数据库不存在context user，或用户已被禁用，拒绝访问
        var userInfo = await App.GetService<IUserCache>().UserInfoAsync();
        if (userInfo == null) {
            return false;
        }

        httpContext.Items.Add(Chars.FLG_CONTEXT_USER_INFO, userInfo);

        var path = httpContext.Request.Path.Value!.TrimStart('/');
        foreach (var role in userInfo.Roles) {
            // 忽略权限控制，直接通过
            if (role.IgnorePermissionControl) {
                return true;
            }

            // 获取所属角色接口权限 进行核对
            if (role.ApiIds?.Contains(path) ?? false) {
                return true;
            }
        }

        return false;
    }
}