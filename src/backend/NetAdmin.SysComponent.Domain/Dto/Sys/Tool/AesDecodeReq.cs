namespace NetAdmin.SysComponent.Domain.Dto.Sys.Tool;

/// <summary>
///     请求：Aes解密
/// </summary>
public record AesDecodeReq : DataAbstraction
{
    /// <summary>
    ///     密文
    /// </summary>
    public string CipherText { get; init; }
}