namespace NetAdmin.Domain.Enums.Tsk;

/// <summary>
///     任务状态
/// </summary>
[Export]
public enum TaskStatues
{
    /// <summary>
    ///     等待启动
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Waiting))]
    Waiting = 1

   ,

    /// <summary>
    ///     启动中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Starting))]
    Starting = 2

   ,

    /// <summary>
    ///     已启动
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Started))]
    Started = 3

   ,

    /// <summary>
    ///     启动失败
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.StartFailed))]
    StartFailed = 4

   ,

    /// <summary>
    ///     完成中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Finishing))]
    Finishing = 5

   ,

    /// <summary>
    ///     已完成
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Finished))]
    Finished = 6

   ,

    /// <summary>
    ///     完成失败
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.FinishFailed))]
    FinishFailed = 7

   ,

    /// <summary>
    ///     回调中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Callbacking))]
    Callbacking = 8

   ,

    /// <summary>
    ///     回调完成
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Callbacked))]
    Callbacked = 9

   ,

    /// <summary>
    ///     回调失败
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.CallbackFailed))]
    CallbackFailed = 10
}