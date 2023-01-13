using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     角色表
/// </summary>
[Table]
[Index("idx_{tablename}_01", nameof(Label), true)]
public record TbSysRole : MutableEntity, IFieldBitSet, IFieldSort
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore]
    [Navigate]
    public ICollection<TbSysRoleApi> Apis { get; set; }

    /// <summary>
    ///     比特位（前4位是公共位<see cref="Enums.BitSets" />） <see cref="Enums.SysRoleBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     数据范围
    /// </summary>
    [JsonIgnore]
    public virtual Enums.DataScopes DataScope { get; init; }

    /// <summary>
    ///     角色-部门映射
    /// </summary>
    [Navigate(ManyToMany = typeof(TbSysRoleDept))]
    [JsonIgnore]
    public virtual ICollection<TbSysDept> Depts { get; init; }

    /// <summary>
    ///     角色名
    /// </summary>
    [JsonIgnore]
    public virtual string Label { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    [Navigate(ManyToMany = typeof(TbSysRoleMenu))]
    [JsonIgnore]
    public virtual ICollection<TbSysMenu> Menus { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    [JsonIgnore]
    public virtual string Remark { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual int Sort { get; init; }

    /// <summary>
    ///     此角色下的用户集合
    /// </summary>
    [Navigate(ManyToMany = typeof(TbSysUserRole))]
    [JsonIgnore]
    public ICollection<TbSysUser> Users { get; init; }
}