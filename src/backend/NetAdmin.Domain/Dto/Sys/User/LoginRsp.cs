namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：密码登录
/// </summary>
public sealed record LoginRsp : DataAbstraction
{
    /// <summary>
    ///     访问令牌
    /// </summary>
    public string AccessToken { get; init; }

    /// <summary>
    ///     刷新令牌
    /// </summary>
    public string RefreshToken { get; init; }

    /// <summary>
    ///     设置到响应头
    /// </summary>
    public void SetToRspHeader()
    {
        // 设置响应报文头
        App.HttpContext.Response.Headers[Chars.FLG_ACCESS_TOKEN]   = AccessToken;
        App.HttpContext.Response.Headers[Chars.FLG_X_ACCESS_TOKEN] = RefreshToken;
    }
}