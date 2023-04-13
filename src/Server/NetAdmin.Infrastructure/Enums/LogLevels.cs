namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     日志等级
/// </summary>
public enum LogLevels
{
    /// <summary>
    ///     Trace
    /// </summary>
    [Display(Name = "[gray]TCE[/]", ShortName = "TCE")]
    Trace = 0

   ,

    /// <summary>
    ///     Debug
    /// </summary>
    [Display(Name = "[gray]DBG[/]", ShortName = "DBG")]
    Debug = 1

   ,

    /// <summary>
    ///     Information
    /// </summary>
    [Display(Name = "[green]INF[/]", ShortName = "INF")]
    Information = 2

   ,

    /// <summary>
    ///     Warning
    /// </summary>
    [Display(Name = "[yellow]WRN[/]", ShortName = "WRN")]
    Warning = 3

   ,

    /// <summary>
    ///     Error
    /// </summary>
    [Display(Name = "[red]ERR[/]", ShortName = "ERR")]
    Error = 4

   ,

    /// <summary>
    ///     Critical
    /// </summary>
    [Display(Name = "[red]CTL[/]", ShortName = "CTL")]
    Critical = 5
}