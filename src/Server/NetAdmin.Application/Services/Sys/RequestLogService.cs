using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IRequestLogService" />
public class RequestLogService : RepositoryService<TbSysRequestLog, IRequestLogService>, IRequestLogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogService" /> class.
    /// </summary>
    public RequestLogService(Repository<TbSysRequestLog> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除请求日志
    /// </summary>
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建请求日志
    /// </summary>
    public async Task<QueryRequestLogRsp> Create(CreateRequestLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <summary>
    ///     删除请求日志
    /// </summary>
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询请求日志
    /// </summary>
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQuery(PagedQueryReq<QueryRequestLogReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.ApiId
                                                 , ApiSummary = a.Api.Summary
                                                 , a.ExtraData
                                                 , a.ClientIp
                                                 , a.CreatedTime
                                                 , a.CreatedUserName
                                                 , a.Duration
                                                 , a.Method
                                                 , a.UserAgent
                                                 , StatusCode = a.HttpStatusCode
                                                 , a.Id
                                               });

        return new PagedQueryRsp<QueryRequestLogRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryRequestLogRsp>>());
    }

    /// <summary>
    ///     查询请求日志
    /// </summary>
    public async Task<IEnumerable<QueryRequestLogRsp>> Query(QueryReq<QueryRequestLogReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryRequestLogRsp>>();
    }

    /// <summary>
    ///     更新请求日志
    /// </summary>
    public Task<NopReq> Update(NopReq req)
    {
        throw new NotImplementedException();
    }

    private ISelect<TbSysRequestLog> QueryInternal(QueryReq<QueryRequestLogReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Api)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }
}