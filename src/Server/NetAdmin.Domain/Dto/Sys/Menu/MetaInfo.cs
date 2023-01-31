using System.ComponentModel.DataAnnotations;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     信息：元数据
/// </summary>
public record MetaInfo(string Icon,     string Title, TbSysMenu.MenuTypes Type, bool Hidden, bool HiddenBreadCrumb
                     , bool   FullPage, string Tag,   string              Color)
{
    /// <summary>
    ///     背景颜色
    /// </summary>
    public string Color { get; } = Color;

    /// <summary>
    ///     是否整页路由
    /// </summary>
    public bool FullPage { get; } = FullPage;

    /// <summary>
    ///     是否隐藏
    /// </summary>
    public bool Hidden { get; } = Hidden;

    /// <summary>
    ///     是否隐藏面包屑
    /// </summary>
    public bool HiddenBreadCrumb { get; } = HiddenBreadCrumb;

    /// <summary>
    ///     图标
    /// </summary>
    public string Icon { get; } = Icon;

    /// <summary>
    ///     标签
    /// </summary>
    public string Tag { get; } = Tag;

    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; } = Title;

    /// <summary>
    ///     类型
    /// </summary>
    [EnumDataType(typeof(TbSysMenu.MenuTypes))]
    public TbSysMenu.MenuTypes Type { get; init; } = Type;
}