using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

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