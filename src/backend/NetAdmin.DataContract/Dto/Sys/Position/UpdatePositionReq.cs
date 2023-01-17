using System.Text.Json.Serialization;

namespace NetAdmin.DataContract.Dto.Sys.Position;

/// <summary>
///     请求：更新岗位
/// </summary>
public record UpdatePositionReq : CreatePositionReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}