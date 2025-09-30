using System.Net.Http.Headers;
using Ganss.Excel;
using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserWallet;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserWalletService" />
public sealed class UserWalletService(BasicRepository<Sys_UserWallet, long> rpo)
    : RepositoryService<Sys_UserWallet, long, IUserWalletService>(rpo), IUserWalletService
{
    private readonly Expression<Func<Sys_UserWallet, Sys_UserWallet>> _toListExp = a => new Sys_UserWallet
    {
        Id = a.Id
        , CreatedTime = a.CreatedTime
        , ModifiedTime = a.ModifiedTime
        , Version = a.Version
        , Owner = new Sys_User
        {
            Id = a.Owner.Id
            , DeptId = a.Owner.DeptId
            , UserName = a.Owner.UserName
            , Avatar = a.Owner.Avatar
            , Roles = a.Owner.Roles
            , Invite = new Sys_UserInvite
            {
                Owner = new Sys_User
                {
                    Id = a.Owner.Invite.Owner.Id, UserName = a.Owner.Invite.Owner.UserName, Avatar = a.Owner.Invite.Owner.Avatar
                }
                , Channel
                    = new Sys_User
                    {
                        Id = a.Owner.Invite.Channel.Id, UserName = a.Owner.Invite.Channel.UserName, Avatar = a.Owner.Invite.Channel.Avatar
                    }
            }
        }
        , AvailableBalance = a.AvailableBalance
        , FrozenBalance = a.FrozenBalance
        , TotalExpenditure = a.TotalExpenditure
        , TotalIncome = a.TotalIncome
    };

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
    public async Task<long> CountAsync(QueryReq<QueryUserWalletReq> req) {
        req.ThrowIfInvalid();
        return await (await QueryInternalComplexAsync(req).ConfigureAwait(false)).WithNoLockNoWait().CountAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserWalletReq> req) {
        req.ThrowIfInvalid();
        var ret = await (await QueryInternalComplexAsync(req with { Order = Orders.None }).ConfigureAwait(false))
            .WithNoLockNoWait()
            .GroupBy(req.GetToListExp<Sys_UserWallet>())
            .ToDictionaryAsync(a => a.Count())
            .ConfigureAwait(false);
        return ret
            .Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                    req.RequiredFields.ToImmutableDictionary(y => y, y => typeof(Sys_UserWallet).GetProperty(y)!.GetValue(x.Key)?.ToString()), x.Value
                )
            )
            .Where(x => x.Key.Any(y => !y.Value.NullOrEmpty()))
            .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryUserWalletRsp> CreateAsync(CreateUserWalletReq req) {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryUserWalletRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req) {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryUserWalletRsp> EditAsync(EditUserWalletReq req) {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req, [nameof(req.FrozenBalance), nameof(req.AvailableBalance)]).ConfigureAwait(false))
            .FirstOrDefault()
            ?.Adapt<QueryUserWalletRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryUserWalletReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public async Task<IActionResult> ExportAsync(QueryReq<QueryUserWalletReq> req) {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req, null)
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .IncludeMany(a => a.Owner.Roles.Select(b => new Sys_Role { Id = b.Id, Name = b.Name }))
            .WithNoLockNoWait()
            .Take(Numbers.MAX_LIMIT_EXPORT)
            .ToListAsync(_toListExp)
            .ConfigureAwait(false);

        var stream = new MemoryStream();
        await new ExcelMapper()
            .SaveAsync(
                stream
                , list.ConvertAll(x => new ExportUserWalletRsp
                    {
                        冻结余额 = (x.FrozenBalance / 100m).Round(2)
                        , 归属角色 = x.Owner.Roles.Select(y => y.Name).Join(",")
                        , 归属用户 = x.Owner.UserName
                        , 可用余额 = (x.AvailableBalance / 100m).Round(2)
                        , 钱包编号 = x.Id.ToInvString()
                        , 上级 = x.Owner.Invite.Owner?.UserName
                        , 渠道 = x.Owner.Invite.Channel?.UserName
                        , 总收入 = (x.TotalIncome / 100m).Round(2)
                        , 总支出 = (x.TotalExpenditure / 100m).Round(2)
                        , 最后交易时间 = x.ModifiedTime?.yyyy_MM_dd_HH_mm_ss()
                    }
                )
            )
            .ConfigureAwait(false);
        _ = stream.Seek(0, SeekOrigin.Begin);
        App.HttpContext.Response.Headers.ContentDisposition = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT)
        {
            FileNameStar = $"钱包{list.Count}个-{list.Min(x => x.CreatedTime):MMddHHmm}-{list.Max(x => x.
                CreatedTime):MMddHHmm}.xlsx"
        }.ToString();
        return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

    /// <inheritdoc />
    public async Task<QueryUserWalletRsp> GetAsync(QueryUserWalletReq req) {
        req.ThrowIfInvalid();
        var ret
            = await (await QueryInternalComplexAsync(new QueryReq<QueryUserWalletReq> { Filter = req, Order = Orders.None }).ConfigureAwait(false))
                .IncludeMany(a => a.Owner.Roles.Select(b => new Sys_Role { Id = b.Id, Name = b.Name }))
                .Include(a => a.Owner.Invite.Owner)
                .Include(a => a.Owner.Invite.Channel)
                .ToOneAsync(_toListExp)
                .ConfigureAwait(false);
        return ret.Adapt<QueryUserWalletRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserWalletRsp>> PagedQueryAsync(PagedQueryReq<QueryUserWalletReq> req) {
        req.ThrowIfInvalid();
        var list = await (await QueryInternalComplexAsync(req).ConfigureAwait(false))
            .IncludeMany(a => a.Owner.Roles.Select(b => new Sys_Role { Id = b.Id, Name = b.Name }))
            .Include(a => a.Owner.Invite.Owner)
            .Include(a => a.Owner.Invite.Channel)
            .Page(req.Page, req.PageSize)
            .WithNoLockNoWait()
            .Count(out var total)
            .ToListAsync(_toListExp)
            .ConfigureAwait(false);

        return new PagedQueryRsp<QueryUserWalletRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserWalletRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserWalletRsp>> QueryAsync(QueryReq<QueryUserWalletReq> req) {
        req.ThrowIfInvalid();
        var ret = await (await QueryInternalComplexAsync(req).ConfigureAwait(false))
            .WithNoLockNoWait()
            .Take(req.Count)
            .ToListAsync(req)
            .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryUserWalletRsp>>();
    }

    /// <inheritdoc />
    public async Task<decimal> SumAsync(QueryReq<QueryUserWalletReq> req) {
        req.ThrowIfInvalid();
        return await (await QueryInternalComplexAsync(req with { Order = Orders.None }).ConfigureAwait(false))
            .WithNoLockNoWait()
            .SumAsync(req.GetSumExp<Sys_UserWallet>())
            .ConfigureAwait(false);
    }

    private ISelect<Sys_UserWallet> QueryInternal(
        QueryReq<QueryUserWalletReq> req
        , IEnumerable<long> deptIds
    ) {
        var ret = Rpo
            .Select.WhereDynamicFilter(req.DynamicFilter)
            .WhereIf(req.Filter?.Id > 0, a => a.Id == req.Filter.Id)
            .WhereIf(deptIds != null, a => deptIds.Contains(a.Owner.DeptId))
            .WhereIf(req.Filter?.RoleId > 0, a => a.Owner.Roles.Any(b => b.Id == req.Filter.RoleId));

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret.AppendOtherFilters(req);
            case Orders.Random:
                return ret.OrderByRandom().AppendOtherFilters(req);
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret.AppendOtherFilters(req);
    }

    private async Task<ISelect<Sys_UserWallet>> QueryInternalComplexAsync(QueryReq<QueryUserWalletReq> req) {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Rpo.Orm.Select<Sys_Dept>().Where(a => a.Id == req.Filter.DeptId).AsTreeCte().ToListAsync(a => a.Id).ConfigureAwait(false);
        }

        return QueryInternal(req, deptIds);
    }
}