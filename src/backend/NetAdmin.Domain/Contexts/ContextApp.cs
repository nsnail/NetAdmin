namespace NetAdmin.Domain.Contexts;

/// <summary>
///     上下文应用信息
/// </summary>
/// <remarks>
///     签名算法： $"${appId}{appSecret.ToLowerInvariant()}{timestamp}{reqBody}".Md5(Encoding.UTF8);
///     reqBody 需去除\r、\n、whitespace
/// </remarks>
public sealed record ContextApp : DataAbstraction, IValidatableObject
{
    private const int _TS_OFFSET_SCOPE_SEC = 30;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ContextApp" /> class.
    /// </summary>
    public ContextApp(long appId, string appSecret, long timestamp)
    {
        AppId     = appId;
        AppSecret = appSecret;
        Timestamp = timestamp;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ContextApp" /> class.
    /// </summary>
    private ContextApp()
    {
        AppId     = App.HttpContext.Request.Headers[nameof(AppId)].FirstOrDefault().Int64Try(0);
        AppSecret = App.HttpContext.Request.Headers[nameof(AppSecret)].FirstOrDefault();
        Sign      = App.HttpContext.Request.Headers[nameof(Sign)].FirstOrDefault();
        Timestamp = App.HttpContext.Request.Headers[nameof(Timestamp)].FirstOrDefault().Int64Try(0);
    }

    /// <summary>
    ///     AppId
    /// </summary>
    [Range(1, long.MaxValue)]
    public long AppId { get; init; }

    /// <summary>
    ///     AppSecret
    /// </summary>
    public string AppSecret { get; init; }

    /// <summary>
    ///     签名
    /// </summary>
    public string Sign { get; set; }

    /// <summary>
    ///     时间戳
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    ///     从HttpContext  创建上下文应用
    /// </summary>
    public static async Task<ContextApp> CreateAsync()
    {
        var ret = new ContextApp();
        if (!ret.TryValidate().IsValid) {
            return null;
        }

        // 具有secret的情况下，自动生成时间戳+sign，方便调试
        if (!ret.AppSecret.NullOrEmpty()) {
            ret.Timestamp = DateTime.Now.TimeUnixUtc();
            ret.Sign      = await ret.BuildSignFromHttpContextAsync();
        }

        return ret;
    }

    /// <summary>
    ///     构建签名
    /// </summary>
    public string BuildSign(string reqBody)
    {
        // 去除\r\n和空格再计算签名，规避风格样式问题
        reqBody = reqBody.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty);
        return $"{AppId}{AppSecret.ToLowerInvariant()}{Timestamp}{reqBody}".Md5(Encoding.UTF8);
    }

    /// <summary>
    ///     构建签名（从http上下文）
    /// </summary>
    public async Task<string> BuildSignFromHttpContextAsync()
    {
        var sr      = new StreamReader(App.HttpContext.Request.Body);
        var reqBody = await sr.ReadToEndAsync();

        _ = App.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
        return BuildSign(reqBody);
    }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!AppSecret.NullOrEmpty()) {
            yield break;
        }

        // 没有密码， 就要签名+时间戳
        if (Sign.NullOrEmpty()) {
            yield return new ValidationResult(Ln.签名缺失, new[] { nameof(Sign) });
        }

        if (Math.Abs(DateTime.Now.TimeUnixUtc() - Timestamp) > _TS_OFFSET_SCOPE_SEC) {
            yield return new ValidationResult(Ln.时间戳缺失或误差过大, new[] { nameof(Timestamp) });
        }
    }
}