using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.Domain.Dto.Sys.Email;

/// <summary>
///     请求：发送邮箱验证码
/// </summary>
public sealed record SendEmailCodeReq : EmailCodeInfo
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.电子邮箱))]
    public override string EmailAddress { get; init; }

    /// <summary>
    ///     人机校验请求
    /// </summary>
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.人机校验请求))]
    public VerifyCaptchaReq VerifyCaptchaReq { get; init; }
}