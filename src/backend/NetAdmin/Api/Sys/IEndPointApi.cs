using NetAdmin.DataContract.Dto.Sys.Endpoint;

namespace NetAdmin.Api.Sys;

/// <summary>
///     端点接口
/// </summary>
public interface IEndPointApi : IRestfulApi
{
    /// <summary>
    ///     端点列表
    /// </summary>
    Task<HashSet<EndpointInfo>> List();

    /// <summary>
    ///     端点同步
    /// </summary>
    Task Sync();
}