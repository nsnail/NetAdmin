using NetAdmin.Domain.Contexts;

namespace NetAdmin.Application.Services;

/// <summary>
///     服务接口
/// </summary>
public interface IService
{
    /// <summary>
    ///     服务编号
    /// </summary>
    Guid ServiceId { get; set; }

    /// <summary>
    ///     上下文用户令牌
    /// </summary>
    ContextUserToken UserToken { get; set; }
}