namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     模块类型
/// </summary>
[Export]
public enum ModuleTypes
{
    /// <summary>
    ///     系统模块
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.系统模块))]
    SysComponent = 1

   ,

    /// <summary>
    ///     业务模块
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.业务模块))]
    BizServer = 2
}