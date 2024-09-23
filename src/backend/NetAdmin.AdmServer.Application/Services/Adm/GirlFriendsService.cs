using NetAdmin.AdmServer.Application.Services.Adm.Dependency;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Adm;
using NetAdmin.Domain.Dto.Adm.GirlFriends;
using NetAdmin.Domain.Dto.Dependency;

namespace NetAdmin.AdmServer.Application.Services.Adm;

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
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
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

        var entity     = req.Adapt<Adm_GirlFriends>();
        var updateCols = new string[] { nameof(req.RealName), nameof(req.Age), nameof(req.Height), nameof(req.Hobby) };
        var ignoreCols = new string[] { nameof(req.Version) };
        var count = await UpdateAsync(entity, updateCols, ignoreCols, a =>
                                  a.Id == req.Id, null, true).ConfigureAwait(false);
        Console.WriteLine(count);

        return (await QueryAsync(new QueryReq<QueryGirlFriendsReq> { Filter = new QueryGirlFriendsReq { Id = req.Id } })
            .ConfigureAwait(false)).First();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
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
        var ret = await QueryInternal(new QueryReq<QueryGirlFriendsReq> { Filter = req, Order = Orders.None })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryGirlFriendsRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryGirlFriendsRsp>> PagedQueryAsync(PagedQueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryGirlFriendsRsp>(req.Page, req.PageSize, total
                                                    , list.Adapt<IEnumerable<QueryGirlFriendsRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryGirlFriendsRsp>> QueryAsync(QueryReq<QueryGirlFriendsReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryGirlFriendsRsp>>();
    }

    private ISelect<Adm_GirlFriends> QueryInternal(QueryReq<QueryGirlFriendsReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf(!string.IsNullOrWhiteSpace(req.Keywords), a =>
                                  a.Id == req.Keywords.Int64Try(0) || a.RealName == req.Keywords);

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