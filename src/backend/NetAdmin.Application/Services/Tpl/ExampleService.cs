using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Tpl.Dependency;
using NetAdmin.DataContract.DbMaps.Tpl;
using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Tpl.Example;

namespace NetAdmin.Application.Services.Tpl;

/// <inheritdoc cref="IExampleService" />
public class ExampleService : RepositoryService<TbTplExample, IExampleService>, IExampleService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExampleService" /> class.
    /// </summary>
    public ExampleService(Repository<TbTplExample> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除示例
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
    ///     创建示例
    /// </summary>
    public async Task<QueryExampleRsp> Create(CreateExampleReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryExampleRsp>();
    }

    /// <summary>
    ///     删除示例
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <summary>
    ///     分页查询示例
    /// </summary>
    public async Task<PagedQueryRsp<QueryExampleRsp>> PagedQuery(PagedQueryReq<QueryExampleReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryExampleRsp>(req.Page, req.PageSize, total
                                                , list.Select(x => x.Adapt<QueryExampleRsp>()));
    }

    /// <summary>
    ///     查询示例
    /// </summary>
    public async Task<List<QueryExampleRsp>> Query(QueryReq<QueryExampleReq> req)
    {
        var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryExampleRsp>());
    }

    /// <summary>
    ///     更新示例
    /// </summary>
    public async Task<QueryExampleRsp> Update(UpdateExampleReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqlite(req);
        }

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

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QueryExampleRsp> UpdateForSqlite(UpdateExampleReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            return null;
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryExampleRsp>();
    }
}