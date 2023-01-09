using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Endpoint;

namespace NetAdmin.Api.Sys;

/// <summary>
///     端点接口
/// </summary>
public interface IEndPointApi : ICrudApi<NopReq, NopReq      // 创建类型
                                  , NopReq, QueryEndpointRsp // 查询类型
                                  , NopReq, NopReq           // 修改类型
                                  , NopReq                   // 删除类型
                                >, IRestfulApi
{
    /// <summary>
    ///     端点列表
    /// </summary>
    Task<HashSet<QueryEndpointRsp>> List();

    /// <summary>
    ///     端点同步
    /// </summary>
    Task Sync();
}