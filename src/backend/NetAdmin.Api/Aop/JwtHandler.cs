using Furion.Authorization;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.Application.Extensions;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.Api.Aop;

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
        var contextUser = context.User.AsContextUser();
        if (contextUser is null) {
            return false;
        }

        // 数据库不存在contextuser，或用户已被禁用，拒绝访问
        var dbUser = await _sql.Select<TbSysUser>().Where(x => x.Token == contextUser.Token).FirstAsync();
        if (dbUser is null || !dbUser.BitSet.HasFlag(Enums.BitSets.Enabled)) {
            return false;
        }

        // 数据库不存在有效的角色，拒绝访问
        var roles = await _sql.Select<TbSysUserRole, TbSysRole>()
                              .InnerJoin((a, b) => a.RoleId == b.Id)
                              .Where((a, b) => a.UserId == dbUser.Id && (b.BitSet & (long)Enums.BitSets.Enabled) ==
                                         (long)Enums.BitSets.Enabled)
                              .ToListAsync((a, b) => new { a, b });
        return roles.Any() &&

               // 忽略权限控制，允许访问
               roles.Any(x => x.b.BitSet.HasFlag(Enums.SysRoleBits.IgnorePermissionControl));
    }
}