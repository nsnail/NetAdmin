using NetAdmin.Infrastructure.Configuration.Options.SubNodes.Upload;

namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     上传配置
/// </summary>
public record UploadOptions : OptionAbstraction
{
    /// <summary>
    ///     允许的文件类型
    /// </summary>
    public List<string> ContentTypes { get; set; }

    /// <summary>
    ///     允许的文件大小（字节）
    /// </summary>
    public long MaxSize { get; set; }

    /// <summary>
    ///     Minio 配置
    /// </summary>
    public MinioNode Minio { get; set; }
}