using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：创建菜单
/// </summary>
public record CreateMenuReq : TbSysMenu
{
    /// <inheritdoc cref="TbSysMenu.Active" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Active { get; init; }

    /// <inheritdoc cref="TbSysMenu.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)BitSets.Enabled;
            }

            if (Meta.HiddenBreadCrumb) {
                ret |= (long)MenuBits.HiddenBreadCrumb;
            }

            if (Meta.Hidden) {
                ret |= (long)MenuBits.Hidden;
            }

            if (Meta.FullPage) {
                ret |= (long)MenuBits.FullPageRouting;
            }

            return ret;
        }
    }

    /// <inheritdoc cref="TbSysMenu.Color" />
    public override string Color => Meta.Color;

    /// <inheritdoc cref="TbSysMenu.Component" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Component { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <inheritdoc cref="TbSysMenu.Icon" />
    public override string Icon => Meta.Icon;

    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta { get; init; }

    /// <inheritdoc cref="TbSysMenu.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Required]
    public override string Name { get; init; }

    /// <inheritdoc cref="TbSysMenu.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long ParentId { get; init; } = 0;

    /// <inheritdoc cref="TbSysMenu.Path" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Path { get; init; }

    /// <inheritdoc cref="TbSysMenu.Redirect" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Redirect { get; init; }

    /// <inheritdoc cref="TbSysMenu.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override int Sort { get; init; }

    /// <inheritdoc cref="TbSysMenu.Tag" />
    public override string Tag => Meta.Tag;

    /// <inheritdoc cref="TbSysMenu.Title" />
    [Required]
    public override string Title => Meta.Title;

    /// <inheritdoc cref="TbSysMenu.Type" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [EnumDataType(typeof(MenuTypes))]
    public override MenuTypes Type => Meta.Type;
}