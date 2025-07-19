using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：发送验证码
/// </summary>
public sealed record SendVerifyCodeReq : Sys_VerifyCode
{
    /// <inheritdoc cref="Sys_VerifyCode.DestDevice" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.目标设备不能为空))]
    public override string DestDevice { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.DeviceType" />
    [EnumDataType(typeof(VerifyCodeDeviceTypes), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码目标设备类型不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.设备类型不能为空))]
    public override VerifyCodeDeviceTypes DeviceType { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.Status" />
    public override VerifyCodeStatues Status => VerifyCodeStatues.Waiting;

    /// <inheritdoc cref="Sys_VerifyCode.Type" />
    [EnumDataType(typeof(VerifyCodeTypes), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码类型不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码类型不能为空))]
    public override VerifyCodeTypes Type { get; init; }

    /// <summary>
    ///     人机校验请求
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.人机校验请求不能为空))]
    public VerifyCaptchaReq VerifyCaptchaReq { get; init; }

    /// <inheritdoc />
    protected override IEnumerable<ValidationResult> ValidateInternal(ValidationContext validationContext)
    {
        ValidationResult validationResult;
        switch (DeviceType) {
            case VerifyCodeDeviceTypes.Email:
                validationResult = new EmailAttribute().GetValidationResult(DestDevice, validationContext);

                break;
            case VerifyCodeDeviceTypes.Mobile:
                validationResult = new MobileAttribute().GetValidationResult(DestDevice, validationContext);

                break;
            default:
                yield break;
        }

        if (validationResult == null) {
            yield break;
        }

        yield return new ValidationResult(validationResult.ErrorMessage, [nameof(DestDevice)]);
    }
}