using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgFlagService" />
public sealed class SiteMsgFlagService(BasicRepository<Sys_SiteMsgFlag, long> rpo) //
    : RepositoryService<Sys_SiteMsgFlag, long, ISiteMsgFlagService>(rpo), ISiteMsgFlagService
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
    public Task<long> CountAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_SiteMsgFlag>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_SiteMsgFlag).GetProperty(y)!.GetValue(x.Key)!.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgFlagRsp> CreateAsync(CreateSiteMsgFlagReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgFlagRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgFlagRsp> EditAsync(EditSiteMsgFlagReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgFlagRsp> GetAsync(QuerySiteMsgFlagReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgFlagReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgFlagRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgFlagRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgFlagReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).WithNoLockNoWait().Count(out var total).ToListAsync().ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgFlagRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgFlagRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgFlagRsp>> QueryAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgFlagRsp>>();
    }

    /// <inheritdoc />
    public Task SetUserSiteMsgStatusAsync(SetUserSiteMsgStatusReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.UserSiteMsgStatus)], null, a => a.UserId == req.UserId && a.SiteMsgId == req.SiteMsgId);
    }

    private ISelect<Sys_SiteMsgFlag> QueryInternal(QueryReq<QuerySiteMsgFlagReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}