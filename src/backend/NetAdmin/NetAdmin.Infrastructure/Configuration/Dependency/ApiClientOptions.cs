namespace NetAdmin.Infrastructure.Configuration.Dependency;

/// <summary>
///     API客户端基础选项
/// </summary>
public abstract record ApiClientOptions : OptionAbstraction
{
    /// <summary>
    ///     网关地址
    /// </summary>
    public string Gateway { get; set; }

    /// <summary>
    ///     超时时间
    /// </summary>
    public int TimeoutSecs { get; set; }

    /// <summary>
    ///     密钥
    /// </summary>
    public string Token { get; set; }
}