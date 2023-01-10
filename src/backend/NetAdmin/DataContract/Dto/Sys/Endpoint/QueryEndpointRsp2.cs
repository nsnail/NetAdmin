namespace NetAdmin.DataContract.Dto.Sys.Endpoint;

/// <summary>
///     信息：端点
/// </summary>
public record QueryEndpointRsp2 : DataAbstraction
{
    /// <summary>
    ///     子节点
    /// </summary>
    public List<QueryEndpointRsp2> Children { get; set; }

    /// <summary>
    ///     说明
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    ///     路径
    /// </summary>
    public string Path { get; set; }
}