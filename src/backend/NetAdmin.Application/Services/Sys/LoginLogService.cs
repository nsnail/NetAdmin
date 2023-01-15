using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="ILoginLogService" />
public class LoginLogService : RepositoryService<TbSysLoginLog, ILoginLogService>, ILoginLogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginLogService" /> class.
    /// </summary>
    public LoginLogService(Repository<TbSysLoginLog> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async ValueTask<QueryLoginLogRsp> Create(CreateLoginLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryLoginLogRsp>();
    }

    /// <inheritdoc />
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async ValueTask<PagedQueryRsp<QueryLoginLogRsp>> PagedQuery(PagedQueryReq<QueryLoginLogReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryLoginLogRsp>(req.Page, req.PageSize, total
                                                 , list.Select(x => x.Adapt<QueryLoginLogRsp>()));
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryLoginLogRsp>> Query(QueryReq<QueryLoginLogReq> req)
    {
        var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryLoginLogRsp>());
    }

    /// <inheritdoc />
    public ValueTask<NopReq> Update(NopReq req)
    {
        throw new NotImplementedException();
    }

    private ISelect<TbSysLoginLog> QueryInternal(QueryReq<QueryLoginLogReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }
}