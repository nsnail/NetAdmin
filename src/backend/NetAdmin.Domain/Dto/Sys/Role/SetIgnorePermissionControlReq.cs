using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：设置是否忽略权限控制
/// </summary>
public sealed record SetIgnorePermissionControlReq : Sys_Role
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Role.IgnorePermissionControl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool IgnorePermissionControl { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}