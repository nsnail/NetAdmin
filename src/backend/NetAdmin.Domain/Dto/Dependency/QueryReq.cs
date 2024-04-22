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
    [Range(1, Numbers.MAX_LIMIT_QUERY)]
    public int Count { get; init; } = Numbers.MAX_LIMIT_QUERY;

    /// <summary>
    ///     动态查询条件
    /// </summary>
    public DynamicFilterInfo DynamicFilter { get; init; }

    /// <summary>
    ///     查询条件
    /// </summary>
    public T Filter { get; init; }

    /// <summary>
    ///     查询关键字
    /// </summary>
    public string Keywords { get; init; }

    /// <summary>
    ///     排序方式
    /// </summary>
    public Orders? Order { get; init; }

    /// <summary>
    ///     排序字段
    /// </summary>
    public string Prop { get; init; }
}