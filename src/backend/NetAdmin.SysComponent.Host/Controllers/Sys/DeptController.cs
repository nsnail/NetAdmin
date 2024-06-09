using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     部门服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class DeptController(IDeptCache cache) : ControllerBase<IDeptCache, IDeptService>(cache), IDeptModule
{
    /// <summary>
    ///     批量删除部门
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     部门计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryDeptReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建部门
    /// </summary>
    [Transaction]
    public Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑部门
    /// </summary>
    [Transaction]
    public Task<QueryDeptRsp> EditAsync(EditDeptReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     部门是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个部门
    /// </summary>
    public Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询部门
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     启用/禁用部门
    /// </summary>
    public Task SetEnabledAsync(SetDeptEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }
}