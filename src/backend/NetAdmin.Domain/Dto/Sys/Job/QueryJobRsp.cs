using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     响应：查询计划作业
/// </summary>
public record QueryJobRsp : Sys_Job
{
    /// <inheritdoc cref="Sys_Job.LastStatusCode" />
    [JsonInclude]
    public new virtual string LastStatusCode =>
        #pragma warning disable IDE0072
        base.LastStatusCode switch {
            #pragma warning restore IDE0072
            null => null
          , _ => (int)base.LastStatusCode.Value == Numbers.HTTP_STATUS_BIZ_FAIL
                ? nameof(ErrorCodes.Unhandled).ToLowerCamelCase()
                : base.LastStatusCode.Value.ToString().ToLowerCamelCase()
        };

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? CreatedUserId { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserName { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="Sys_Job.ExecutionCron" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ExecutionCron { get; init; }

    /// <inheritdoc cref="Sys_Job.HttpMethod" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Job.JobName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string JobName { get; init; }

    /// <inheritdoc cref="Sys_Job.LastDuration" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? LastDuration { get; init; }

    /// <inheritdoc cref="Sys_Job.LastExecTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? LastExecTime { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? ModifiedTime { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? ModifiedUserId { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ModifiedUserName { get; init; }

    /// <inheritdoc cref="Sys_Job.NextExecTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? NextExecTime { get; init; }

    /// <inheritdoc cref="Sys_Job.NextTimeId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? NextTimeId { get; init; }

    /// <inheritdoc cref="Sys_Job.RequestBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestBody { get; init; }

    /// <inheritdoc cref="Sys_Job.RequestHeader" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestHeader { get; init; }

    /// <inheritdoc cref="Sys_Job.RequestUrl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestUrl { get; init; }

    /// <inheritdoc cref="Sys_Job.Status" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override JobStatues Status { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="Sys_Job.User" />
    public new virtual QueryUserRsp User { get; init; }

    /// <inheritdoc cref="Sys_Job.UserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserId { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}