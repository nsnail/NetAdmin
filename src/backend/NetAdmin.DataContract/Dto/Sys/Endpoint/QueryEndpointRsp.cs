namespace NetAdmin.DataContract.Dto.Sys.Endpoint;

/// <summary>
///     信息：端点
/// </summary>
public record QueryEndpointRsp : DataAbstraction
{
    /// <summary>
    ///     子节点
    /// </summary>
    public IEnumerable<QueryEndpointRsp> Children { get; init; }

    /// <summary>
    ///     请求方式
    /// </summary>
    public string Method { get; init; }

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     路径
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    ///     说明
    /// </summary>
    public string Summary { get; init; }

    /// <summary>
    ///     分类
    /// </summary>
    public string Type { get; init; }
}