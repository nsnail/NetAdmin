namespace NetAdmin.Domain.Dto.Sys.SiteMsgDept;

/// <summary>
///     响应：查询站内信-部门映射
/// </summary>
public sealed record QuerySiteMsgDeptRsp : Sys_SiteMsgDept
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}