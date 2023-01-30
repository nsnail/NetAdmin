using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Infrastructure.Attributes;
using NSExt.Attributes;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     菜单表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(Name)}", nameof(Name), true)]
public record TbSysMenu : MutableEntity, IFieldBitSet, IFieldSort
{
    /// <summary>
    ///     菜单设置
    /// </summary>
    [Flags]
    public enum MenuBits : long
    {
        /// <summary>
        ///     隐藏
        /// </summary>
        [Description(nameof(Ln.Hidden))]
        [Localization(typeof(Ln))]
        Hidden = 0b_0000_0001_0000

       ,

        /// <summary>
        ///     隐藏面包屑
        /// </summary>
        [Description(nameof(Ln.Hidden_bread_crumb))]
        [Localization(typeof(Ln))]
        HiddenBreadCrumb = 0b_0000_0010_0000

       ,

        /// <summary>
        ///     整页路由
        /// </summary>
        [Description(nameof(Ln.Full_page_routing))]
        [Localization(typeof(Ln))]
        FullPageRouting = 0b_0000_0100_0000
    }

    /// <summary>
    ///     菜单类型
    /// </summary>
    [Export]
    public enum MenuTypes
    {
        /// <summary>
        ///     菜单
        /// </summary>
        [Description(nameof(Ln.Menu))]
        [Localization(typeof(Ln))]
        Menu = 1

       ,

        /// <summary>
        ///     链接
        /// </summary>
        [Description(nameof(Ln.Link))]
        [Localization(typeof(Ln))]
        Link = 2

       ,

        /// <summary>
        ///     框架
        /// </summary>
        [Description(nameof(Ln.Iframe))]
        [Localization(typeof(Ln))]
        Iframe = 3

       ,

        /// <summary>
        ///     按钮
        /// </summary>
        [Description(nameof(Ln.Button))]
        [Localization(typeof(Ln))]
        Button = 4
    }

    /// <summary>
    ///     子节点或详情页需要高亮的上级菜单路由地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string Active { get; init; }

    /// <summary>
    ///     设置（前4位是公共位<see cref="EntityBase.BitSets" />） <see cref="MenuBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public virtual IEnumerable<TbSysMenu> Children { get; init; }

    /// <summary>
    ///     背景颜色
    /// </summary>
    [JsonIgnore]
    [MaxLength(7)]
    public virtual string Color { get; init; }

    /// <summary>
    ///     组件
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string Component { get; init; }

    /// <summary>
    ///     图标
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Icon { get; init; }

    /// <summary>
    ///     菜单名称
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
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
    [MaxLength(127)]
    public virtual string Path { get; init; }

    /// <summary>
    ///     重定向地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string Redirect { get; init; }

    /// <summary>
    ///     拥有此菜单的角色集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleMenu))]
    public virtual ICollection<TbSysRole> Roles { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <summary>
    ///     标签
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Tag { get; init; }

    /// <summary>
    ///     菜单标题
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string Title { get; init; }

    /// <summary>
    ///     菜单类型
    /// </summary>
    [JsonIgnore]
    public virtual MenuTypes Type { get; init; }
}