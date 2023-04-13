using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Position;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IPositionService" />
public sealed class PositionService : RepositoryService<Sys_Position, IPositionService>, IPositionService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PositionService" /> class.
    /// </summary>
    public PositionService(Repository<Sys_Position> rpo) //
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

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryPositionReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryPositionRsp> GetAsync(QueryPositionReq req)
    {
        throw new NotImplementedException();
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
        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryPositionRsp>();
    }

    private ISelect<Sys_Position> QueryInternal(QueryReq<QueryPositionReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.Sort), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Sort);
        }

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}