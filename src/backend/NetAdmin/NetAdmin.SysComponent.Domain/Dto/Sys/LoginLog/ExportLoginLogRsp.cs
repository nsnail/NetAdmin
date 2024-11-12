using NetAdmin.SysComponent.Domain.Dto.Sys.User;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.LoginLog;

/// <summary>
///     响应：导出登录日志
/// </summary>
public sealed record ExportLoginLogRsp : QueryLoginLogRsp
{
    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.客户端IP))]
    public override string CreatedClientIp => base.CreatedClientIp;

    /// <inheritdoc />
    [CsvIndex(4)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.操作系统))]
    public override string Os => base.Os;

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(6)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(5)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.用户代理))]
    public override string CreatedUserAgent { get; init; }

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
    [CsvIgnore(false)]
    [CsvIndex(2)]
    [CsvName(nameof(Ln.登录名))]
    public override string LoginUserName { get; protected init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryUserRsp Owner { get; init; }
}