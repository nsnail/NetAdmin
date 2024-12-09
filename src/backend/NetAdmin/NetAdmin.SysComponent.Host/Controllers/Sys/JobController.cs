using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     计划作业服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
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
    ///     计划作业计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryJobReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     计划作业分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryJobReq> req)
    {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     作业记录计数
    /// </summary>
    public Task<long> CountRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Cache.CountRecordAsync(req);
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
    ///     编辑作业
    /// </summary>
    [Transaction]
    public Task<QueryJobRsp> EditAsync(EditJobReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     执行作业
    /// </summary>
    public Task ExecuteAsync(QueryJobReq req)
    {
        return Cache.ExecuteAsync(req);
    }

    /// <summary>
    ///     导出计划作业
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     导出作业记录
    /// </summary>
    public Task<IActionResult> ExportRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Cache.ExportRecordAsync(req);
    }

    /// <summary>
    ///     获取单个计划作业
    /// </summary>
    public Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取单个作业记录
    /// </summary>
    public Task<QueryJobRecordRsp> GetRecordAsync(QueryJobRecordReq req)
    {
        return Cache.GetRecordAsync(req);
    }

    /// <summary>
    ///     获取作业记录条形图数据
    /// </summary>
    public Task<IEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Cache.GetRecordBarChartAsync(req);
    }

    /// <summary>
    ///     状态码分组作业记录饼图数据
    /// </summary>
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Cache.GetRecordPieChartByHttpStatusCodeAsync(req);
    }

    /// <summary>
    ///     名称分组作业记录饼图数据
    /// </summary>
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Cache.GetRecordPieChartByNameAsync(req);
    }

    /// <summary>
    ///     分页查询计划作业
    /// </summary>
    public Task<PagedQueryRsp<QueryJobRsp>> PagedQueryAsync(PagedQueryReq<QueryJobReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     分页查询作业记录
    /// </summary>
    public Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryRecordAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return Cache.PagedQueryRecordAsync(req);
    }

    /// <summary>
    ///     查询计划作业
    /// </summary>
    public Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     作业记录分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> RecordCountByAsync(QueryReq<QueryJobRecordReq> req)
    {
        return Cache.RecordCountByAsync(req);
    }

    /// <summary>
    ///     启用/禁用作业
    /// </summary>
    public Task<int> SetEnabledAsync(SetJobEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }
}