using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.JobRecord;

/// <summary>
///     请求：查询计划作业执行记录
/// </summary>
public sealed record QueryJobRecordReq : Sys_JobRecord
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}