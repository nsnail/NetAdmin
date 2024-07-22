using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.RequestLogDetail;

/// <summary>
///     响应：查询请求日志明细
/// </summary>
public sealed record QueryRequestLogDetailRsp : Sys_RequestLogDetail
{
    /// <summary>
    ///     登录名
    /// </summary>
    [JsonInclude]
    public string LoginName => RequestBody?.ToObject<LoginByPwdReq>()?.Account;

    /// <summary>
    ///     操作系统
    /// </summary>
    [JsonInclude]
    public string Os => UserAgentParser.Create(CreatedUserAgent)?.Platform;

    /// <inheritdoc cref="IFieldCreatedClientUserAgent.CreatedUserAgent" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserAgent { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.ErrorCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override ErrorCodes ErrorCode { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.Exception" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Exception { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.RequestBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestBody { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.RequestContentType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestContentType { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.RequestHeaders" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestHeaders { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.RequestUrl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RequestUrl { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.ResponseBody" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseBody { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.ResponseContentType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseContentType { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.ResponseHeaders" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ResponseHeaders { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.ServerIp" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override int? ServerIp { get; init; }

    /// <inheritdoc cref="Sys_RequestLogDetail.TraceId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string TraceId { get; init; }
}