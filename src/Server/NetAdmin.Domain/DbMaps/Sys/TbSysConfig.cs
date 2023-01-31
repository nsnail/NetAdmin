using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     配置表
/// </summary>
[Table]
public record TbSysConfig : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

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
    public virtual TbSysDept UserRegisterDept { get; init; }

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
    public virtual TbSysPosition UserRegisterPos { get; init; }

    /// <summary>
    ///     用户注册默认岗位id
    /// </summary>
    [JsonIgnore]
    public virtual long UserRegisterPosId { get; set; }

    /// <summary>
    ///     用户注册默认角色
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(UserRegisterRoleId))]
    public virtual TbSysRole UserRegisterRole { get; init; }

    /// <summary>
    ///     用户注册默认角色id
    /// </summary>
    [JsonIgnore]
    public virtual long UserRegisterRoleId { get; set; }
}