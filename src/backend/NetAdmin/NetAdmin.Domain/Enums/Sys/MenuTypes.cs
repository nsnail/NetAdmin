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
    [ResourceDescription<Ln>(nameof(Ln.菜单))]
    Menu = 1

    ,

    /// <summary>
    ///     链接
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.链接))]
    Link = 2

    ,

    /// <summary>
    ///     框架
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.框架))]
    Iframe = 3

    ,

    /// <summary>
    ///     按钮
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.按钮))]
    Button = 4
}