namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     档案可见性
/// </summary>
[Export]
public enum ArchiveVisibilities
{
    /// <summary>
    ///     完全公开
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.完全公开))]
    Public = 1

    ,

    /// <summary>
    ///     登录用户
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.登录用户))]
    LogonUser = 2

    ,

    /// <summary>
    ///     部门可见
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.部门可见))]
    DeptUser = 3

    ,

    /// <summary>
    ///     自己可见
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.自己可见))]
    Self = 4
}