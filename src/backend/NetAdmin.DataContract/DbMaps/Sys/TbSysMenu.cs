using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NSExt.Attributes;

namespace NetAdmin.DataContract.DbMaps.Sys;

/// <summary>
///     菜单表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(Name)}", nameof(Name), true)]
public record TbSysMenu : MutableEntity, IFieldBitSet
{
    /// <summary>
    ///     菜单表比特位
    /// </summary>
    [Flags]
    public enum MenuBits : long
    {
        /// <summary>
        ///     隐藏
        /// </summary>
        [Description(nameof(Str.Hidden))]
        [Localization(typeof(Str))]
        Hidden = 0b_0000_0001_0000

       ,

        /// <summary>
        ///     隐藏面包屑
        /// </summary>
        [Description(nameof(Str.Hidden_bread_crumb))]
        [Localization(typeof(Str))]
        HiddenBreadCrumb = 0b_0000_0010_0000

       ,

        /// <summary>
        ///     整页路由
        /// </summary>
        [Description(nameof(Str.Full_page_routing))]
        [Localization(typeof(Str))]
        FullPageRouting = 0b_0000_0100_0000
    }

    /// <summary>
    ///     菜单类型
    /// </summary>
    public enum MenuTypes
    {
        /// <summary>
        ///     菜单
        /// </summary>
        [Description(nameof(Str.Menu))]
        [Localization(typeof(Str))]
        Menu = 1

       ,

        /// <summary>
        ///     链接
        /// </summary>
        [Description(nameof(Str.Link))]
        [Localization(typeof(Str))]
        Link = 2

       ,

        /// <summary>
        ///     框架
        /// </summary>
        [Description(nameof(Str.Iframe))]
        [Localization(typeof(Str))]
        Iframe = 3

       ,

        /// <summary>
        ///     按钮
        /// </summary>
        [Description(nameof(Str.Button))]
        [Localization(typeof(Str))]
        Button = 4
    }

    /// <summary>
    ///     子节点或详情页需要高亮的上级菜单路由地址
    /// </summary>
    [JsonIgnore]
    public virtual string Active { get; init; }

    /// <summary>
    ///     比特位（前4位是公共位<see cref="EntityBase.BitSets" />） <see cref="MenuBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual IEnumerable<TbSysMenu> Children { get; init; }

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
    public virtual MenuTypes Type { get; init; }
}