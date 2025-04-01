using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Extensions;
using YourSolution.AdmServer.Application.Services.Adm.Dependency;
using YourSolution.AdmServer.Domain.DbMaps.Adm;
using YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

namespace YourSolution.AdmServer.Application.Services.Adm;

/// <inheritdoc cref="IGirlFriendsService" />
public sealed class GirlFriendsService(BasicRepository<Adm_GirlFriends, long> rpo) //
    : RepositoryService<Adm_GirlFriends, long, IGirlFriendsService>(rpo), IGirlFriendsService
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
    public Task<long> CountAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();

        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Adm_GirlFriends>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Adm_GirlFriends).GetProperty(y)!.GetValue(x.Key)!.ToString())
                            , x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryGirlFriendsRsp> CreateAsync(CreateGirlFriendsReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryGirlFriendsRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryGirlFriendsRsp> EditAsync(EditGirlFriendsReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryGirlFriendsRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryGirlFriendsReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryGirlFriendsReq, QueryGirlFriendsRsp>(QueryInternal, req, Ln.女朋友导出);
    }

    /// <inheritdoc />
    public async Task<QueryGirlFriendsRsp> GetAsync(QueryGirlFriendsReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryGirlFriendsReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryGirlFriendsRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryGirlFriendsRsp>> PagedQueryAsync(PagedQueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryGirlFriendsRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryGirlFriendsRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryGirlFriendsRsp>> QueryAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryGirlFriendsRsp>>();
    }

    private ISelect<Adm_GirlFriends> QueryInternal(QueryReq<QueryGirlFriendsReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).Where(a => !string.IsNullOrEmpty(a.Name))
            .WhereIf(
                req.Keywords?.Length > 0
                       , a => a.Id == req.Keywords.Int64Try(0) || a.Name.Contains(req.Keywords) || a.Interest.Contains(req.Keywords) || a.Seed.Contains(req.Keywords));

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