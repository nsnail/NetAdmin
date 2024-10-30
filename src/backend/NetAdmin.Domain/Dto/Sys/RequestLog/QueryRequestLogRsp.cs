using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.RequestLogDetail;
using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.RequestLog;

/// <summary>
///     响应：查询请求日志
/// </summary>
public record QueryRequestLogRsp : Sys_RequestLog
{
    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    [JsonInclude]
    public new virtual string CreatedClientIp => base.CreatedClientIp?.ToIpV4();

    /// <inheritdoc cref="Sys_RequestLog.Api" />
    public new virtual QueryApiRsp Api { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.ApiPathCrc32" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int ApiPathCrc32 { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.Detail" />
    public new virtual QueryRequestLogDetailRsp Detail { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.Duration" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Duration { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.HttpMethod" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.HttpStatusCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int HttpStatusCode { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.Owner" />
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.TraceId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Guid TraceId { get; init; }
}