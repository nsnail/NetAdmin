using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Application.Service.Sys.IOperationLogService" />
public class OperationLogService : RepositoryService<TbSysOperationLog, IOperationLogService>, IOperationLogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OperationLogService" /> class.
    /// </summary>
    public OperationLogService(ContextUser user, Repository<TbSysOperationLog> rpo) //
        : base(user, rpo) { }

    /// <inheritdoc />
    public async Task<QueryOperationLogRsp> Create(CreateOperationLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryOperationLogRsp>();
    }

    /// <inheritdoc />
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryOperationLogRsp>> PagedQuery(PagedQueryReq<QueryOperationLogReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryOperationLogRsp>(req.Page, req.PageSize, total
                                                     , list.Select(x => x.Adapt<QueryOperationLogRsp>()));
    }

    /// <inheritdoc />
    public async Task<List<QueryOperationLogRsp>> Query(QueryReq<QueryOperationLogReq> req)
    {
        var ret = await QueryInternal(req).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryOperationLogRsp>());
    }

    /// <inheritdoc />
    public Task<NopReq> Update(NopReq req)
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