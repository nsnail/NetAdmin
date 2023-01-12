using NetAdmin.DataContract.Dto.Sys.Endpoint;

namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     端点服务
/// </summary>
public interface IEndPointService : IService
{
    /// <summary>
    ///     生成前端代码
    /// </summary>
    /// <param name="diskPath">磁盘路径</param>
    Task GenerateJsCode(string diskPath);

    /// <summary>
    ///     端点列表
    /// </summary>
    Task<IEnumerable<QueryEndpointRsp>> List();
}