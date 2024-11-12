namespace NetAdmin.SysComponent.Domain.Dto.Sys.Dept;

/// <summary>
///     请求：编辑部门
/// </summary>
public sealed record EditDeptReq : CreateDeptReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}