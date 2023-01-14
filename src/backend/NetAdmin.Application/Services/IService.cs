using NetAdmin.DataContract;

namespace NetAdmin.Application.Services;

/// <summary>
///     服务接口
/// </summary>
public interface IService
{
    /// <summary>
    ///     服务Id
    /// </summary>
    Guid ServiceId { get; }

    /// <summary>
    ///     登录用户
    /// </summary>
    ContextUser User { get; set; }
}