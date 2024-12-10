namespace NetAdmin.Domain.Dto.Tpl.Example;

/// <summary>
///     请求：编辑示例
/// </summary>
public record EditExampleReq : CreateExampleReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}