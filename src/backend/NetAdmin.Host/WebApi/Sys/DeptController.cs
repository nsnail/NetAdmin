using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.Dept;

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
    ///     创建部门
    /// </summary>
    public async ValueTask<QueryDeptRsp> Create(CreateDeptReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    public async ValueTask<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询部门
    /// </summary>
    [NonAction]
    public ValueTask<PagedQueryRsp<QueryDeptRsp>> PagedQuery(PagedQueryReq<QueryDeptReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public async ValueTask<List<QueryDeptRsp>> Query(QueryReq<QueryDeptReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新部门
    /// </summary>
    public async ValueTask<QueryDeptRsp> Update(UpdateDeptReq req)
    {
        return await Service.Update(req);
    }
}