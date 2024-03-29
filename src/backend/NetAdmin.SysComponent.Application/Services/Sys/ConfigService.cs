using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IConfigService" />
public sealed class ConfigService(DefaultRepository<Sys_Config> rpo) //
    : RepositoryService<Sys_Config, IConfigService>(rpo), IConfigService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryConfigRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryConfigReq> { Filter = req }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryConfigRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        var ret = await QueryAsync(
                new QueryReq<QueryConfigReq> { Count = 1, Filter = new QueryConfigReq { Enabled = true } })
            .ConfigureAwait(false);
        return ret.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryConfigRsp>(req.Page, req.PageSize, total
                                               , list.Adapt<IEnumerable<QueryConfigRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryConfigRsp>>();
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> UpdateAsync(UpdateConfigReq req)
    {
        req.ThrowIfInvalid();
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req).ConfigureAwait(false) as QueryConfigRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QueryConfigRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_Config> UpdateForSqliteAsync(Sys_Config req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QueryConfigReq { Id = req.Id }).ConfigureAwait(false);
    }

    private ISelect<Sys_Config> QueryInternal(QueryReq<QueryConfigReq> req)
    {
        var ret = Rpo.Select.Include(a => a.UserRegisterDept)
                     .Include(a => a.UserRegisterRole)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf( //
                         req.Filter?.Enabled.HasValue ?? false, a => a.Enabled == req.Filter.Enabled.Value)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}