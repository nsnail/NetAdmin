using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.SiteMsgDept;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgDeptService" />
public sealed class SiteMsgDeptService(BasicRepository<Sys_SiteMsgDept, long> rpo)
    : RepositoryService<Sys_SiteMsgDept, long, ISiteMsgDeptService>(rpo), ISiteMsgDeptService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QuerySiteMsgDeptReq> req) {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgDeptReq> req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
            .WithNoLockNoWait()
            .GroupBy(req.GetToListExp<Sys_SiteMsgDept>())
            .ToDictionaryAsync(a => a.Count())
            .ConfigureAwait(false);
        return ret
            .Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                    req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_SiteMsgDept).GetProperty(y)!.GetValue(x.Key)?.ToString())
                    , x.Value
                )
            )
            .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgDeptRsp> CreateAsync(CreateSiteMsgDeptReq req) {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgDeptRsp> EditAsync(EditSiteMsgDeptReq req) {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgDeptReq> req) {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgDeptRsp> GetAsync(QuerySiteMsgDeptReq req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgDeptReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgDeptRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgDeptRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgDeptReq> req) {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
            .Page(req.Page, req.PageSize)
            .WithNoLockNoWait()
            .Count(out var total)
            .ToListAsync(req)
            .ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgDeptRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgDeptRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgDeptRsp>> QueryAsync(QueryReq<QuerySiteMsgDeptReq> req) {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgDeptRsp>>();
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QuerySiteMsgDeptReq> req) {
        req.ThrowIfInvalid();
        return QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().SumAsync(req.GetSumExp<Sys_SiteMsgDept>());
    }

    private ISelect<Sys_SiteMsgDept> QueryInternal(QueryReq<QuerySiteMsgDeptReq> req) {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

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