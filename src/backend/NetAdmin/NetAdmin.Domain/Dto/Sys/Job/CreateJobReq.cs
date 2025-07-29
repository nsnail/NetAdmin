using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     请求：创建计划作业
/// </summary>
public record CreateJobReq : Sys_Job
{
    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; } = true;

    /// <inheritdoc cref="Sys_Job.ExecutionCron" />
    [Cron]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.时间计划不能为空))]
    public override string ExecutionCron { get; init; }

    /// <inheritdoc cref="Sys_Job.HttpMethod" />
    [EnumDataType(typeof(HttpMethods), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.请求方法不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc cref="Sys_Job.JobName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.作业名称不能为空))]
    public override string JobName { get; init; }

    /// <inheritdoc />
    public override long? NextTimeId { get; init; }

    /// <inheritdoc />
    [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.随机延时起始时间不正确))]
    public override int? RandomDelayBegin { get; init; }

    /// <inheritdoc />
    [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.随机延时结束时间不正确))]
    public override int? RandomDelayEnd { get; init; }

    /// <inheritdoc cref="Sys_Job.RequestBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestBody { get; init; }

    /// <summary>
    ///     请求头
    /// </summary>
    public Dictionary<string, string> RequestHeaders { get; init; }

    /// <inheritdoc cref="Sys_Job.RequestUrl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.请求地址不能为空))]
    [Url(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.网络地址不正确))]
    public override string RequestUrl { get; init; }

    /// <inheritdoc />
    public override JobStatues Status { get; init; } = JobStatues.Idle;

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; set; }

    /// <inheritdoc cref="Sys_Job.UserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Range(1, long.MaxValue, ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户编号不存在))]
    [UserId]
    public override long UserId { get; init; }
}