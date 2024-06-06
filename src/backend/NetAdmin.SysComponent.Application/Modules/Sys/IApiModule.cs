using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     接口模块
/// </summary>
public interface IApiModule : ICrudModule<CreateApiReq, QueryApiRsp // 创建类型
  , QueryApiReq, QueryApiRsp                                        // 查询类型
  , DelReq                                                          // 删除类型
>
{
    /// <summary>
    ///     同步接口
    /// </summary>
    Task SyncAsync();
}