namespace NetAdmin.Domain.Dto.Sys.WalletTrade;

/// <summary>
///     响应：导出钱包交易
/// </summary>
public record ExportWalletTradeRsp : DataAbstraction
{
    /// <summary>
    ///     备注
    /// </summary>
    [Ganss.Excel.Column(11)]
    public string 备注 { get; init; }

    /// <summary>
    ///     操作人
    /// </summary>
    [Ganss.Excel.Column(12)]
    public string 操作人 { get; init; }

    /// <summary>
    ///     归属用户
    /// </summary>
    [Ganss.Excel.Column(2)]
    public string 归属用户 { get; init; }

    /// <summary>
    ///     交易编号
    /// </summary>
    [Ganss.Excel.Column(1)]
    public string 交易编号 { get; init; }

    /// <summary>
    ///     交易方向
    /// </summary>
    [Ganss.Excel.Column(5)]
    public string 交易方向 { get; init; }

    /// <summary>
    ///     交易后余额
    /// </summary>
    [Ganss.Excel.Column(10)]
    public decimal 交易后余额 { get; init; }

    /// <summary>
    ///     交易金额
    /// </summary>
    [Ganss.Excel.Column(9)]
    public decimal 交易金额 { get; init; }

    /// <summary>
    ///     交易类型
    /// </summary>
    [Ganss.Excel.Column(6)]
    public string 交易类型 { get; init; }

    /// <summary>
    ///     交易前余额
    /// </summary>
    [Ganss.Excel.Column(8)]
    public decimal 交易前余额 { get; init; }

    /// <summary>
    ///     渠道
    /// </summary>
    [Ganss.Excel.Column(4)]
    public string 渠道 { get; init; }

    /// <summary>
    ///     上级
    /// </summary>
    [Ganss.Excel.Column(3)]
    public string 上级 { get; init; }

    /// <summary>
    ///     业务订单号
    /// </summary>
    [Ganss.Excel.Column(7)]
    public string 业务订单号 { get; init; }
}