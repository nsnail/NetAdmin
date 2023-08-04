using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户基本信息表
/// </summary>
[Table(Name = Chars.FLG_TABLE_NAME_PREFIX + nameof(Sys_User))]
[Index($"idx_{{tablename}}_{nameof(UserName)}", nameof(UserName), true)]
[Index($"idx_{{tablename}}_{nameof(Mobile)}",   nameof(Mobile),   true)]
[Index($"idx_{{tablename}}_{nameof(Email)}",    nameof(Email),    true)]
public record Sys_User : VersionEntity, IFieldSummary, IFieldEnabled, IRegister
{
    /// <summary>
    ///     头像链接
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string Avatar { get; init; }

    /// <summary>
    ///     所属部门
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(DeptId))]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     部门编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long DeptId { get; init; }

    /// <summary>
    ///     邮箱
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string Email { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     手机号
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    public virtual string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [JsonIgnore]
    [Column]
    public Guid Password { get; set; }

    /// <summary>
    ///     用户档案
    /// </summary>
    [JsonIgnore]
    public Sys_UserProfile Profile { get; init; }

    /// <summary>
    ///     所属角色
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_UserRole))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     授权验证Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    [JsonIgnore]
    [Column]
    public Guid Token { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string UserName { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateUserReq, Sys_User>()
                  .Map(d => d.Password, s => s.PasswordText.Pwd().Guid())
                  .Map(d => d.Token,    _ => Guid.NewGuid())
                  .Map(d => d.Enabled,  _ => true)
                  .Map( //
                      d => d.Roles
                    , s => s.RoleIds.NullOrEmpty()
                          ? Array.Empty<Sys_Role>()
                          : s.RoleIds.Select(x => new Sys_Role { Id = x }));

        _ = config.ForType<UpdateUserReq, Sys_User>()
                  .Map( //
                      d => d.Password, s => s.PasswordText.NullOrEmpty() ? Guid.Empty : s.PasswordText.Pwd().Guid())
                  .Map( //
                      d => d.Roles
                    , s => s.RoleIds.NullOrEmpty()
                          ? Array.Empty<Sys_Role>()
                          : s.RoleIds.Select(x => new Sys_Role { Id = x }));
    }
}