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
    [CsvIndex(5)]
    [Ignore(false)]
    [Name(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [Ignore(false)]
    [Name(nameof(Ln.用户名))]
    public override string CreatedUserName { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override IEnumerable<QueryDeptRsp> Depts { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [Ignore(false)]
    [Name(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [Ignore(false)]
    [Name(nameof(Ln.消息类型))]
    public override SiteMsgTypes MsgType { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [Ignore(false)]
    [Name(nameof(Ln.消息摘要))]
    public override string Summary { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [Ignore(false)]
    [Name(nameof(Ln.消息主题))]
    public override string Title { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override IEnumerable<QueryUserRsp> Users { get; init; }
}