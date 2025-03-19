namespace NetAdmin.Domain.Dto.Sys.File;

/// <summary>
///     响应：文件上传
/// </summary>
public record UploadFileRsp : DataAbstraction
{
    /// <summary>
    ///     文件名
    /// </summary>
    public string FileName { get; init; }

    /// <summary>
    ///     可访问的url地址
    /// </summary>
    public string Url { get; init; }
}