using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Contexts;

/// <summary>
///     上下文用户凭据
/// </summary>
public sealed record ContextUserToken : DataAbstraction
{
    /// <summary>
    ///     部门编号
    /// </summary>
    /// ReSharper disable once MemberCanBePrivate.Global
    public long DeptId { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    /// ReSharper disable once MemberCanBePrivate.Global
    public long Id { get; init; }

    /// <summary>
    ///     做授权验证的Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    /// ReSharper disable once MemberCanBePrivate.Global
    public Guid Token { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    /// ReSharper disable once MemberCanBePrivate.Global
    public string UserName { get; init; }

    /// <summary>
    ///     从HttpContext 创建上下文用户
    /// </summary>
    public static ContextUserToken Create()
    {
        var claim = App.User?.FindFirst(nameof(ContextUserToken));
        return claim?.Value.ToObject<ContextUserToken>();
    }

    /// <summary>
    ///     从 QueryUserRsp 创建上下文用户
    /// </summary>
    public static ContextUserToken Create(QueryUserRsp user)
    {
        return new ContextUserToken { Id = user.Id, Token = user.Token, UserName = user.UserName, DeptId = user.DeptId };
    }

    /// <summary>
    ///     从 Json Web Token 创建上下文用户
    /// </summary>
    public static ContextUserToken Create(string jwt)
    {
        var claim = JWTEncryption.ReadJwtToken(jwt.TrimPrefix($"{Chars.FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA} "))
                                 ?.Claims.FirstOrDefault(x => x.Type == nameof(ContextUserToken));
        return claim?.Value.ToObject<ContextUserToken>();
    }
}