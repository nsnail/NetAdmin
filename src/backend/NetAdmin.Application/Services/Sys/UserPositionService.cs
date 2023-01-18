using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.UserPosition;

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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建用户-岗位映射
    /// </summary>
    public async Task<QueryUserPositionRsp> Create(CreateUserPositionReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryUserPositionRsp>();
    }

    /// <summary>
    ///     删除用户-岗位映射
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <summary>
    ///     分页查询用户-岗位映射
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserPositionRsp>> PagedQuery(PagedQueryReq<QueryUserPositionReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryUserPositionRsp>(req.Page, req.PageSize, total
                                                     , list.Adapt<IEnumerable<QueryUserPositionRsp>>());
    }

    /// <summary>
    ///     查询用户-岗位映射
    /// </summary>
    public async Task<IEnumerable<QueryUserPositionRsp>> Query(QueryReq<QueryUserPositionReq> req)
    {
        var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
        return ret.Adapt<IEnumerable<QueryUserPositionRsp>>();
    }

    /// <summary>
    ///     更新用户-岗位映射
    /// </summary>
    public async Task<QueryUserPositionRsp> Update(UpdateUserPositionReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqlite(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryUserPositionRsp>();
    }

    private ISelect<TbSysUserPosition> QueryInternal(QueryReq<QueryUserPositionReq> req)
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
    private async Task<QueryUserPositionRsp> UpdateForSqlite(UpdateUserPositionReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            return null;
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryUserPositionRsp>();
    }
}