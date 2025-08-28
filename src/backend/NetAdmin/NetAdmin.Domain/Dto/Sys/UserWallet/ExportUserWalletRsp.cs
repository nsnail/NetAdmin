namespace NetAdmin.Domain.Dto.Sys.UserWallet;

/// <summary>
///     响应：导出用户钱包
/// </summary>
public record ExportUserWalletRsp : DataAbstraction
{
    /// <summary>
    ///     冻结余额
    /// </summary>
    [Ganss.Excel.Column(7)]
    public decimal 冻结余额 { get; init; }

    /// <summary>
    ///     归属角色
    /// </summary>
    [Ganss.Excel.Column(3)]
    public string 归属角色 { get; init; }

    /// <summary>
    ///     归属用户
    /// </summary>
    [Ganss.Excel.Column(2)]
    public string 归属用户 { get; init; }

    /// <summary>
    ///     可用余额
    /// </summary>
    [Ganss.Excel.Column(6)]
    public decimal 可用余额 { get; init; }

    /// <summary>
    ///     钱包编号
    /// </summary>
    [Ganss.Excel.Column(1)]
    public string 钱包编号 { get; init; }

    /// <summary>
    ///     渠道
    /// </summary>
    [Ganss.Excel.Column(5)]
    public string 渠道 { get; init; }

    /// <summary>
    ///     上级
    /// </summary>
    [Ganss.Excel.Column(4)]
    public string 上级 { get; init; }

    /// <summary>
    ///     总收入
    /// </summary>
    [Ganss.Excel.Column(8)]
    public decimal 总收入 { get; init; }

    /// <summary>
    ///     总支出
    /// </summary>
    [Ganss.Excel.Column(9)]
    public decimal 总支出 { get; init; }

    /// <summary>
    ///     最后交易时间
    /// </summary>
    [Ganss.Excel.Column(10)]
    public string 最后交易时间 { get; init; }
}