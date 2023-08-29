namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     日志等级
/// </summary>
public enum LogLevels
{
    /// <summary>
    ///     跟踪
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.跟踪))]
    [Display(Name = "[gray]TCE[/]", ShortName = "TCE")]
    Trace = 0

   ,

    /// <summary>
    ///     调试
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.调试))]
    [Display(Name = "[gray]DBG[/]", ShortName = "DBG")]
    Debug = 1

   ,

    /// <summary>
    ///     信息
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.信息))]
    [Display(Name = "[green]INF[/]", ShortName = "INF")]
    Information = 2

   ,

    /// <summary>
    ///     警告
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.警告))]
    [Display(Name = "[yellow]WRN[/]", ShortName = "WRN")]
    Warning = 3

   ,

    /// <summary>
    ///     错误
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.错误))]
    [Display(Name = "[red]ERR[/]", ShortName = "ERR")]
    Error = 4

   ,

    /// <summary>
    ///     宕机
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.宕机))]
    [Display(Name = "[red]CTL[/]", ShortName = "CTL")]
    Critical = 5
}