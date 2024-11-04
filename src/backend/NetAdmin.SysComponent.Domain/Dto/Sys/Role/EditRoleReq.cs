namespace NetAdmin.SysComponent.Domain.Dto.Sys.Role;

/// <summary>
///     请求：编辑角色
/// </summary>
public sealed record EditRoleReq : CreateRoleReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}