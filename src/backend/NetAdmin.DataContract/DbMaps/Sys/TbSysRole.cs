using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Attributes;
using NSExt.Attributes;

namespace NetAdmin.DataContract.DbMaps.Sys;

/// <summary>
///     角色表
/// </summary>
[Table]
[Index("idx_{tablename}_01", nameof(Label), true)]
public record TbSysRole : MutableEntity, IFieldBitSet, IFieldSort
{
    /// <summary>
    ///     角色数据范围
    /// </summary>
    [Export]
    public enum DataScopes
    {
        /// <summary>
        ///     全部
        /// </summary>
        [Description(nameof(Str.All))]
        [Localization(typeof(Str))]
        All = 1

       ,

        /// <summary>
        ///     本部门和下级部门
        /// </summary>
        [Description(nameof(Str.This_department_and_subordinate_departments))]
        [Localization(typeof(Str))]
        DeptWithChild = 2

       ,

        /// <summary>
        ///     本部门
        /// </summary>
        [Description(nameof(Str.Department_data))]
        [Localization(typeof(Str))]
        Dept = 3

       ,

        /// <summary>
        ///     本人数据
        /// </summary>
        [Description(nameof(Str.Personal_data))]
        [Localization(typeof(Str))]
        Self = 4

       ,

        /// <summary>
        ///     指定部门
        /// </summary>
        [Description(nameof(Str.Designated_department))]
        [Localization(typeof(Str))]
        SpecificDept = 5
    }

    /// <summary>
    ///     角色设置
    /// </summary>
    [Flags]
    public enum RoleBits : long
    {
        /// <summary>
        ///     忽略权限控制（拥有所有权限）
        /// </summary>
        [Description(nameof(Str.Ignoring_permissions_control))]
        [Localization(typeof(Str))]
        IgnorePermissionControl = 0b_0000_0001_0000
    }

    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleApi))]
    public ICollection<TbSysApi> Apis { get; set; }

    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />） <see cref="RoleBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     数据范围
    /// </summary>
    [JsonIgnore]
    public virtual DataScopes DataScope { get; init; }

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