namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：更新菜单
/// </summary>
public sealed record EditMenuReq : CreateMenuReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}