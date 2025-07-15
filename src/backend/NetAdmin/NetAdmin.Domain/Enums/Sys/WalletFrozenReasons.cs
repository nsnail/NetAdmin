namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     钱包冻结原因
/// </summary>
[Export]
public enum WalletFrozenReasons
{
    /// <summary>
    ///     交易
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.交易))]
    Trade = 1

   ,

    /// <summary>
    ///     人工
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.人工))]
    Manual = 2
}