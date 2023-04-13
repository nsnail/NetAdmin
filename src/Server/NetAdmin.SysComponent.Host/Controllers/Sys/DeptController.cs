using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     部门服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class DeptController : ControllerBase<IDeptService>, IDeptModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeptController" /> class.
    /// </summary>
    public DeptController(IDeptService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除部门
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建部门
    /// </summary>
    [Transaction]
    public Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     部门是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        return Service.GetAsync(req);
    }

    /// <summary>
    ///     分页查询部门
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     更新部门
    /// </summary>
    [Transaction]
    public Task<QueryDeptRsp> UpdateAsync(UpdateDeptReq req)
    {
        return Service.UpdateAsync(req);
    }
}