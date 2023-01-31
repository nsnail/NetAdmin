namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     人机验证配置
/// </summary>
public record CaptchaOptions : OptionAbstraction
{
    /// <summary>
    ///     图片路径（相对）
    /// </summary>
    public string ImageRelativePath { get; init; }

    /// <summary>
    ///     密钥
    /// </summary>
    public string SecretKey { get; init; }
}