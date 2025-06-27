namespace NetAdmin.Infrastructure.Rpc.TronScan;

/// <summary>
///     表示一个普通的地址信息记录。
/// </summary>
/// <remarks>
///     该记录包含一个布尔值属性，用于指示地址是否存在风险。
/// </remarks>
public record NormalAddressInfo
{
    /// <summary>
    ///     获取或设置地址是否存在风险的标识。
    /// </summary>
    public bool Risk { get; set; }
}