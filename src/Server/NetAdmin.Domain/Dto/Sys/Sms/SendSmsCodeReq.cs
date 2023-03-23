using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：发送短信验证码
/// </summary>
public record SendSmsCodeReq : TbSysSms
{
    /// <inheritdoc />
    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string DestMobile { get; init; }

    /// <inheritdoc />
    public override Statues Status => Statues.Wating;

    /// <inheritdoc />
    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Types Type { get; init; }

    /// <summary>
    ///     人机校验请求
    /// </summary>
    [Required]
    public VerifyCaptchaReq VerifyCaptchaReq { get; init; }
}