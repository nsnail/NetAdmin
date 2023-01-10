using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Menu;

/// <summary>
///     信息：元数据
/// </summary>
public record MetaInfo(string Icon,     string Title, Enums.SysMenuTypes Type, bool Hidden, bool HiddenBreadCrumb
                     , bool   FullPage, string Tag,   string             Color)
{
    /// <summary>
    ///     背景颜色
    /// </summary>
    public string Color { get; init; } = Color;

    /// <summary>
    ///     是否整页路由
    /// </summary>
    public bool FullPage { get; init; } = FullPage;

    /// <summary>
    ///     是否隐藏
    /// </summary>
    public bool Hidden { get; set; } = Hidden;

    /// <summary>
    ///     是否隐藏面包屑
    /// </summary>
    public bool HiddenBreadCrumb { get; init; } = HiddenBreadCrumb;

    /// <summary>
    ///     图标
    /// </summary>
    public string Icon { get; set; } = Icon;

    /// <summary>
    ///     标签
    /// </summary>
    public string Tag { get; set; } = Tag;

    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; } = Title;

    /// <summary>
    ///     类型
    /// </summary>
    public Enums.SysMenuTypes Type { get; set; } = Type;
}