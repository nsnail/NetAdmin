namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     管理员交易类型
/// </summary>
[Export]
public enum AdminTradeTypes
{
    /// <summary>
    ///     管理员赠送
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.管理员赠送))]
    [Trade(Direction = TradeDirections.Income)]
    AdminDeposit = 1

   ,

    /// <summary>
    ///     管理员扣费
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.管理员扣费))]
    [Trade(Direction = TradeDirections.Expense)]
    AdminDeduct = 2
}