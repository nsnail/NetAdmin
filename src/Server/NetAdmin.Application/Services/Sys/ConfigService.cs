using FreeSql;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IConfigService" />
public class ConfigService : RepositoryService<TbSysConfig, IConfigService>, IConfigService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigService" /> class.
    /// </summary>
    public ConfigService(Repository<TbSysConfig> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除配置
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
    ///     创建配置
    /// </summary>
    public async Task<QueryConfigRsp> Create(CreateConfigReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryConfigRsp>();
    }

    /// <summary>
    ///     删除配置
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetLatestConfig()
    {
        var ret = await Query(new QueryReq<QueryConfigReq> {
                                                               Count  = 1
                                                             , Order  = Enums.Orders.Descending
                                                             , Prop   = nameof(TbSysConfig.Id)
                                                             , Filter = new QueryConfigReq { Enabled = true }
                                                           });
        return ret.FirstOrDefault();
    }

    /// <summary>
    ///     分页查询配置
    /// </summary>
    public async Task<PagedQueryRsp<QueryConfigRsp>> PagedQuery(PagedQueryReq<QueryConfigReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryConfigRsp>(req.Page, req.PageSize, total
                                               , list.Adapt<IEnumerable<QueryConfigRsp>>());
    }

    /// <summary>
    ///     查询配置
    /// </summary>
    public async Task<IEnumerable<QueryConfigRsp>> Query(QueryReq<QueryConfigReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryConfigRsp>>();
    }

    /// <summary>
    ///     更新配置
    /// </summary>
    public async Task<QueryConfigRsp> Update(UpdateConfigReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqlite(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryConfigRsp>();
    }

    private ISelect<TbSysConfig> QueryInternal(QueryReq<QueryConfigReq> req)
    {
        var ret = Rpo.Select.Include(a => a.UserRegisterDept)
                     .Include(a => a.UserRegisterRole)
                     .Include(a => a.UserRegisterPos)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf( //
                         req.Filter?.Enabled.HasValue ?? false
                       , a => req.Filter.Enabled.Value
                             ? (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1
                             : (a.BitSet & (long)EntityBase.BitSets.Enabled) == 0)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QueryConfigRsp> UpdateForSqlite(UpdateConfigReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            return null;
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryConfigRsp>();
    }
}