using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     信息：菜单
/// </summary>
public sealed record QueryMenuRsp : Sys_Menu, IRegister
{
    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta => new(Color, FullPageRouting, Hidden, HiddenBreadCrumb, Icon, Tag, Title, Type);

    /// <inheritdoc cref="Sys_Menu.Active" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Active { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    public new IEnumerable<QueryMenuRsp> Children { get; init; }

    /// <inheritdoc cref="Sys_Menu.Component" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Component { get; init; }

    /// <inheritdoc cref="Sys_Menu.FullPageRouting" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool FullPageRouting { get; init; }

    /// <inheritdoc cref="Sys_Menu.Hidden" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Hidden { get; init; }

    /// <inheritdoc cref="Sys_Menu.HiddenBreadCrumb" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool HiddenBreadCrumb { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Menu.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="Sys_Menu.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; }

    /// <inheritdoc cref="Sys_Menu.Path" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Path { get; init; }

    /// <inheritdoc cref="Sys_Menu.Redirect" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Redirect { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }

    /// <inheritdoc cref="IRegister.Register" />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_Menu, QueryMenuRsp>().Map(dest => dest.Path, src => src.Path ?? string.Empty);
    }
}