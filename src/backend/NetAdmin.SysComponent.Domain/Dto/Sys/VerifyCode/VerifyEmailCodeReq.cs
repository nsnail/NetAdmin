namespace NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：核实邮箱验证码
/// </summary>
public record VerifyEmailCodeReq : VerifyCodeReq
{
    /// <inheritdoc cref="Sys_VerifyCode.DestDevice" />
    [Email]
    public override string DestDevice { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.DeviceType" />
    [JsonIgnore]
    public override VerifyCodeDeviceTypes DeviceType { get; init; } = VerifyCodeDeviceTypes.Email;
}