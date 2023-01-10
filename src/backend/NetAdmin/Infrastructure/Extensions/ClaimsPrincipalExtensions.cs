using System.Security.Claims;
using Furion.DependencyInjection;
using NetAdmin.DataContract;
using NSExt.Extensions;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     ClaimsPrincipal  扩展方法
/// </summary>
[SuppressSniffer]
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    ///     转换成ContextUser
    /// </summary>
    public static ContextUser AsContextUser(this ClaimsPrincipal me)
    {
        var claim = me.FindFirst(nameof(ContextUser));
        if (claim is null) {
            return null;
        }

        ContextUser contextUser;
        try {
            contextUser = claim.Value.Object<ContextUser>();
        }
        catch (Exception) {
            return null;
        }

        return contextUser;
    }
}