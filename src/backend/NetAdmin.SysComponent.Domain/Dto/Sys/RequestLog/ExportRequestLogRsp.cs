using NetAdmin.SysComponent.Domain.Dto.Sys.Api;
using NetAdmin.SysComponent.Domain.Dto.Sys.RequestLogDetail;
using NetAdmin.SysComponent.Domain.Dto.Sys.User;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.RequestLog;

/// <summary>
///     响应：导出请求日志
/// </summary>
public sealed record ExportRequestLogRsp : QueryRequestLogRsp
{
    /// <summary>
    ///     接口路径
    /// </summary>
    [CsvIndex(2)]
    [JsonInclude]
    [CsvName(nameof(Ln.接口路径))]
    public string ApiId => Api.Id;

    /// <inheritdoc />
    [CsvIndex(6)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.客户端IP))]
    public override string CreatedClientIp => base.CreatedClientIp;

    /// <summary>
    ///     用户名
    /// </summary>
    [CsvIndex(5)]
    [JsonInclude]
    [CsvName(nameof(Ln.用户名))]
    public string UserName => Owner?.UserName;

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryApiRsp Api { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(8)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryRequestLogDetailRsp Detail { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.执行耗时))]
    public override int Duration { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.请求方式))]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.响应状态码))]
    public override int HttpStatusCode { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryUserRsp Owner { get; init; }

    /// <inheritdoc />
    [CsvIndex(7)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.跟踪标识))]
    public override Guid TraceId { get; init; }
}