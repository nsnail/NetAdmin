using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：发送短信验证码
/// </summary>
public sealed record SendSmsCodeReq : Sys_Sms
{
    /// <inheritdoc cref="Sys_Sms.DestMobile" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.目标手机))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string DestMobile { get; init; }

    /// <inheritdoc cref="Sys_Sms.Status" />
    public override SmsStatues Status => SmsStatues.Waiting;

    /// <inheritdoc cref="Sys_Sms.Type" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.短信类型))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override SmsTypes Type { get; init; }

    /// <summary>
    ///     人机校验请求
    /// </summary>
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.人机校验请求))]
    public VerifyCaptchaReq VerifyCaptchaReq { get; init; }
}