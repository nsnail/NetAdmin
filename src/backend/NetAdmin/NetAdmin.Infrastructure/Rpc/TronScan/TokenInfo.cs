namespace NetAdmin.Infrastructure.Rpc.TronScan;

/// <summary>
///     表示一个令牌的信息，包括发行者地址、令牌缩写、显示属性等详细信息。
/// </summary>
public record TokenInfo
{
    /// <summary>
    ///     获取或设置发行者的地址。
    /// </summary>
    public string IssuerAddr { get; set; }

    /// <summary>
    ///     获取或设置令牌的缩写。
    /// </summary>
    public string TokenAbbr { get; set; }

    /// <summary>
    ///     获取或设置令牌可显示的数量。
    /// </summary>
    public int TokenCanShow { get; set; }

    /// <summary>
    ///     获取或设置令牌的小数位数。
    /// </summary>
    public int TokenDecimal { get; set; }

    /// <summary>
    ///     获取或设置令牌的唯一标识符。
    /// </summary>
    public string TokenId { get; set; }

    /// <summary>
    ///     获取或设置令牌的级别。
    /// </summary>
    public string TokenLevel { get; set; }

    /// <summary>
    ///     获取或设置令牌的Logo URL或路径。
    /// </summary>
    public string TokenLogo { get; set; }

    /// <summary>
    ///     获取或设置令牌的名称。
    /// </summary>
    public string TokenName { get; set; }

    /// <summary>
    ///     获取或设置令牌的类型。
    /// </summary>
    public string TokenType { get; set; }

    /// <summary>
    ///     获取或设置是否为VIP令牌。
    /// </summary>
    public bool Vip { get; set; }
}