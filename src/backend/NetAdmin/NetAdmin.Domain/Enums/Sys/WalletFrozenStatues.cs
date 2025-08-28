namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     钱包冻结状态
/// </summary>
[Export]
public enum WalletFrozenStatues
{
    /// <summary>
    ///     已冻结
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.已冻结))]
    Frozen = 1

    ,

    /// <summary>
    ///     已解冻
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.已解冻))]
    Thawed = 2
}