using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.SiteMsgUser;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISiteMsgUserService" />
public sealed class SiteMsgUserService(BasicRepository<Sys_SiteMsgUser, long> rpo) //
    : RepositoryService<Sys_SiteMsgUser, long, ISiteMsgUserService>(rpo), ISiteMsgUserService
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
    public Task<long> CountAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_SiteMsgUser>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_SiteMsgUser).GetProperty(y)!.GetValue(x.Key)!.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgUserRsp> CreateAsync(CreateSiteMsgUserReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgUserRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<QuerySiteMsgUserRsp> EditAsync(EditSiteMsgUserReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QuerySiteMsgUserRsp> GetAsync(QuerySiteMsgUserReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QuerySiteMsgUserReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QuerySiteMsgUserRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QuerySiteMsgUserRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgUserReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QuerySiteMsgUserRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySiteMsgUserRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuerySiteMsgUserRsp>> QueryAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QuerySiteMsgUserRsp>>();
    }

    private ISelect<Sys_SiteMsgUser> QueryInternal(QueryReq<QuerySiteMsgUserReq> req)
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