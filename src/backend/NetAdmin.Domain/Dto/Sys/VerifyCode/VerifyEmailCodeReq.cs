using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：核实邮箱验证码
/// </summary>
public record VerifyEmailCodeReq : VerifyCodeReq
{
    /// <inheritdoc />
    [Email]
    public override string DestDevice { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public override VerifyCodeDeviceTypes DeviceType { get; init; } = VerifyCodeDeviceTypes.Email;
}