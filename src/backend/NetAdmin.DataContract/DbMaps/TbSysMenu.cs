using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     菜单表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(Name)}", nameof(Name), true)]
public record TbSysMenu : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     子节点或详情页需要高亮的上级菜单路由地址
    /// </summary>
    [JsonIgnore]
    public virtual string Active { get; init; }

    /// <summary>
    ///     比特位（前4位是公共位<see cref="Enums.BitSets" />） <see cref="Enums.SysMenuBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual List<TbSysMenu> Children { get; init; }

    /// <summary>
    ///     背景颜色
    /// </summary>
    [JsonIgnore]
    public virtual string Color { get; init; }

    /// <summary>
    ///     组件
    /// </summary>
    [JsonIgnore]
    public virtual string Component { get; init; }

    /// <summary>
    ///     图标
    /// </summary>
    [JsonIgnore]
    public virtual string Icon { get; init; }

    /// <summary>
    ///     菜单名称
    /// </summary>
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; init; }

    /// <summary>
    ///     菜单路径
    /// </summary>
    [JsonIgnore]
    public virtual string Path { get; init; }

    /// <summary>
    ///     重定向地址
    /// </summary>
    [JsonIgnore]
    public virtual string Redirect { get; init; }

    /// <summary>
    ///     拥有此菜单的角色集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleMenu))]
    public virtual ICollection<TbSysRole> Roles { get; init; }

    /// <summary>
    ///     排序
    /// </summary>
    [JsonIgnore]
    public virtual int Sort { get; init; }

    /// <summary>
    ///     标签
    /// </summary>
    [JsonIgnore]
    public virtual string Tag { get; init; }

    /// <summary>
    ///     菜单标题
    /// </summary>
    [JsonIgnore]
    public virtual string Title { get; init; }

    /// <summary>
    ///     菜单类型
    /// </summary>
    [JsonIgnore]
    public virtual Enums.SysMenuTypes Type { get; init; }
}