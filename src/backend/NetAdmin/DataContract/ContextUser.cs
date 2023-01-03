using Furion.DependencyInjection;

namespace NetAdmin.DataContract;

/// <summary>
///     上下文用户信息
/// </summary>
public record ContextUser : IScoped
{
    /// <summary>
    ///     用户id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     做授权验证的Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    internal Guid Token { get; init; }
}