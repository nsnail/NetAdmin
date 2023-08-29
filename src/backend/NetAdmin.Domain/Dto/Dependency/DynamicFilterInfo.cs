using NetAdmin.Domain.Enums;

namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     动态过滤条件
/// </summary>
public record DynamicFilterInfo : DataAbstraction
{
    /// <summary>
    ///     字段名
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    ///     子过滤条件
    /// </summary>
    public List<DynamicFilterInfo> Filters { get; set; }

    /// <summary>
    ///     子过滤条件逻辑关系
    /// </summary>
    public DynamicFilterLogics Logic { get; set; }

    /// <summary>
    ///     操作符
    /// </summary>
    public DynamicFilterOperators Operator { get; set; }

    /// <summary>
    ///     值
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    ///     隐式转换为 FreeSql 的 DynamicFilterInfo 对象
    /// </summary>
    public static implicit operator FreeSql.Internal.Model.DynamicFilterInfo(DynamicFilterInfo d)
    {
        return d.Adapt<FreeSql.Internal.Model.DynamicFilterInfo>();
    }
}