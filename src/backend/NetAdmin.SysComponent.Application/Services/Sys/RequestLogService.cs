using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRequestLogService" />
public sealed class RequestLogService : RepositoryService<Sys_RequestLog, IRequestLogService>, IRequestLogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogService" /> class.
    /// </summary>
    public RequestLogService(Repository<Sys_RequestLog> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除请求日志
    /// </summary>
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建请求日志
    /// </summary>
    public async Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <summary>
    ///     删除请求日志
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     判断请求日志是否存在
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     获取单个请求日志
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询请求日志
    /// </summary>
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.ApiId
                                                 , ApiSummary = a.Api.Summary
                                                 , a.ExtraData
                                                 , a.CreatedClientIp
                                                 , a.CreatedTime
                                                 , a.CreatedUserName
                                                 , a.Duration
                                                 , a.Method
                                                 , a.CreatedUserAgent
                                                 , a.HttpStatusCode
                                                 , a.Id
                                               });

        return new PagedQueryRsp<QueryRequestLogRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryRequestLogRsp>>());
    }

    /// <summary>
    ///     查询请求日志
    /// </summary>
    public async Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryRequestLogRsp>>();
    }

    /// <summary>
    ///     更新请求日志
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        throw new NotImplementedException();
    }

    private ISelect<Sys_RequestLog> QueryInternal(QueryReq<QueryRequestLogReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Api)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}