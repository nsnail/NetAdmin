namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     交易方向
/// </summary>
[Export]
public enum TradeDirections
{
    /// <summary>
    ///     收入
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.收入))]
    Income = 1

    ,

    /// <summary>
    ///     支出
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.支出))]
    Expense = 2
}