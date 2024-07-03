using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.RequestLog;

/// <summary>
///     响应：查询请求日志
/// </summary>
public record QueryRequestLogRsp : Sys_RequestLog, IRegister
{
    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    public new virtual string CreatedClientIp => base.CreatedClientIp?.ToIpV4();

    /// <summary>
    ///     登录名
    /// </summary>
    public virtual string LoginName => RequestBody?.ToObject<LoginByPwdReq>()?.Account;

    /// <summary>
    ///     操作系统
    /// </summary>
    public virtual string Os => UserAgentParser.Create(CreatedUserAgent)?.Platform;

    /// <inheritdoc cref="Sys_RequestLog.ApiId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ApiId { get; init; }

    /// <summary>
    ///     接口描述
    /// </summary>
    public string ApiSummary { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.CreatedUserAgent" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserAgent { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.Duration" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Duration { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.ErrorCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override ErrorCodes ErrorCode { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.Exception" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Exception { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.HttpStatusCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int HttpStatusCode { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.Method" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Method { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.RequestBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestBody { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.RequestContentType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestContentType { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.RequestHeaders" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestHeaders { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.RequestUrl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestUrl { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.ResponseBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseBody { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.ResponseContentType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseContentType { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.ResponseHeaders" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseHeaders { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.ServerIp" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override int? ServerIp { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.User" />
    public new virtual QueryUserRsp User { get; init; }

    /// <inheritdoc cref="Sys_RequestLog.UserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? UserId { get; init; }

    /// <inheritdoc />
    public virtual void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_RequestLog, QueryRequestLogRsp>().Map(d => d.ApiSummary, s => s.Api.Summary);
    }
}