using NetAdmin.Domain;
using NetAdmin.Domain.Dto.Dependency;

namespace NetAdmin.Application.Extensions;

/// <summary>
///     FreeSql Select 扩展方法
/// </summary>
public static class ISelectExtensions
{
    /// <summary>
    ///     附加其他过滤条件
    /// </summary>
    public static ISelect<T> AppendOtherFilters<T, TQuery>(this ISelect<T> me, QueryReq<TQuery> req)
        where TQuery : DataAbstraction, new()
    {
        if (req.IgnoreOwner) {
            me = me.DisableGlobalFilter(Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT
                                      , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILD);
        }

        return me;
    }
}