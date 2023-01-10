using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.Menu;

/// <summary>
///     信息：菜单
/// </summary>
public record QueryMenuRsp : TbSysMenu, IRegister
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(Enums.SysMenuBits.Enabled);

    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta =>
        new(Icon, Title, Type, BitSet.HasFlag(Enums.SysMenuBits.Hidden)
          , BitSet.HasFlag(Enums.SysMenuBits.HiddenBreadCrumb), BitSet.HasFlag(Enums.SysMenuBits.FullPageRouting), Tag
          , Color);

    /// <inheritdoc cref="TbSysMenu.Active" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Active { get; set; }

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public new List<QueryMenuRsp> Children { get; set; }

    /// <inheritdoc cref="TbSysMenu.Component" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Component { get; set; }

    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="TbSysMenu.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Name { get; set; }

    /// <inheritdoc cref="TbSysMenu.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; set; }

    /// <inheritdoc cref="TbSysMenu.Path" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Path { get; set; }

    /// <inheritdoc cref="TbSysMenu.Redirect" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Redirect { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; set; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TbSysMenu, QueryMenuRsp>().Map(dest => dest.Path, src => src.Path ?? string.Empty);
    }
}