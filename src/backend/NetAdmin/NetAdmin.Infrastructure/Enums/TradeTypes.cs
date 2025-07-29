namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     交易类型
/// </summary>
[Export]
public enum TradeTypes
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

   ,

    /// <summary>
    ///     自助充值
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.自助充值))]
    [Trade(Direction = TradeDirections.Income)]
    SelfDeposit = 3

   ,

    /// <summary>
    ///     转账支出
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.转账支出))]
    [Trade(Direction = TradeDirections.Expense)]
    TransferExpense = 4

   ,

    /// <summary>
    ///     转账收入
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.转账收入))]
    [Trade(Direction = TradeDirections.Income)]
    TransferIncome = 5
}