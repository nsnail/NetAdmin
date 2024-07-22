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
    [JsonInclude]
    [Name(nameof(Ln.接口路径))]
    public string ApiId => Api.Id;

    /// <inheritdoc />
    [CsvIndex(6)]
    [Ignore(false)]
    [Name(nameof(Ln.客户端IP))]
    public override string CreatedClientIp => base.CreatedClientIp;

    /// <summary>
    ///     用户名
    /// </summary>
    [CsvIndex(5)]
    [JsonInclude]
    [Name(nameof(Ln.用户名))]
    public string UserName => Owner?.UserName;

    /// <inheritdoc />
    [Ignore]
    public override QueryApiRsp Api { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override QueryRequestLogDetailRsp Detail { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [Ignore(false)]
    [Name(nameof(Ln.执行耗时))]
    public override int Duration { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [Ignore(false)]
    [Name(nameof(Ln.请求方式))]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [Ignore(false)]
    [Name(nameof(Ln.响应状态码))]
    public override int HttpStatusCode { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [Ignore(false)]
    [Name(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override QueryUserLiteRsp Owner { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override long? OwnerId { get; init; }
}