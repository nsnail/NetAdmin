namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     日志等级
/// </summary>
[Export]
public enum LogLevels
{
    /// <summary>
    ///     追踪
    /// </summary>
    [Display(Name = "[gray]TCE[/]", ShortName = "TCE")]
    [ResourceDescription<Ln>(nameof(Ln.追踪))]
    Trace = 0

    ,

    /// <summary>
    ///     调试
    /// </summary>
    [Display(Name = "[gray]DBG[/]", ShortName = "DBG")]
    [ResourceDescription<Ln>(nameof(Ln.调试))]
    Debug = 1

    ,

    /// <summary>
    ///     信息
    /// </summary>
    [Display(Name = "[green]INF[/]", ShortName = "INF")]
    [ResourceDescription<Ln>(nameof(Ln.信息))]
    Information = 2

    ,

    /// <summary>
    ///     警告
    /// </summary>
    [Display(Name = "[yellow]WRN[/]", ShortName = "WRN")]
    [ResourceDescription<Ln>(nameof(Ln.警告))]
    Warning = 3

    ,

    /// <summary>
    ///     错误
    /// </summary>
    [Display(Name = "[red]ERR[/]", ShortName = "ERR")]
    [ResourceDescription<Ln>(nameof(Ln.错误))]
    Error = 4

    ,

    /// <summary>
    ///     宕机
    /// </summary>
    [Display(Name = "[red]CTL[/]", ShortName = "CTL")]
    [ResourceDescription<Ln>(nameof(Ln.宕机))]
    Critical = 5
}