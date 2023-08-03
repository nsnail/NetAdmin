using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     配置表
/// </summary>
[Table(Name = Chars.FLG_TABLE_NAME_PREFIX + nameof(Sys_Config))]
public record Sys_Config : VersionEntity, IFieldEnabled
{
    /// <summary>
    ///     是否启用
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     用户注册是否需要人工确认
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual bool UserRegisterConfirm { get; set; }

    /// <summary>
    ///     用户注册默认部门
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(UserRegisterDeptId))]
    public Sys_Dept UserRegisterDept { get; init; }

    /// <summary>
    ///     用户注册默认部门编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long UserRegisterDeptId { get; init; }

    /// <summary>
    ///     用户注册默认角色
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(UserRegisterRoleId))]
    public Sys_Role UserRegisterRole { get; init; }

    /// <summary>
    ///     用户注册默认角色编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long UserRegisterRoleId { get; init; }
}