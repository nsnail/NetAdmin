namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：用户登录
/// </summary>
public record LoginRsp : DataAbstraction
{
    /// <summary>
    ///     访问令牌
    /// </summary>
    public string AccessToken { get; init; }

    /// <summary>
    ///     刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }
}