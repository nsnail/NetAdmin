namespace NetAdmin.Domain.Dto.Sys.SiteMsgDept;

/// <summary>
///     请求：查询站内信-部门映射
/// </summary>
public sealed record QuerySiteMsgDeptReq : Sys_SiteMsgDept
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}