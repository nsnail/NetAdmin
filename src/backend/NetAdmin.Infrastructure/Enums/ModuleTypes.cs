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
    ///     管理模块
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.管理模块))]
    AdmServer = 2
}