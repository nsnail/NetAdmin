namespace NetAdmin.Domain.Enums.Res;

/// <summary>
///     状态
/// </summary>
public enum AllocStatues
{
    /// <summary>
    ///     等待分配
    /// </summary>
    Waiting = 1

   ,

    /// <summary>
    ///     已分配
    /// </summary>
    Allocated = 2

   ,

    /// <summary>
    ///     等待重分配
    /// </summary>
    ReAllocating = 3
}