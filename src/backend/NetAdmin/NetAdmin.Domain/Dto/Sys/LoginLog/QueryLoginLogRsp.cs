using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.LoginLog;

/// <summary>
///     响应：查询登录日志
/// </summary>
public record QueryLoginLogRsp : Sys_LoginLog
{
    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    [JsonInclude]
    public new virtual string CreatedClientIp => base.CreatedClientIp?.ToIpV4();

    /// <summary>
    ///     操作系统
    /// </summary>
    [JsonInclude]
    public virtual string Os => UserAgentParser.Create(CreatedUserAgent)?.Platform;

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldCreatedClientUserAgent.CreatedUserAgent" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserAgent { get; init; }

    /// <inheritdoc cref="Sys_LoginLog.Duration" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Duration { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.ErrorCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override ErrorCodes ErrorCode { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.HttpStatusCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int HttpStatusCode { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_LoginLog.LoginUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string LoginUserName { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.Owner" />
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="Sys_LoginLog.RequestBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestBody { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.RequestHeaders" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestHeaders { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.RequestUrl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestUrl { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.ResponseBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseBody { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.ResponseHeaders" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseHeaders { get; protected init; }

    /// <inheritdoc cref="Sys_LoginLog.ServerIp" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override int? ServerIp { get; protected init; }
}