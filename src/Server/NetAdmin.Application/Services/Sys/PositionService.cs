using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Position;
using DataType = FreeSql.DataType;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IPositionService" />
public class PositionService : RepositoryService<TbSysPosition, IPositionService>, IPositionService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PositionService" /> class.
    /// </summary>
    public PositionService(Repository<TbSysPosition> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除岗位
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
    ///     创建岗位
    /// </summary>
    public async Task<QueryPositionRsp> CreateAsync(CreatePositionReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryPositionRsp>();
    }

    /// <summary>
    ///     删除岗位
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <summary>
    ///     分页查询岗位
    /// </summary>
    public async Task<PagedQueryRsp<QueryPositionRsp>> PagedQueryAsync(PagedQueryReq<QueryPositionReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryPositionRsp>(req.Page, req.PageSize, total
                                                 , list.Adapt<IEnumerable<QueryPositionRsp>>());
    }

    /// <summary>
    ///     查询岗位
    /// </summary>
    public async Task<IEnumerable<QueryPositionRsp>> QueryAsync(QueryReq<QueryPositionReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryPositionRsp>>();
    }

    /// <summary>
    ///     更新岗位
    /// </summary>
    public async Task<QueryPositionRsp> UpdateAsync(UpdatePositionReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryPositionRsp>();
    }

    private ISelect<TbSysPosition> QueryInternal(QueryReq<QueryPositionReq> req)
    {
        return Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                  .OrderByDescending(a => a.Id);
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QueryPositionRsp> UpdateForSqliteAsync(UpdatePositionReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            return null;
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryPositionRsp>();
    }
}