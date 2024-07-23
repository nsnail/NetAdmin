using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.RequestLogDetail;
using NetAdmin.Domain.Dto.Sys.User;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.Dto.Sys.RequestLog;

/// <summary>
///     响应：导出请求日志
/// </summary>
public record ExportRequestLogRsp : QueryRequestLogRsp
{
    /// <summary>
    ///     接口路径
    /// </summary>
    [CsvIndex(2)]
    [CsvName(nameof(Ln.接口路径))]
    [JsonInclude]
    public string ApiId => Api.Id;

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(6)]
    [CsvName(nameof(Ln.客户端IP))]
    public override string CreatedClientIp => base.CreatedClientIp;

    /// <summary>
    ///     用户名
    /// </summary>
    [CsvIndex(5)]
    [CsvName(nameof(Ln.用户名))]
    [JsonInclude]
    public string UserName => Owner?.UserName;

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryApiRsp Api { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryRequestLogDetailRsp Detail { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(4)]
    [CsvName(nameof(Ln.执行耗时))]
    public override int Duration { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.请求方式))]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.响应状态码))]
    public override int HttpStatusCode { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryUserLiteRsp Owner { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override long? OwnerId { get; init; }
}