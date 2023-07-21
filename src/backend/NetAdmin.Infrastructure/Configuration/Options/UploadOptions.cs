namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     上传配置
/// </summary>
public sealed record UploadOptions : OptionAbstraction
{
    /// <summary>
    ///     允许的文件类型
    /// </summary>
    public IReadOnlyCollection<string> ContentTypes { get; init; }

    /// <summary>
    ///     允许的文件大小（字节）
    /// </summary>
    public long MaxSize { get; init; }

    /// <summary>
    ///     Minio 配置
    /// </summary>
    public MinioNode Minio { get; init; }
}