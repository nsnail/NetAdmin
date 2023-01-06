using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     菜单表
/// </summary>
[Table]
public record TbSysMenu : DefaultEntity, IFieldBitSet
{
    /// <summary>
    ///     比特位 <see cref="Enums.SysMenuBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; set; }

    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual List<TbSysMenu> Children { get; set; }

    /// <summary>
    ///     组件
    /// </summary>
    [JsonIgnore]
    public virtual string Component { get; set; }

    /// <summary>
    ///     图标
    /// </summary>
    [JsonIgnore]
    public virtual string Icon { get; set; }

    /// <summary>
    ///     菜单名称
    /// </summary>
    [JsonIgnore]
    public virtual string Name { get; set; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual long ParentId { get; set; }

    /// <summary>
    ///     菜单路径
    /// </summary>
    [JsonIgnore]
    public virtual string Path { get; set; }

    /// <summary>
    ///     排序
    /// </summary>
    [JsonIgnore]
    public virtual int Sort { get; set; }

    /// <summary>
    ///     菜单标题
    /// </summary>
    [JsonIgnore]
    public virtual string Title { get; set; }

    /// <summary>
    ///     菜单类型
    /// </summary>
    [JsonIgnore]
    public virtual Enums.MenuTypes Type { get; set; }
}