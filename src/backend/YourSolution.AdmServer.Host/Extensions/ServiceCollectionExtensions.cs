using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.SysComponent.Host.Extensions;
using YourSolution.AdmServer.Host.Filters;

namespace YourSolution.AdmServer.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     添加 FreeSql
    /// </summary>
    public static IServiceCollection AddFreeSqlWithArgs(this IServiceCollection me) {
        return me.AddFreeSql(
            (Startup.Args.SyncStructure ? FreeSqlInitMethods.SyncStructure : FreeSqlInitMethods.None)
            | (Startup.Args.InsertSeedData ? FreeSqlInitMethods.InsertSeedData : FreeSqlInitMethods.None), freeSql =>
            {
                // 数据权限过滤器
                // 本人
                _ = freeSql.GlobalFilter.ApplyOnlyIf<IFieldOwner>(
                    Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, () => ContextUserInfo.Create()?.Roles.All(x => x.DataScope == DataScopes.Self) ?? false
                    , a => a.OwnerId == ContextUserInfo.Create().Id
                );

                // 本部门
                _ = freeSql.GlobalFilter.ApplyOnlyIf<IFieldOwner>(
                    Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT, () => ContextUserInfo.Create()?.Roles.All(x => x.DataScope == DataScopes.Dept) ?? false
                    , a => ContextUserInfo.Create().DeptId == a.OwnerDeptId
                );

                // 本部门和子部门
                _ = freeSql.GlobalFilter.ApplyOnlyIf<IFieldOwner>(
                    Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN
                    , () => ContextUserInfo.Create()?.Roles.All(x => x.DataScope == DataScopes.DeptWithChildren) ?? false
                    , a => ContextUserInfo.Create().AllDeptIds.Contains(a.OwnerDeptId)
                );

                // 本部门和下一级部门
                _ = freeSql.GlobalFilter.ApplyOnlyIf<IFieldOwner>(
                    Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON
                    , () => ContextUserInfo.Create()?.Roles.All(x => x.DataScope == DataScopes.DeptWithSon) ?? false
                    , a => ContextUserInfo.Create().SonDeptIds.Contains(a.OwnerDeptId)
                );
            }
        );
    }

    /// <summary>
    ///     添加 jwt 授权处理器
    /// </summary>
    public static IServiceCollection AddJwt(this IServiceCollection me) {
        _ = me.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
        return me;
    }
}