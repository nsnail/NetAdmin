using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.RequestLog;

/// <summary>
///     响应：导出请求日志
/// </summary>
public record ExportRequestLogRsp : QueryRequestLogRsp
{
    /// <inheritdoc />
    [CsvIndex(6)]
    [Ignore(false)]
    [Name(nameof(Ln.客户端IP))]
    public override string CreatedClientIp => base.CreatedClientIp;

    /// <inheritdoc />
    [Ignore]
    public override string LoginName => base.LoginName;

    /// <inheritdoc />
    [CsvIndex(7)]
    [Ignore(false)]
    [Name(nameof(Ln.操作系统))]
    public override string Os => base.Os;

    /// <inheritdoc />
    [CsvIndex(2)]
    [Ignore(false)]
    [Name(nameof(Ln.接口路径))]
    public override string ApiId { get; init; }

    /// <inheritdoc />
    [CsvIndex(8)]
    [Ignore(false)]
    [Name(nameof(Ln.用户代理))]
    public override string CreatedUserAgent { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [Ignore(false)]
    [Name(nameof(Ln.执行耗时))]
    public override long Duration { get; init; }

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
    [CsvIndex(3)]
    [Ignore(false)]
    [Name(nameof(Ln.请求方式))]
    public override string Method { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override QueryUserRsp User { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [CsvIndex(5)]
    [Ignore(false)]
    [Name(nameof(Ln.用户名))]
    public string UserName { get; init; }

    /// <inheritdoc />
    public override void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_RequestLog, ExportRequestLogRsp>().Map(d => d.UserName, s => s.User.UserName);
    }
}