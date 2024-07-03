namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     请求：设置计划作业启用状态
/// </summary>
public sealed record SetJobEnabledReq : Sys_Job
{
    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}