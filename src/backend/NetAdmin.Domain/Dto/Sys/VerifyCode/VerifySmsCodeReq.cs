using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：核实短信验证码
/// </summary>
public record VerifySmsCodeReq : VerifyCodeReq
{
    /// <inheritdoc cref="Sys_VerifyCode.DestDevice" />
    [Mobile]
    public override string DestDevice { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.DeviceType" />
    [JsonIgnore]
    public override VerifyCodeDeviceTypes DeviceType { get; init; } = VerifyCodeDeviceTypes.Mobile;
}