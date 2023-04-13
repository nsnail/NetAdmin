using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     接口服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class ApiController : ControllerBase<IApiService>, IApiModule
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
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建接口
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除接口
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     接口是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        return Service.GetAsync(req);
    }

    /// <summary>
    ///     分页查询接口
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询接口
    /// </summary>
    public Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     同步接口
    /// </summary>
    [Transaction]
    public Task SyncAsync()
    {
        return Service.SyncAsync();
    }

    /// <summary>
    ///     更新接口
    /// </summary>
    [NonAction]
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        return Service.UpdateAsync(req);
    }
}