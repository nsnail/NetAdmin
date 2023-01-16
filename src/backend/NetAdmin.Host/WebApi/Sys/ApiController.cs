using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.Api;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     接口服务
/// </summary>
public class ApiController : ControllerBase<IApiService>, IApiModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiController" /> class.
    /// </summary>
    public ApiController(IApiService service) //
        : base(service) { }

    /// <summary>
    ///     创建接口
    /// </summary>
    [NonAction]
    public ValueTask<QueryApiRsp> Create(CreateApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     删除接口
    /// </summary>
    [NonAction]
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询接口
    /// </summary>
    [NonAction]
    public ValueTask<PagedQueryRsp<QueryApiRsp>> PagedQuery(PagedQueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询接口
    /// </summary>
    public async ValueTask<List<QueryApiRsp>> Query(QueryReq<QueryApiReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     同步接口
    /// </summary>
    [Transaction]
    public ValueTask Sync()
    {
        return Service.Sync();
    }

    /// <summary>
    ///     更新接口
    /// </summary>
    [NonAction]
    public ValueTask<NopReq> Update(NopReq req)
    {
        throw new NotImplementedException();
    }
}