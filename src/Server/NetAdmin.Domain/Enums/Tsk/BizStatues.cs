namespace NetAdmin.Domain.Enums.Tsk;

/// <summary>
///     业务状态
/// </summary>
[Export]
public enum BizStatues
{
    /// <summary>
    ///     未知的
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Unknown))]
    Unknown = 1

   ,

    /// <summary>
    ///     已成功
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Succeed))]
    Succeed = 2

   ,

    /// <summary>
    ///     已失败
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Failed))]
    Failed = 3
}