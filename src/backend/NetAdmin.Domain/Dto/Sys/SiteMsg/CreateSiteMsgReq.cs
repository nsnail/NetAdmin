using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsg;

/// <summary>
///     请求：创建站内信
/// </summary>
public record CreateSiteMsgReq : Sys_SiteMsg
{
    /// <inheritdoc cref="Sys_SiteMsg.Content" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.消息内容不能为空))]
    public override string Content { get; init; }

    /// <summary>
    ///     部门编号列表
    /// </summary>
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> DeptIds { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.MsgType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(SiteMsgTypes), ErrorMessageResourceType = typeof(Ln)
                , ErrorMessageResourceName = nameof(Ln.站内信类型不正确))]
    public override SiteMsgTypes MsgType { get; init; }

    /// <summary>
    ///     角色编号列表
    /// </summary>
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="Sys_SiteMsg.Title" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.消息主题不能为空))]
    public override string Title { get; init; }

    /// <summary>
    ///     用户编号列表
    /// </summary>
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> UserIds { get; init; }
}