using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户基本信息表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(Email),      nameof(Email),      true)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(Mobile),     nameof(Mobile),     true)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(InviteCode), nameof(InviteCode), true)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(UserName),   nameof(UserName),   true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_User))]
public record Sys_User : VersionEntity, IFieldSummary, IFieldEnabled, IRegister
{
    /// <summary>
    ///     头像链接
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Avatar { get; init; }

    /// <summary>
    ///     归属部门
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(DeptId))]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     部门编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long DeptId { get; init; }

    /// <summary>
    ///     邮箱
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Email { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     用户邀请
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    public Sys_UserInvite Invite { get; init; }

    /// <summary>
    ///     邀请码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_CHAR_6)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string InviteCode { get; init; }

    /// <summary>
    ///     最后登录时间
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual DateTime? LastLoginTime { get; init; }

    /// <summary>
    ///     手机号码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [Column]
    [CsvIgnore]
    [DangerField]
    [JsonIgnore]
    public Guid Password { get; init; }

    /// <summary>
    ///     用户档案
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    public Sys_UserProfile Profile { get; init; }

    /// <summary>
    ///     归属角色
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_UserRole))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     发送给此用户的站内信集合
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgUser))]
    public IReadOnlyCollection<Sys_SiteMsg> SiteMsgs { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     授权验证Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public Guid Token { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string UserName { get; init; }

    /// <inheritdoc />
    public virtual void Register(TypeAdapterConfig config)
    {
        #pragma warning disable RCS1196

        // ReSharper disable InvokeAsExtensionMethod
        _ = config.ForType<CreateUserReq, Sys_User>()
                  .Map(d => d.Password, s => s.PasswordText.Pwd().Guid())
                  .Map(d => d.Token,    _ => Guid.NewGuid())
                  .Map( //
                      d => d.Roles
                    , s => s.RoleIds.NullOrEmpty() ? Array.Empty<Sys_Role>() : Enumerable.Select(s.RoleIds, x => new Sys_Role { Id = x }));

        _ = config.ForType<EditUserReq, Sys_User>()
                  .Map( //
                      d => d.Password, s => s.PasswordText.NullOrEmpty() ? Guid.Empty : s.PasswordText.Pwd().Guid())
                  .Map( //
                      d => d.Roles
                    , s => s.RoleIds.NullOrEmpty() ? Array.Empty<Sys_Role>() : Enumerable.Select(s.RoleIds, x => new Sys_Role { Id = x }));

        // ReSharper restore InvokeAsExtensionMethod
        #pragma warning restore RCS1196
    }
}