using NetAdmin.BizServer.Host.Filters;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Host.Extensions;

namespace NetAdmin.BizServer.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     添加 FreeSql
    /// </summary>
    public static IServiceCollection AddFreeSqlWithArgs(this IServiceCollection me)
    {
        return me.AddFreeSql( //
            (Startup.Args.SyncStructure ? FreeSqlInitMethods.SyncStructure : FreeSqlInitMethods.None) |
            (Startup.Args.InsertSeedData ? FreeSqlInitMethods.InsertSeedData : FreeSqlInitMethods.None), freeSql => {
                // 数据权限过滤器
                _ = freeSql.GlobalFilter.ApplyOnlyIf<IFieldOwner>( //
                    Chars.FLG_FREE_SQL_GLOBAL_FILTER_DATA
                  , () => ContextUserInfo.Create()?.Roles.All(x => x.DataScope == DataScopes.Self) ?? false
                  , a => a.OwnerId == ContextUserInfo.Create().Id);
            });
    }

    /// <summary>
    ///     添加 jwt 授权处理器
    /// </summary>
    public static IServiceCollection AddJwt(this IServiceCollection me)
    {
        _ = me.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
        return me;
    }
}