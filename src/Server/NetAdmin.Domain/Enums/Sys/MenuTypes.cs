namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     菜单类型
/// </summary>
[Export]
public enum MenuTypes
{
    /// <summary>
    ///     菜单
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Menu))]
    Menu = 1

   ,

    /// <summary>
    ///     链接
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Link))]
    Link = 2

   ,

    /// <summary>
    ///     框架
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Iframe))]
    Iframe = 3

   ,

    /// <summary>
    ///     按钮
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Button))]
    Button = 4
}