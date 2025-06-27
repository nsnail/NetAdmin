namespace NetAdmin.Infrastructure.Rpc.TronScan;

/// <summary>
///     表示一个代币转账事件。
/// </summary>
public record TokenTransferInfo
{
    /// <summary>
    ///     获取或设置批准的转账金额。
    /// </summary>
    [JsonPropertyName("approval_amount")]
    public string ApprovalAmount { get; set; }

    /// <summary>
    ///     获取或设置发生转账事件的区块编号。
    /// </summary>
    public int Block { get; set; }

    /// <summary>
    ///     获取或设置发生转账事件的区块的时间戳。
    /// </summary>
    [JsonPropertyName("block_ts")]
    public long BlockTs { get; set; }

    /// <summary>
    ///     获取或设置一个值，指示转账是否已确认。
    /// </summary>
    public bool Confirmed { get; set; }

    /// <summary>
    ///     获取或设置代币的合约地址。
    /// </summary>
    [JsonPropertyName("contract_address")]
    public string ContractAddress { get; set; }

    /// <summary>
    ///     获取或设置合约执行的返回结果。
    /// </summary>
    public string ContractRet { get; set; }

    /// <summary>
    ///     获取或设置合约类型。
    /// </summary>
    [JsonPropertyName("contract_type")]
    public string ContractType { get; set; }

    /// <summary>
    ///     获取或设置事件类型。
    /// </summary>
    [JsonPropertyName("event_type")]
    public string EventType { get; set; }

    /// <summary>
    ///     获取或设置转账的最终结果。
    /// </summary>
    public string FinalResult { get; set; }

    /// <summary>
    ///     获取或设置发送方地址。
    /// </summary>
    [JsonPropertyName("from_address")]
    public string FromAddress { get; set; }

    /// <summary>
    ///     获取或设置一个值，指示发送方地址是否为合约地址。
    /// </summary>
    public bool FromAddressIsContract { get; set; }

    /// <summary>
    ///     获取或设置转账数量。
    /// </summary>
    public string Quant { get; set; }

    /// <summary>
    ///     获取或设置一个值，指示转账是否已回滚。
    /// </summary>
    public bool Revert { get; set; }

    /// <summary>
    ///     获取或设置一个值，指示转账是否为风险交易。
    /// </summary>
    public bool RiskTransaction { get; set; }

    /// <summary>
    ///     获取或设置转账状态。
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///     获取或设置接收方地址。
    /// </summary>
    [JsonPropertyName("to_address")]
    public string ToAddress { get; set; }

    /// <summary>
    ///     获取或设置一个值，指示接收方地址是否为合约地址。
    /// </summary>
    public bool ToAddressIsContract { get; set; }

    /// <summary>
    ///     获取或设置代币信息。
    /// </summary>
    public TokenInfo TokenInfo { get; set; }

    /// <summary>
    ///     获取或设置交易ID。
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; }

    /// <summary>
    ///     获取或设置合约触发信息。
    /// </summary>
    [JsonPropertyName("trigger_info")]
    public TriggerInfo TriggerInfo { get; set; }
}