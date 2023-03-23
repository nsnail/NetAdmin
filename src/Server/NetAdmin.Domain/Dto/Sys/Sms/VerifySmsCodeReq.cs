using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：核实短信验证码
/// </summary>
public record VerifySmsCodeReq : TbSysSms
{
    /// <inheritdoc />
    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Code { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string DestMobile { get; init; }
}