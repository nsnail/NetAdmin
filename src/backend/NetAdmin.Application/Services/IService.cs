using NetAdmin.DataContract.Context;

namespace NetAdmin.Application.Services;

/// <summary>
///     服务接口
/// </summary>
public interface IService
{
    /// <summary>
    ///     服务Id
    /// </summary>
    Guid ServiceId { get; set; }

    /// <summary>
    ///     上下文用户
    /// </summary>
    ContextUser User { get; set; }
}