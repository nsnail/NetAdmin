namespace NetAdmin.Infrastructure.Rpc.TronScan;

/// <summary>
///     表示触发智能合约的信息。
/// </summary>
public record TriggerInfo
{
    /// <summary>
    ///     获取或设置调用合约时传递的值。
    /// </summary>
    [JsonPropertyName("call_value")]
    public int CallValue { get; set; }

    /// <summary>
    ///     获取或设置合约的地址。
    /// </summary>
    [JsonPropertyName("contract_address")]
    public string ContractAddress { get; set; }

    /// <summary>
    ///     获取或设置调用合约方法的数据。
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    ///     获取或设置合约方法的标识符。
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    ///     获取或设置合约方法的名称。
    /// </summary>
    public string MethodName { get; set; }
}