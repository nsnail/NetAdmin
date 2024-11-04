using NetAdmin.SysComponent.Domain.Dto.Sys.SiteMsg;

namespace NetAdmin.SysComponent.Domain.DbMaps.Sys;

/// <summary>
///     站内信表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_SiteMsg))]
public record Sys_SiteMsg : VersionEntity, IRegister, IFieldSummary
{
    /// <summary>
    ///     消息内容
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Content { get; init; }

    /// <summary>
    ///     消息-创建者映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(CreatedUserId))]
    public Sys_User Creator { get; init; }

    /// <summary>
    ///     消息-部门映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgDept))]
    public ICollection<Sys_Dept> Depts { get; init; }

    /// <summary>
    ///     消息-标记映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(Sys_SiteMsgFlag.SiteMsgId))]
    public ICollection<Sys_SiteMsgFlag> Flags { get; init; }

    /// <summary>
    ///     消息类型
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    public virtual SiteMsgTypes MsgType { get; init; }

    /// <summary>
    ///     消息-角色映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgRole))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     消息摘要
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     消息主题
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Title { get; init; }

    /// <summary>
    ///     消息-用户映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgUser))]
    public ICollection<Sys_User> Users { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateSiteMsgReq, Sys_SiteMsg>()
                  .Map( //
                      d => d.Summary, s => s.Content.RemoveHtmlTag().HtmlDe().Sub(0, 100))
                  .Map( //
                      d => d.Roles, s => s.RoleIds.NullOrEmpty() ? Array.Empty<Sys_Role>() : s.RoleIds.Select(x => new Sys_Role { Id = x }))
                  .Map( //
                      d => d.Users, s => s.UserIds.NullOrEmpty() ? Array.Empty<Sys_User>() : s.UserIds.Select(x => new Sys_User { Id = x }))
                  .Map( //
                      d => d.Depts, s => s.DeptIds.NullOrEmpty() ? Array.Empty<Sys_Dept>() : s.DeptIds.Select(x => new Sys_Dept { Id = x }))

            //
            ;
    }
}