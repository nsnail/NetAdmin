using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsg;

/// <summary>
///     响应：导出站内信
/// </summary>
public record ExportSiteMsgRsp : QuerySiteMsgRsp
{
    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(5)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.用户名))]
    public override string CreatedUserName { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryDeptRsp> Depts { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(2)]
    [CsvName(nameof(Ln.消息类型))]
    public override SiteMsgTypes MsgType { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(4)]
    [CsvName(nameof(Ln.消息摘要))]
    public override string Summary { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.消息主题))]
    public override string Title { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryUserRsp> Users { get; init; }
}