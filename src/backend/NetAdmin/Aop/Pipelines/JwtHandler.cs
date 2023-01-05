using Furion.Authorization;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.Aop.Pipelines;

/// <inheritdoc />
[SuppressSniffer]
public class JwtHandler : AppAuthorizeHandler
{
    private readonly IFreeSql _sql;

    /// <summary>
    ///     Initializes a new instance of the <see cref="JwtHandler" /> class.
    /// </summary>
    public JwtHandler(IFreeSql sql)
    {
        _sql = sql;
    }

    /// <inheritdoc />
    public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 无法从token中获取contextuser，拒绝访问
        var claim = context.User.FindFirst(nameof(ContextUser));
        if (claim is null) {
            return false;
        }

        ContextUser contextUser;
        try {
            contextUser = claim.Value.Object<ContextUser>();
        }
        catch (Exception) {
            return false;
        }

        // 数据库不存在contextuser，或用户已被禁用，拒绝访问
        var dbUser = await _sql.Select<TbSysUser>().Where(x => x.Token == contextUser.Token).FirstAsync();
        if (dbUser is null || !dbUser.BitSet.HasFlag(Enums.SysUserBits.Enabled)) {
            return false;
        }

        //数据库不存在有效的角色，拒绝访问
        var roles = await _sql.Select<TbSysUserRole, TbSysRole>()
                              .InnerJoin((a, b) => a.RoleId == b.Id)
                              .Where((a, b) => a.UserId == dbUser.Id && (b.BitSet & (long)Enums.SysRoleBits.Enabled) ==
                                         (long)Enums.SysRoleBits.Enabled)
                              .ToListAsync((a, b) => new { a, b });
        if (!roles.Any()) {
            return false;
        }

        //忽略权限控制，允许访问
        return roles.Any(x => x.b.BitSet.HasFlag(Enums.SysRoleBits.IgnorePermissionControl));
    }
}