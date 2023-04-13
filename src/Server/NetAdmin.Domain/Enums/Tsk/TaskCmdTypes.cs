namespace NetAdmin.Domain.Enums.Tsk;

/// <summary>
///     任务指令类型
/// </summary>
[Export]
public enum TaskCmdTypes
{
    /// <summary>
    ///     带内容
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.TookBody))]
    TookBody = 0

   ,

    /// <summary>
    ///     反向获取内容
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.ReverseBody))]
    ReverseBody = 1

   ,

    /// <summary>
    ///     不带内容
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.NoBody))]
    NoBody = 2
}