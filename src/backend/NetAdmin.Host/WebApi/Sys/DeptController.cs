using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     部门服务
/// </summary>
public class DeptController : ControllerBase<IDeptService>, IDeptModule
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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建部门
    /// </summary>
    [Transaction]
    public async Task<QueryDeptRsp> Create(CreateDeptReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询部门
    /// </summary>
    [NonAction]
    public async Task<PagedQueryRsp<QueryDeptRsp>> PagedQuery(PagedQueryReq<QueryDeptReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public async Task<IEnumerable<QueryDeptRsp>> Query(QueryReq<QueryDeptReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新部门
    /// </summary>
    [Transaction]
    public async Task<QueryDeptRsp> Update(UpdateDeptReq req)
    {
        return await Service.Update(req);
    }
}