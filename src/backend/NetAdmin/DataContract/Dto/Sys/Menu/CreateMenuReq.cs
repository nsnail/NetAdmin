using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Menu;

/// <summary>
///     请求：创建菜单
/// </summary>
public record CreateMenuReq : TbSysMenu
{
    /// <inheritdoc cref="TbSysMenu.Active" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Active { get; set; }

    /// <inheritdoc cref="TbSysMenu.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)Enums.SysMenuBits.Enabled;
            }

            if (Meta.HiddenBreadCrumb) {
                ret |= (long)Enums.SysMenuBits.HiddenBreadCrumb;
            }

            if (Meta.Hidden) {
                ret |= (long)Enums.SysMenuBits.Hidden;
            }

            if (Meta.FullPage) {
                ret |= (long)Enums.SysMenuBits.FullPageRouting;
            }

            return ret;
        }
    }

    /// <inheritdoc cref="TbSysMenu.Color" />
    public override string Color => Meta.Color;

    /// <inheritdoc cref="TbSysMenu.Component" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Component { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <inheritdoc cref="TbSysMenu.Icon" />
    public override string Icon => Meta.Icon;

    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta { get; set; }

    /// <inheritdoc cref="TbSysMenu.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Name { get; set; }

    /// <inheritdoc cref="TbSysMenu.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; set; } = 0;

    /// <inheritdoc cref="TbSysMenu.Path" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Path { get; set; }

    /// <inheritdoc cref="TbSysMenu.Redirect" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Redirect { get; set; }

    /// <inheritdoc cref="TbSysMenu.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Sort { get; set; }

    /// <inheritdoc cref="TbSysMenu.Tag" />
    public override string Tag => Meta.Tag;

    /// <inheritdoc cref="TbSysMenu.Title" />
    [Required]
    public override string Title => Meta.Title;

    /// <inheritdoc cref="TbSysMenu.Type" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(Enums.SysMenuTypes))]
    public override Enums.SysMenuTypes Type { get; set; } = Enums.SysMenuTypes.Menu;
}