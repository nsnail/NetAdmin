using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户基本信息表
/// </summary>
[Table(Name = nameof(Sys_User))]
[Index($"idx_{{tablename}}_{nameof(UserName)}", nameof(UserName), true)]
[Index($"idx_{{tablename}}_{nameof(Mobile)}",   nameof(Mobile),   true)]
[Index($"idx_{{tablename}}_{nameof(Email)}",    nameof(Email),    true)]
public record Sys_User : VersionEntity, IFieldEnabled
{
    /// <summary>
    ///     头像链接
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR127)]
    public virtual string Avatar { get; init; }

    /// <summary>
    ///     所属部门
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(DeptId))]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     部门id
    /// </summary>
    [JsonIgnore]
    public virtual long DeptId { get; init; }

    /// <summary>
    ///     邮箱
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR63)]
    public virtual string Email { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     手机号
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR15)]
    public virtual string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [JsonIgnore]
    public Guid Password { get; set; }

    /// <summary>
    ///     所属岗位
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_UserPosition))]
    public ICollection<Sys_Position> Positions { get; init; }

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
    ///     做授权验证的Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    [JsonIgnore]
    public Guid Token { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR31)]
    public virtual string UserName { get; init; }
}