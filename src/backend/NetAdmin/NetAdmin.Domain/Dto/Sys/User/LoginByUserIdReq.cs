namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：用户编号登录
/// </summary>
public sealed record LoginByUserIdReq : DataAbstraction
{
    /// <summary>
    ///     无痕登录
    /// </summary>
    public bool NoTrace { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    public long UserId { get; init; }
}