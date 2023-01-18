using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Position;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     岗位服务
/// </summary>
public class PositionController : ControllerBase<IPositionService>, IPositionModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PositionController" /> class.
    /// </summary>
    public PositionController(IPositionService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除岗位
    /// </summary>
    [Transaction]
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建岗位
    /// </summary>
    [Transaction]
    public async Task<QueryPositionRsp> Create(CreatePositionReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除岗位
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询岗位
    /// </summary>
    public async Task<PagedQueryRsp<QueryPositionRsp>> PagedQuery(PagedQueryReq<QueryPositionReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询岗位
    /// </summary>
    public async Task<IEnumerable<QueryPositionRsp>> Query(QueryReq<QueryPositionReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新岗位
    /// </summary>
    [Transaction]
    public async Task<QueryPositionRsp> Update(UpdatePositionReq req)
    {
        return await Service.Update(req);
    }
}