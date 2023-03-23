namespace NetAdmin.Domain.Contexts;

/// <summary>
///     上下文用户信息
/// </summary>
public record ContextUser
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     做授权验证的Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    public Guid Token { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; init; }

    /// <summary>
    ///     从HttpContext ClaimsPrincipal 创建上下文用户
    /// </summary>
    public static ContextUser Create()
    {
        var claim = App.User?.FindFirst(nameof(ContextUser));
        return claim?.Value.Object<ContextUser>();
    }
}