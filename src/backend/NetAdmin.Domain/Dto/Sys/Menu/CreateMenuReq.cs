namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：创建菜单
/// </summary>
public record CreateMenuReq : Sys_Menu
{
    /// <inheritdoc cref="Sys_Menu.Active" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Active { get; init; }

    /// <inheritdoc cref="Sys_Menu.Color" />
    public override string Color => Meta.Color;

    /// <inheritdoc cref="Sys_Menu.Component" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Component { get; init; }

    /// <inheritdoc cref="Sys_Menu.FullPageRouting" />
    public override bool FullPageRouting => Meta.FullPage;

    /// <inheritdoc cref="Sys_Menu.Hidden" />
    public override bool Hidden => Meta.Hidden;

    /// <inheritdoc cref="Sys_Menu.HiddenBreadCrumb" />
    public override bool HiddenBreadCrumb => Meta.HiddenBreadCrumb;

    /// <inheritdoc cref="Sys_Menu.Icon" />
    public override string Icon => Meta.Icon;

    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta { get; init; }

    /// <inheritdoc cref="Sys_Menu.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.菜单名称不能为空))]
    public override string Name { get; init; }

    /// <inheritdoc cref="Sys_Menu.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; } = 0;

    /// <inheritdoc cref="Sys_Menu.Path" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Path { get; init; }

    /// <inheritdoc cref="Sys_Menu.Redirect" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Redirect { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <inheritdoc cref="Sys_Menu.Tag" />
    public override string Tag => Meta.Tag;

    /// <inheritdoc cref="Sys_Menu.Title" />
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.菜单标题不能为空))]
    public override string Title => Meta.Title;

    /// <inheritdoc cref="Sys_Menu.Type" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override MenuTypes Type => Meta.Type;
}