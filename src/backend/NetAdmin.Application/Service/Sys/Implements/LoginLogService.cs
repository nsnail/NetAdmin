using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Log;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Application.Service.Sys.ILoginLogService" />
public class LoginLogService : RepositoryService<TbSysLoginLog, ILoginLogService>, ILoginLogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LoginLogService" /> class.
    /// </summary>
    public LoginLogService(Repository<TbSysLoginLog> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async Task<QueryLoginLogRsp> Create(CreateLoginLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryLoginLogRsp>();
    }

    /// <inheritdoc />
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQuery(PagedQueryReq<QueryLoginLogReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryLoginLogRsp>(req.Page, req.PageSize, total
                                                 , list.Select(x => x.Adapt<QueryLoginLogRsp>()));
    }

    /// <inheritdoc />
    public async Task<List<QueryLoginLogRsp>> Query(QueryReq<QueryLoginLogReq> req)
    {
        var ret = await QueryInternal(req).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryLoginLogRsp>());
    }

    /// <inheritdoc />
    public Task<NopReq> Update(NopReq req)
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