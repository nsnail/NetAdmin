using Furion;
using Furion.Authorization;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Repositories;
using NSExt.Extensions;

namespace NetAdmin.Aop.Filters;

/// <inheritdoc />
[SuppressSniffer]
public class JwtHandler : AppAuthorizeHandler
{
    /// <inheritdoc />
    public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
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

        var dbUser = await App.GetRequiredService<Repository<TbSysUser>>().GetAsync(x => x.Token == contextUser.Token);
        return dbUser != null && dbUser.BitSet.HasFlag(Enums.SysUserBits.Enabled);
    }
}