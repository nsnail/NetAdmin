namespace NetAdmin.Domain.Dto.Sys.Cache;

/// <summary>
///     响应：获取所有缓存项
/// </summary>
public sealed record GetAllEntriesRsp : DataAbstraction
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="GetAllEntriesRsp" /> class.
    /// </summary>
    public GetAllEntriesRsp() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="GetAllEntriesRsp" /> class.
    /// </summary>
    public GetAllEntriesRsp(long absExp, string key, long sldExp, string data)
    {
        AbsExp = absExp;
        Key    = key;
        SldExp = sldExp;
        Data   = data;
    }

    /// <summary>
    ///     绝对过期时间
    /// </summary>
    public DateTime? AbsExpTime => AbsExp == -1 ? null : DateTime.FromBinary(AbsExp).ToLocalTime();

    /// <summary>
    ///     滑动过期时间
    /// </summary>
    public DateTime? SldExpTime => SldExp == -1 ? null : DateTime.FromBinary(SldExp).ToLocalTime();

    /// <summary>
    ///     绝对过期时间
    /// </summary>
    [JsonInclude]
    public long AbsExp { get; init; }

    /// <summary>
    ///     缓存值
    /// </summary>
    public string Data { get; init; }

    /// <summary>
    ///     缓存键
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    ///     滑动过期时间
    /// </summary>
    [JsonInclude]
    public long SldExp { get; init; }
}