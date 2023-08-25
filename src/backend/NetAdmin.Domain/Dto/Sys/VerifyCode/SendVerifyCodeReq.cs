using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：发送验证码
/// </summary>
public sealed record SendVerifyCodeReq : Sys_VerifyCode, IValidatableObject
{
    /// <inheritdoc cref="Sys_VerifyCode.DestDevice" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.目标设备))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string DestDevice { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.DeviceType" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.设备类型))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(VerifyCodeDeviceTypes))]
    public override VerifyCodeDeviceTypes DeviceType { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.Status" />
    public override VerifyCodeStatues Status => VerifyCodeStatues.Waiting;

    /// <inheritdoc cref="Sys_VerifyCode.Type" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码类型))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(VerifyCodeTypes))]
    public override VerifyCodeTypes Type { get; init; }

    /// <summary>
    ///     人机校验请求
    /// </summary>
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.人机校验请求))]
    public VerifyCaptchaReq VerifyCaptchaReq { get; init; }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ValidationResult validationResult;
        switch (DeviceType) {
            case VerifyCodeDeviceTypes.Email:
                validationResult = new EmailAttribute().GetValidationResult(DestDevice, validationContext);
                if (validationResult == null) {
                    yield break;
                }

                yield return new ValidationResult(validationResult.ErrorMessage, new[] { nameof(DestDevice) });
                break;
            case VerifyCodeDeviceTypes.Mobile:
                validationResult = new MobileAttribute().GetValidationResult(DestDevice, validationContext);
                if (validationResult == null) {
                    yield break;
                }

                yield return new ValidationResult(validationResult.ErrorMessage, new[] { nameof(DestDevice) });
                break;
            default:
                yield break;
        }
    }
}