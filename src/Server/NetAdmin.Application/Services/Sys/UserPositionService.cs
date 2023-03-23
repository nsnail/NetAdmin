using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.UserPosition;
using DataType = FreeSql.DataType;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IUserPositionService" />
public class UserPositionService : RepositoryService<TbSysUserPosition, IUserPositionService>, IUserPositionService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserPositionService" /> class.
    /// </summary>
    public UserPositionService(Repository<TbSysUserPosition> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除用户-岗位映射
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
    ///     创建用户-岗位映射
    /// </summary>
    public async Task<QueryUserPositionRsp> CreateAsync(CreateUserPositionReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryUserPositionRsp>();
    }

    /// <summary>
    ///     删除用户-岗位映射
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <summary>
    ///     分页查询用户-岗位映射
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserPositionRsp>> PagedQueryAsync(PagedQueryReq<QueryUserPositionReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryUserPositionRsp>(req.Page, req.PageSize, total
                                                     , list.Adapt<IEnumerable<QueryUserPositionRsp>>());
    }

    /// <summary>
    ///     查询用户-岗位映射
    /// </summary>
    public async Task<IEnumerable<QueryUserPositionRsp>> QueryAsync(QueryReq<QueryUserPositionReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryUserPositionRsp>>();
    }

    /// <summary>
    ///     更新用户-岗位映射
    /// </summary>
    public async Task<QueryUserPositionRsp> UpdateAsync(UpdateUserPositionReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryUserPositionRsp>();
    }

    private ISelect<TbSysUserPosition> QueryInternal(QueryReq<QueryUserPositionReq> req)
    {
        return Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                  .OrderByDescending(a => a.Id);
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QueryUserPositionRsp> UpdateForSqliteAsync(UpdateUserPositionReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            return null;
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryUserPositionRsp>();
    }
}