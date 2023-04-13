using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     配置表
/// </summary>
[Table(Name = nameof(Sys_Config))]
public record Sys_Config : VersionEntity, IFieldEnabled
{
    /// <inheritdoc />
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     用户注册是否需要人工确认
    /// </summary>
    [JsonIgnore]
    public virtual bool UserRegisterConfirm { get; set; }

    /// <summary>
    ///     用户注册默认部门
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(UserRegisterDeptId))]
    public Sys_Dept UserRegisterDept { get; init; }

    /// <summary>
    ///     用户注册默认部门id
    /// </summary>
    [JsonIgnore]
    public virtual long UserRegisterDeptId { get; init; }

    /// <summary>
    ///     用户注册默认岗位
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(UserRegisterPosId))]
    public Sys_Position UserRegisterPos { get; init; }

    /// <summary>
    ///     用户注册默认岗位id
    /// </summary>
    [JsonIgnore]
    public virtual long UserRegisterPosId { get; init; }

    /// <summary>
    ///     用户注册默认角色
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(UserRegisterRoleId))]
    public Sys_Role UserRegisterRole { get; init; }

    /// <summary>
    ///     用户注册默认角色id
    /// </summary>
    [JsonIgnore]
    public virtual long UserRegisterRoleId { get; init; }
}