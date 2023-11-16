using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsg;

/// <summary>
///     响应：查询站内信
/// </summary>
public sealed record QuerySiteMsgRsp : Sys_SiteMsg
{
    /// <inheritdoc cref="Sys_SiteMsg.Content" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Content { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <summary>
    ///     创建者
    /// </summary>
    public QueryUserRsp Creator { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.Depts" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new IEnumerable<QueryDeptRsp> Depts { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? ModifiedTime { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.MsgType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override SiteMsgTypes MsgType { get; init; }

    /// <summary>
    ///     我的标记
    /// </summary>
    public QuerySiteMsgFlagRsp MyFlags { get; set; }

    /// <inheritdoc cref="Sys_SiteMsg.Roles" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.Title" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Title { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.Users" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new IEnumerable<QueryUserRsp> Users { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}