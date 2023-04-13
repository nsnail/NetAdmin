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
    [ResourceDescription<Ln>(nameof(Ln.System_module))]
    SysComponent = 1

   ,

    /// <summary>
    ///     管理模块
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Manage_module))]
    BizServer = 2

   ,

    /// <summary>
    ///     任务模块
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Task_module))]
    TskComponent = 3

   ,

    /// <summary>
    ///     资源模块
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Resource_module))]
    ResComponent = 4
}