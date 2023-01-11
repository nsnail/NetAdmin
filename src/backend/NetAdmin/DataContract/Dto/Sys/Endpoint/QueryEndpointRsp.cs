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
    public string Method { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    ///     说明
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    ///     分类
    /// </summary>
    public string Type { get; set; }
}