using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.Api;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     接口模块
/// </summary>
public interface IApiModule : ICrudModule<CreateApiReq, QueryApiRsp // 创建类型
  , QueryApiReq, QueryApiRsp                                        // 查询类型
  , NopReq, NopReq                                                  // 修改类型
  , DelReq                                                          // 删除类型
>
{
    /// <summary>
    ///     同步接口
    /// </summary>
    Task Sync();
}