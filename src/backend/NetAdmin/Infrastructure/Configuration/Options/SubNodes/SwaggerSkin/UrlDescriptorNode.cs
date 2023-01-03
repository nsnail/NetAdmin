namespace NetAdmin.Infrastructure.Configuration.Options.SubNodes.SwaggerSkin;

/// <summary>
///     url描述符 配置节点
/// </summary>
public record UrlDescriptorNode
{
    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     url
    /// </summary>
    public string Url { get; set; }
}