using NetAdmin.DataContract.Dto.Sys.User;

namespace NetAdmin.Application.Caches.Sys;

/// <summary>
///     用户缓存接口
/// </summary>
public interface IUserCache
{
    /// <summary>
    ///     当前用户信息
    /// </summary>
    Task<QueryUserRsp> UserInfo(Func<Task<QueryUserRsp>> create);
}