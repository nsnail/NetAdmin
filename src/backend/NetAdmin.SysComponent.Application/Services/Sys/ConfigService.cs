using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IConfigService" />
public sealed class ConfigService : RepositoryService<Sys_Config, IConfigService>, IConfigService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigService" /> class.
    /// </summary>
    public ConfigService(Repository<Sys_Config> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除配置
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
    ///     创建配置
    /// </summary>
    public async Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryConfigRsp>();
    }

    /// <summary>
    ///     删除配置
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <summary>
    ///     判断配置是否存在
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     获取单个配置
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     获取最新有效配置
    /// </summary>
    public async Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        var ret = await QueryAsync(
            new QueryReq<QueryConfigReq> { Count = 1, Filter = new QueryConfigReq { Enabled = true } });
        return ret.FirstOrDefault();
    }

    /// <summary>
    ///     分页查询配置
    /// </summary>
    public async Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryConfigRsp>(req.Page, req.PageSize, total
                                               , list.Adapt<IEnumerable<QueryConfigRsp>>());
    }

    /// <summary>
    ///     查询配置
    /// </summary>
    public async Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryConfigRsp>>();
    }

    /// <summary>
    ///     更新配置
    /// </summary>
    public async Task<QueryConfigRsp> UpdateAsync(UpdateConfigReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryConfigRsp>();
    }

    private ISelect<Sys_Config> QueryInternal(QueryReq<QueryConfigReq> req)
    {
        return Rpo.Select.Include(a => a.UserRegisterDept)
                  .Include(a => a.UserRegisterRole)
                  .WhereDynamicFilter(req.DynamicFilter)
                  .WhereIf( //
                      req.Filter?.Enabled.HasValue ?? false, a => a.Enabled == req.Filter.Enabled.Value)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                  .OrderByDescending(a => a.Id);
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QueryConfigRsp> UpdateForSqliteAsync(Sys_Config req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QueryConfigReq { Id = req.Id });
    }
}