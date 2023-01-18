using System.Text.Json.Serialization;

namespace NetAdmin.Domain.Dto.Sys.Position;

/// <summary>
///     请求：更新岗位
/// </summary>
public record UpdatePositionReq : CreatePositionReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long Version { get; init; }
}