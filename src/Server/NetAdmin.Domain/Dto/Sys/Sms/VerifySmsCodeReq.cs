using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：核实短信验证码
/// </summary>
public record VerifySmsCodeReq : Sys_Sms
{
    /// <inheritdoc cref="Sys_Sms.Code" />
    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Code { get; init; }

    /// <inheritdoc cref="Sys_Sms.DestMobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string DestMobile { get; init; }
}