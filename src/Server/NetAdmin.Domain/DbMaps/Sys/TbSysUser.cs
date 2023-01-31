using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;
using NSExt.Attributes;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户基本信息表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(UserName)}", nameof(UserName), true)]
[Index($"idx_{{tablename}}_{nameof(Mobile)}",   nameof(Mobile),   true)]
[Index($"idx_{{tablename}}_{nameof(Email)}",    nameof(Email),    true)]
public record TbSysUser : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     用户设置
    /// </summary>
    [Flags]
    public enum UserBits : long
    {
        /// <summary>
        ///     已激活
        /// </summary>
        [Description(nameof(Ln.Activated))]
        [Localization(typeof(Ln))]
        Activated = 0b_0000_0001_0000
    }

    /// <summary>
    ///     头像链接
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string Avatar { get; init; }

    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />）
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     所属部门
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(DeptId))]
    public virtual TbSysDept Dept { get; init; }

    /// <summary>
    ///     部门id
    /// </summary>
    [JsonIgnore]
    public virtual long DeptId { get; init; }

    /// <summary>
    ///     邮箱
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string Email { get; init; }

    /// <summary>
    ///     手机号
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    public virtual string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [JsonIgnore]
    public virtual Guid Password { get; set; }

    /// <summary>
    ///     所属岗位
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysUserPosition))]
    public virtual ICollection<TbSysPosition> Positions { get; init; }

    /// <summary>
    ///     用户档案
    /// </summary>
    [JsonIgnore]
    public virtual TbSysUserProfile Profile { get; init; }

    /// <summary>
    ///     所属角色
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysUserRole))]
    public virtual ICollection<TbSysRole> Roles { get; init; }

    /// <summary>
    ///     做授权验证的Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    [JsonIgnore]
    public virtual Guid Token { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string UserName { get; init; }
}