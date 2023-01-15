using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IOperationLogService" />
public class OperationLogService : RepositoryService<TbSysOperationLog, IOperationLogService>, IOperationLogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OperationLogService" /> class.
    /// </summary>
    public OperationLogService(Repository<TbSysOperationLog> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async ValueTask<QueryOperationLogRsp> Create(CreateOperationLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryOperationLogRsp>();
    }

    /// <inheritdoc />
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async ValueTask<PagedQueryRsp<QueryOperationLogRsp>> PagedQuery(PagedQueryReq<QueryOperationLogReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.ApiId
                                                 , a.Api.Summary
                                                 , a.ClientIp
                                                 , a.CreatedTime
                                                 , a.CreatedUserName
                                                 , a.Duration
                                                 , a.Method
                                                 , a.UserAgent
                                                 , a.StatusCode
                                                 , a.Id
                                               });

        return new PagedQueryRsp<QueryOperationLogRsp>(req.Page, req.PageSize, total
                                                     , list.Select(x => x.Adapt<QueryOperationLogRsp>()));
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryOperationLogRsp>> Query(QueryReq<QueryOperationLogReq> req)
    {
        var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryOperationLogRsp>());
    }

    /// <inheritdoc />
    public ValueTask<NopReq> Update(NopReq req)
    {
        throw new NotImplementedException();
    }

    private ISelect<TbSysOperationLog> QueryInternal(QueryReq<QueryOperationLogReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Api)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }
}