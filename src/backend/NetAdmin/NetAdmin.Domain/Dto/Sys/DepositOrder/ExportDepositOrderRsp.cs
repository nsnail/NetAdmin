namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     请求：导出充值订单
/// </summary>
public sealed record ExportDepositOrderRsp : DataAbstraction
{
    /// <summary>
    ///     充值金额
    /// </summary>
    [Ganss.Excel.Column(6)]
    public decimal 充值金额 { get; init; }

    /// <summary>
    ///     订单编号
    /// </summary>
    [Ganss.Excel.Column(1)]
    public string 订单编号 { get; init; }

    /// <summary>
    ///     订单状态
    /// </summary>
    [Ganss.Excel.Column(5)]
    public string 订单状态 { get; init; }

    /// <summary>
    ///     归属用户
    /// </summary>
    [Ganss.Excel.Column(2)]
    public string 归属用户 { get; init; }

    /// <summary>
    ///     汇率
    /// </summary>
    [Ganss.Excel.Column(7)]
    public decimal 汇率 { get; init; }

    /// <summary>
    ///     渠道
    /// </summary>
    [Ganss.Excel.Column(4)]
    public string 渠道 { get; set; }

    /// <summary>
    ///     上级
    /// </summary>
    [Ganss.Excel.Column(3)]
    public string 上级 { get; set; }

    /// <summary>
    ///     支付方式
    /// </summary>
    [Ganss.Excel.Column(9)]
    public string 支付方式 { get; init; }

    /// <summary>
    ///     支付金额
    /// </summary>
    [Ganss.Excel.Column(8)]
    public decimal 支付金额 { get; init; }
}