using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     信息：元数据
/// </summary>
public sealed record MetaInfo : DataAbstraction
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MetaInfo" /> class.
    /// </summary>
    public MetaInfo() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MetaInfo" /> class.
    /// </summary>
    public MetaInfo(string color, bool      fullPage, bool hidden, bool hiddenBreadCrumb, string icon, string tag
                  , string title, MenuTypes type)
    {
        Color            = color;
        FullPage         = fullPage;
        Hidden           = hidden;
        HiddenBreadCrumb = hiddenBreadCrumb;
        Icon             = icon;
        Tag              = tag;
        Title            = title;
        Type             = type;
    }

    /// <summary>
    ///     背景颜色
    /// </summary>
    public string Color { get; init; }

    /// <summary>
    ///     是否整页路由
    /// </summary>
    public bool FullPage { get; init; }

    /// <summary>
    ///     是否隐藏
    /// </summary>
    public bool Hidden { get; init; }

    /// <summary>
    ///     是否隐藏面包屑
    /// </summary>
    public bool HiddenBreadCrumb { get; init; }

    /// <summary>
    ///     图标
    /// </summary>
    public string Icon { get; init; }

    /// <summary>
    ///     标签
    /// </summary>
    public string Tag { get; init; }

    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    ///     类型
    /// </summary>
    [EnumDataType(typeof(MenuTypes))]
    public MenuTypes Type { get; init; }
}