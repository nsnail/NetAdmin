using System.Text.Json.Serialization;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     请求：更新短信
/// </summary>
public record UpdateSmsReq : CreateSmsReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}