namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     交易类型
/// </summary>
[Export]
public enum TradeTypes
{
    /// <summary>
    ///     管理员充值
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.管理员充值))]
    [Trade(Direction = TradeDirections.Income)]
    AdminDeposit = 1

   ,

    /// <summary>
    ///     管理员扣费
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.管理员扣费))]
    [Trade(Direction = TradeDirections.Expense)]
    AdminDeduct = 2

   ,

    /// <summary>
    ///     自助充值
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.自助充值))]
    [Trade(Direction = TradeDirections.Income)]
    SelfDeposit = 3
}