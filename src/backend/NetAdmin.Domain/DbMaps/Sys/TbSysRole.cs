using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Infrastructure.Attributes;
using NSExt.Attributes;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色表
/// </summary>
[Table]
[Index("idx_{tablename}_01", nameof(Name), true)]
public record TbSysRole : MutableEntity, IFieldBitSet, IFieldSort, IFieldSummary
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
        [Description(nameof(Ln.All))]
        [Localization(typeof(Ln))]
        All = 1

       ,

        /// <summary>
        ///     本部门和下级部门
        /// </summary>
        [Description(nameof(Ln.This_department_and_subordinate_departments))]
        [Localization(typeof(Ln))]
        DeptWithChild = 2

       ,

        /// <summary>
        ///     本部门
        /// </summary>
        [Description(nameof(Ln.Department_data))]
        [Localization(typeof(Ln))]
        Dept = 3

       ,

        /// <summary>
        ///     本人数据
        /// </summary>
        [Description(nameof(Ln.Personal_data))]
        [Localization(typeof(Ln))]
        Self = 4

       ,

        /// <summary>
        ///     指定部门
        /// </summary>
        [Description(nameof(Ln.Designated_department))]
        [Localization(typeof(Ln))]
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
        [Description(nameof(Ln.Ignoring_permissions_control))]
        [Localization(typeof(Ln))]
        IgnorePermissionControl = 0b_0000_0001_0000

       ,

        /// <summary>
        ///     是否显示仪表板
        /// </summary>
        [Description(nameof(Ln.Display_Dashboard))]
        [Localization(typeof(Ln))]
        DisplayDashboard = 0b_0000_0010_0000
    }

    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleApi))]
    public virtual ICollection<TbSysApi> Apis { get; init; }

    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />） <see cref="RoleBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     数据范围
    /// </summary>
    [JsonIgnore]
    [EnumDataType(typeof(DataScopes))]
    public virtual DataScopes DataScope { get; init; } = DataScopes.All;

    /// <summary>
    ///     角色-部门映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleDept))]
    public virtual ICollection<TbSysDept> Depts { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleMenu))]
    public virtual ICollection<TbSysMenu> Menus { get; init; }

    /// <summary>
    ///     角色名
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Name { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <summary>
    ///     备注
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     此角色下的用户集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysUserRole))]
    public virtual ICollection<TbSysUser> Users { get; init; }
}