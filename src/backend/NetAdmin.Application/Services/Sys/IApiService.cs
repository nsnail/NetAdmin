using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Api;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     接口服务
/// </summary>
public interface IApiService : ICrudService<CreateApiReq, QueryApiRsp // 创建类型
  , QueryApiReq, QueryApiRsp                                          // 查询类型
  , NopReq, NopReq                                                    // 修改类型
  , DelReq                                                            // 删除类型
>
{
    /// <summary>
    ///     反射接口列表
    /// </summary>
    IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true);

    /// <summary>
    ///     同步接口
    /// </summary>
    ValueTask Sync();
}