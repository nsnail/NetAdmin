namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     请求：编辑计划作业
/// </summary>
public sealed record EditJobReq : CreateJobReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}