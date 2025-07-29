using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IConfigService" />
public sealed class ConfigService(BasicRepository<Sys_Config, long> rpo) //
    : RepositoryService<Sys_Config, long, IConfigService>(rpo), IConfigService
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
    public Task<long> CountAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_Config>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_Config).GetProperty(y)!.GetValue(x.Key)?.ToString())
                            , x.Value))
                  .OrderByDescending(x => x.Value);
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
    public async Task<QueryConfigRsp> EditAsync(EditConfigReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryConfigRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryConfigReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryConfigReq, ExportConfigRsp>(QueryInternal, req, Ln.配置导出);
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryConfigReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryConfigRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        var ret = await QueryAsync(new QueryReq<QueryConfigReq> { Count = 1, Filter = new QueryConfigReq { Enabled = true } }).ConfigureAwait(false);
        return ret.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<QueryConfigRsp> GetRegisterConfigAsync()
    {
        var latestConfigAsync = await GetLatestConfigAsync().ConfigureAwait(false);
        return new QueryConfigRsp {
                                      RegisterInviteRequired = latestConfigAsync.RegisterInviteRequired
                                    , RegisterMobileRequired = latestConfigAsync.RegisterMobileRequired
                                  };
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryConfigRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryConfigRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryConfigRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetConfigEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.Enabled)]);
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryConfigReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().SumAsync(req.GetSumExp<Sys_Config>());
    }

    private ISelect<Sys_Config> QueryInternal(QueryReq<QueryConfigReq> req)
    {
        var ret = Rpo.Select.Include(a => a.UserRegisterDept)
                     .Include(a => a.UserRegisterRole)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf( //
                         req.Filter?.Enabled.HasValue ?? false, a => a.Enabled == req.Filter.Enabled.Value);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret.AppendOtherFilters(req);
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret.AppendOtherFilters(req);
    }
}