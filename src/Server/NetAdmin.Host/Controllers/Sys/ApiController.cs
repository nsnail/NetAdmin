using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.Controllers.Sys;

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
    ///     批量删除接口
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建接口
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<QueryApiRsp> Create(CreateApiReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除接口
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询接口
    /// </summary>
    [NonAction]
    public async Task<PagedQueryRsp<QueryApiRsp>> PagedQuery(PagedQueryReq<QueryApiReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询接口
    /// </summary>
    public async Task<IEnumerable<QueryApiRsp>> Query(QueryReq<QueryApiReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     同步接口
    /// </summary>
    [Transaction]
    public async Task Sync()
    {
        await Service.Sync();
    }

    /// <summary>
    ///     更新接口
    /// </summary>
    [NonAction]
    public async Task<NopReq> Update(NopReq req)
    {
        return await Service.Update(req);
    }
}