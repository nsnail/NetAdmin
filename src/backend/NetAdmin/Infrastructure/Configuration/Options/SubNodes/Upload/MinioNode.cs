namespace NetAdmin.Infrastructure.Configuration.Options.SubNodes.Upload;

/// <summary>
///     Minio 节点
/// </summary>
public record MinioNode
{
    /// <summary>
    ///     访问令牌
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    ///     文件访问Url地址
    /// </summary>
    public string AccessUrl { get; set; }

    /// <summary>
    ///     使用的存储桶名称
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    ///     服务器地址
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    ///     安全码
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     是否启用ssl
    /// </summary>
    public bool Secure { get; set; }
}