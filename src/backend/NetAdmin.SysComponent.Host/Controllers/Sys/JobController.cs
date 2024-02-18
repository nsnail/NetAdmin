using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     计划作业服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class JobController(IJobCache cache) : ControllerBase<IJobCache, IJobService>(cache), IJobModule
{
    /// <summary>
    ///     批量删除计划作业
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建计划作业
    /// </summary>
    [Transaction]
    public Task<QueryJobRsp> CreateAsync(CreateJobReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除计划作业
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     计划作业是否存在
    /// </summary>
    public Task<bool> ExistAsync(QueryReq<QueryJobReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个计划作业
    /// </summary>
    public Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询计划作业
    /// </summary>
    public Task<PagedQueryRsp<QueryJobRsp>> PagedQueryAsync(PagedQueryReq<QueryJobReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询计划作业
    /// </summary>
    public Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     获取单个作业记录
    /// </summary>
    public Task<QueryJobRecordRsp> RecordGetAsync(QueryJobRecordReq req)
    {
        return Cache.RecordGetAsync(req);
    }

    /// <summary>
    ///     分页查询作业记录
    /// </summary>
    public Task<PagedQueryRsp<QueryJobRecordRsp>> RecordPagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return Cache.RecordPagedQueryAsync(req);
    }

    /// <summary>
    ///     启用/禁用作业
    /// </summary>
    public Task SetEnabledAsync(UpdateJobReq req)
    {
        return Cache.SetEnabledAsync(req);
    }

    /// <summary>
    ///     更新计划作业
    /// </summary>
    [Transaction]
    public Task<QueryJobRsp> UpdateAsync(UpdateJobReq req)
    {
        return Cache.UpdateAsync(req);
    }
}