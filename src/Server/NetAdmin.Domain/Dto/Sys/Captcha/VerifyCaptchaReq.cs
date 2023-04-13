namespace NetAdmin.Domain.Dto.Sys.Captcha;

/// <summary>
///     请求：完成人机验证
/// </summary>
public sealed record VerifyCaptchaReq : DataAbstraction
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Required]
    public string Id { get; init; }

    /// <summary>
    ///     缺口x坐标
    /// </summary>
    [JsonIgnore]
    public int? SawOffsetX { get; init; }

    /// <summary>
    ///     验证数据
    /// </summary>
    [Required]
    public string VerifyData { get; init; }
}