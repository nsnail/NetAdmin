using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     请求：更新计划作业
/// </summary>
public sealed record UpdateJobReq : CreateJobReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}