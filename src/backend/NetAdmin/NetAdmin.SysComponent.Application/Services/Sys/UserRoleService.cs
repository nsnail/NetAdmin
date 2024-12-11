using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserRole;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserRoleService" />
public sealed class UserRoleService(BasicRepository<Sys_UserRole, long> rpo) //
    : RepositoryService<Sys_UserRole, long, IUserRoleService>(rpo), IUserRoleService
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
    public Task<long> CountAsync(QueryReq<QueryUserRoleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserRoleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_UserRole>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_UserRole).GetProperty(y)!.GetValue(x.Key)!.ToString())
                            , x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryUserRoleRsp> CreateAsync(CreateUserRoleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryUserRoleRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<QueryUserRoleRsp> EditAsync(EditUserRoleReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserRoleReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QueryUserRoleRsp> GetAsync(QueryUserRoleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryUserRoleReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryUserRoleRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryUserRoleReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req.GetToListExp<Sys_UserRole>() ?? (a => a))
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryUserRoleRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserRoleRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserRoleRsp>> QueryAsync(QueryReq<QueryUserRoleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        .WithNoLockNoWait()
                        .Take(req.Count)
                        .ToListAsync(req.GetToListExp<Sys_UserRole>() ?? (a => a))
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryUserRoleRsp>>();
    }

    private ISelect<Sys_UserRole> QueryInternal(QueryReq<QueryUserRoleReq> req)
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