using NetAdmin.Domain.Dto.Sys.Api;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     接口服务
/// </summary>
public interface IApiService : IService, IApiModule
{
    /// <summary>
    ///     接口是否存在
    /// </summary>
    Task<bool> ExistAsync(QueryReq<QueryApiReq> req);

    /// <summary>
    ///     反射接口列表
    /// </summary>
    IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true);
}