using NetAdmin.Domain.Dto.Sys.Job;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.Dto.Sys.JobRecord;

/// <summary>
///     响应：查询计划作业执行记录
/// </summary>
public record QueryJobRecordRsp : Sys_JobRecord
{
    /// <inheritdoc cref="Sys_JobRecord.HttpStatusCode" />
    public new virtual string HttpStatusCode =>
        base.HttpStatusCode == Numbers.HTTP_STATUS_BIZ_FAIL
            ? nameof(ErrorCodes.Unhandled).ToLowerCamelCase()
            : ((HttpStatusCode)base.HttpStatusCode).ToString().ToLowerCamelCase();

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.Duration" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Duration { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.HttpMethod" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <summary>
    ///     作业信息
    /// </summary>
    public new virtual QueryJobRsp Job { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.JobId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long JobId { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.RequestBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestBody { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.RequestHeader" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestHeader { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.RequestUrl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestUrl { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.ResponseBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseBody { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.ResponseHeader" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseHeader { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.TimeId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long TimeId { get; init; }
}