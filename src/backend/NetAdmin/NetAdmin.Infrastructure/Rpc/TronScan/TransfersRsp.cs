namespace NetAdmin.Infrastructure.Rpc.TronScan;

/// <summary>
///     表示包含合约信息及相关数据的根响应对象。
/// </summary>
public record TransfersRsp
{
    /// <summary>
    ///     获取或设置按地址键入的正常地址信息字典。
    /// </summary>
    public Dictionary<string, NormalAddressInfo> NormalAddressInfo { get; set; }

    /// <summary>
    ///     获取或设置范围内的项目总数。
    /// </summary>
    public int RangeTotal { get; set; }

    /// <summary>
    ///     获取或设置令牌转账列表。
    /// </summary>
    [JsonPropertyName("token_transfers")]
    public IReadOnlyCollection<TokenTransferInfo> TokenTransfers { get; set; }

    /// <summary>
    ///     获取或设置项目的总数。
    /// </summary>
    public int Total { get; set; }
}