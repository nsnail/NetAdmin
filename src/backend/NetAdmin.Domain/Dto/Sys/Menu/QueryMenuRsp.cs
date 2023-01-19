using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     信息：菜单
/// </summary>
public record QueryMenuRsp : TbSysMenu, IRegister
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta =>
        new(Icon, Title, Type, BitSet.HasFlag(MenuBits.Hidden), BitSet.HasFlag(MenuBits.HiddenBreadCrumb)
          , BitSet.HasFlag(MenuBits.FullPageRouting), Tag, Color);

    /// <inheritdoc cref="TbSysMenu.Active" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Active { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    public new List<QueryMenuRsp> Children { get; init; }

    /// <inheritdoc cref="TbSysMenu.Component" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Component { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long Id { get; set; }

    /// <inheritdoc cref="TbSysMenu.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Name { get; init; }

    /// <inheritdoc cref="TbSysMenu.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long ParentId { get; init; }

    /// <inheritdoc cref="TbSysMenu.Path" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Path { get; init; }

    /// <inheritdoc cref="TbSysMenu.Redirect" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Redirect { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TbSysMenu, QueryMenuRsp>().Map(dest => dest.Path, src => src.Path ?? string.Empty);
    }
}