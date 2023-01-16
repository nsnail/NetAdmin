using System.Text.Json.Serialization;

namespace NetAdmin.DataContract.Dto.Tpl.Example;

/// <summary>
///     请求：更新示例
/// </summary>
public record UpdateExampleReq : CreateExampleReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}