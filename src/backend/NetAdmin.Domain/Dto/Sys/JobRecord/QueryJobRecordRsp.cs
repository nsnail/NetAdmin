using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.Dto.Sys.JobRecord;

/// <summary>
///     响应：查询计划作业执行记录
/// </summary>
public sealed record QueryJobRecordRsp : Sys_JobRecord
{
    /// <inheritdoc cref="Sys_JobRecord.HttpStatusCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public new HttpStatusCode HttpStatusCode => (HttpStatusCode)base.HttpStatusCode;

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.Duration" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Duration { get; init; }

    /// <inheritdoc cref="Sys_JobRecord.HttpMethod" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

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