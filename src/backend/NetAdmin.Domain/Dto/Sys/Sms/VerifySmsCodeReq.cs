using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：核实短信验证码
/// </summary>
public record VerifySmsCodeReq : SmsCodeInfo
{
    /// <inheritdoc />
    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Code { get; init; }
}