namespace NetAdmin.Domain.Enums.Res;

/// <summary>
///     设备执行状态
/// </summary>
public enum ExecStatues
{
    /// <summary>
    ///     空闲
    /// </summary>
    Idle = 0

   ,

    /// <summary>
    ///     执行中
    /// </summary>
    Executing = 1

   ,

    /// <summary>
    ///     异常
    /// </summary>
    Exception = -1
}