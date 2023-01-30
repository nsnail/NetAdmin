using System.ComponentModel.DataAnnotations;
using FreeSql.Internal.Model;

namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     请求：查询
/// </summary>
public record QueryReq<T> : DataAbstraction
    where T : DataAbstraction, new()
{
    /// <summary>
    ///     取前n条
    /// </summary>
    [Range(1, Numbers.QUERY_LIMIT)]
    public int Count { get; init; } = Numbers.QUERY_LIMIT;

    /// <summary>
    ///     动态查询条件
    /// </summary>
    public DynamicFilterInfo DynamicFilter { get; init; }

    /// <summary>
    ///     查询条件
    /// </summary>
    public T Filter { get; init; }

    /// <summary>
    ///     排序方式
    /// </summary>
    public Enums.Orders? Order { get; init; }

    /// <summary>
    ///     排序字段
    /// </summary>
    public string Prop { get; init; }
}