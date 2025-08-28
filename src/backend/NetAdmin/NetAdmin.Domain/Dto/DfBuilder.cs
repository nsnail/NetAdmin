using NetAdmin.Domain.Enums;

namespace NetAdmin.Domain.Dto;

/// <summary>
///     动态过滤条件生成器
/// </summary>
public sealed record DfBuilder
{
    /// <summary>
    ///     构建生成器
    /// </summary>
    public static DynamicFilterInfo New(DynamicFilterLogics logic) {
        return new DynamicFilterInfo { Logic = logic, Filters = [] };
    }
}