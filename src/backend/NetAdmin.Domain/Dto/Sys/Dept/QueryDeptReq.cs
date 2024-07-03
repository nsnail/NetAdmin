namespace NetAdmin.Domain.Dto.Sys.Dept;

/// <summary>
///     请求：查询部门
/// </summary>
public sealed record QueryDeptReq : Sys_Dept
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}