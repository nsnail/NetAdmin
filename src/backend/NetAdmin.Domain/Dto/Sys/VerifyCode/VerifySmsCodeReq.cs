using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：核实短信验证码
/// </summary>
public record VerifySmsCodeReq : VerifyCodeReq
{
    /// <inheritdoc />
    [Mobile]
    public override string DestDevice { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public override VerifyCodeDeviceTypes DeviceType { get; init; } = VerifyCodeDeviceTypes.Mobile;
}