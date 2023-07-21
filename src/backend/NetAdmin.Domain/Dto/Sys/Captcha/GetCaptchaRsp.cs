namespace NetAdmin.Domain.Dto.Sys.Captcha;

/// <summary>
///     响应：获取人机校验图
/// </summary>
public sealed record GetCaptchaRsp : DataAbstraction
{
    /// <summary>
    ///     背景图（base64）
    /// </summary>
    public string BackgroundImage { get; init; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    ///     缺口x坐标
    /// </summary>
    [JsonIgnore]
    public int SawOffsetX { get; init; }

    /// <summary>
    ///     滑块图（base64）
    /// </summary>
    public string SliderImage { get; init; }
}