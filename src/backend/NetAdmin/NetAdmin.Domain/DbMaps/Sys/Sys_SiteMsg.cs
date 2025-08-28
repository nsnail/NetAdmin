using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     站内信表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_SiteMsg))]
public record Sys_SiteMsg : VersionEntity, IRegister, IFieldSummary
{
    /// <summary>
    ///     消息内容
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [JsonIgnore]
    public virtual string Content { get; init; }

    /// <summary>
    ///     消息-创建者映射
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(CreatedUserId))]
    public Sys_User Creator { get; init; }

    /// <summary>
    ///     消息-部门映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgDept))]
    public IReadOnlyCollection<Sys_Dept> Depts { get; init; }

    /// <summary>
    ///     消息-标记映射
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(Sys_SiteMsgFlag.SiteMsgId))]
    public IReadOnlyCollection<Sys_SiteMsgFlag> Flags { get; init; }

    /// <summary>
    ///     消息类型
    /// </summary>
    [JsonIgnore]
    public virtual SiteMsgTypes MsgType { get; init; }

    /// <summary>
    ///     消息-角色映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgRole))]
    public IReadOnlyCollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     消息摘要
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string Summary { get; set; }

    /// <summary>
    ///     消息主题
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string Title { get; init; }

    /// <summary>
    ///     消息-用户映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgUser))]
    public IReadOnlyCollection<Sys_User> Users { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config) {
        #pragma warning disable RCS1196

        // ReSharper disable InvokeAsExtensionMethod
        _ = config
            .ForType<CreateSiteMsgReq, Sys_SiteMsg>()
            .Map(d => d.Summary, s => s.Content.RemoveHtmlTag().HtmlDe().Sub(0, 100))
            .Map(d => d.Roles, s => s.RoleIds.NullOrEmpty() ? Array.Empty<Sys_Role>() : Enumerable.Select(s.RoleIds, x => new Sys_Role { Id = x }))
            .Map(d => d.Users, s => s.UserIds.NullOrEmpty() ? Array.Empty<Sys_User>() : Enumerable.Select(s.UserIds, x => new Sys_User { Id = x }))
            .Map(d => d.Depts, s => s.DeptIds.NullOrEmpty() ? Array.Empty<Sys_Dept>() : Enumerable.Select(s.DeptIds, x => new Sys_Dept { Id = x }));

        // ReSharper restore InvokeAsExtensionMethod
        #pragma warning restore RCS1196
    }
}