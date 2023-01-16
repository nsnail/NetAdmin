using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Tpl.Dependency;
using NetAdmin.DataContract.DbMaps.Tpl;
using NetAdmin.DataContract.Dto.Tpl.Example;

namespace NetAdmin.Application.Services.Tpl;

/// <inheritdoc cref="NetAdmin.Application.Services.Tpl.Dependency.IExampleService" />
public class ExampleService : RepositoryService<TbTplExample, IExampleService>, IExampleService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExampleService" /> class.
    /// </summary>
    public ExampleService(Repository<TbTplExample> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async ValueTask<QueryExampleRsp> Create(CreateExampleReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryExampleRsp>();
    }

    /// <inheritdoc />
    public async ValueTask<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    public async ValueTask<PagedQueryRsp<QueryExampleRsp>> PagedQuery(PagedQueryReq<QueryExampleReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryExampleRsp>(req.Page, req.PageSize, total
                                                , list.Select(x => x.Adapt<QueryExampleRsp>()));
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryExampleRsp>> Query(QueryReq<QueryExampleReq> req)
    {
        var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryExampleRsp>());
    }

    /// <inheritdoc />
    public async ValueTask<QueryExampleRsp> Update(UpdateExampleReq req)
    {
        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryExampleRsp>();
    }

    private ISelect<TbTplExample> QueryInternal(QueryReq<QueryExampleReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }
}