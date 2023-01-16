using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps.Sys;

/// <summary>
///     用户表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(UserName)}", nameof(UserName), true)]
[Index($"idx_{{tablename}}_{nameof(Mobile)}",   nameof(Mobile),   true)]
public record TbSysUser : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     头像链接
    /// </summary>
    [JsonIgnore]
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
    ///     手机号
    /// </summary>
    [JsonIgnore]
    public virtual string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [JsonIgnore]
    public virtual Guid Password { get; init; }

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
    public virtual string UserName { get; init; }
}