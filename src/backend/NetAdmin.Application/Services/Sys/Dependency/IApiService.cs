using NetAdmin.Application.Modules.Sys;

namespace NetAdmin.Application.Services.Sys.Dependency;

/// <summary>
///     接口服务
/// </summary>
public interface IApiService : IService, IApiModule
{
    /// <summary>
    ///     反射接口列表
    /// </summary>
    IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true);
}