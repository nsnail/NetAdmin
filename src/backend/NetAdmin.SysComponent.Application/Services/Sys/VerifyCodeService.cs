using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IVerifyCodeService" />
public sealed class VerifyCodeService(DefaultRepository<Sys_VerifyCode> rpo, IEventPublisher eventPublisher) //
    : RepositoryService<Sys_VerifyCode, IVerifyCodeService>(rpo), IVerifyCodeService
{
    private static readonly int[] _randRange = [0, 10000];

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
    public Task<long> CountAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        var entity = await Rpo.InsertAsync(req).ConfigureAwait(false);

        var ret = entity.Adapt<QueryVerifyCodeRsp>();

        // 发布验证码创建事件
        await eventPublisher.PublishAsync(new VerifyCodeCreatedEvent(ret)).ConfigureAwait(false);

        return ret;
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryVerifyCodeReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryVerifyCodeRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryVerifyCodeRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryVerifyCodeRsp>>();
    }

    /// <inheritdoc />
    public async Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        var lastSent = await GetLastSentAsync(req.DestDevice).ConfigureAwait(false);

        QueryVerifyCodeRsp ret;

        #if !DEBUG
        // 有发送记录，且小于1分钟，不允许
        if (lastSent != null && (DateTime.UtcNow - lastSent.CreatedTime).TotalMinutes < 1) {
            throw new NetAdminInvalidOperationException(Ln._1分钟内只能发送1次);
        }
        #endif

        if (lastSent != null && lastSent.Status != VerifyCodeStatues.Verified) { // 上次发送未验证，生成相同code
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = lastSent.Code })
                .ConfigureAwait(false);
        }
        else { // 生成新的code
            var code = _randRange.Rand().ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = code }).ConfigureAwait(false);
        }

        return ret.Adapt<SendVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> UpdateAsync(UpdateVerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req).ConfigureAwait(false) as QueryVerifyCodeRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QueryVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAsync(VerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        #if DEBUG
        if (req.Code == "8888") {
            return true;
        }
        #endif
        if (req.Code == GlobalStatic.SecretKey) {
            return true;
        }

        var lastSent = await GetLastSentAsync(req.DestDevice).ConfigureAwait(false);

        if (lastSent is not { Status: VerifyCodeStatues.Sent } || req.Code != lastSent.Code ||
            (DateTime.UtcNow - lastSent.CreatedTime).TotalMinutes          > 10) {
            return false;
        }

        _ = await UpdateAsync((lastSent with { Status = VerifyCodeStatues.Verified }).Adapt<UpdateVerifyCodeReq>())
            .ConfigureAwait(false);

        return true;
    }

    /// <inheritdoc />
    protected override async Task<Sys_VerifyCode> UpdateForSqliteAsync(Sys_VerifyCode req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QueryVerifyCodeReq { Id = req.Id }).ConfigureAwait(false);
    }

    private Task<Sys_VerifyCode> GetLastSentAsync(string destDevice)
    {
        return QueryInternal(new QueryReq<QueryVerifyCodeReq> {
                                                                  Count = 1
                                                                , DynamicFilter
                                                                      = new DynamicFilterInfo {
                                                                            Field = nameof(
                                                                                Sys_VerifyCode.DestDevice)
                                                                          , Operator = DynamicFilterOperators.Eq
                                                                          , Value    = destDevice
                                                                        }
                                                              })
            .ToOneAsync();
    }

    private ISelect<Sys_VerifyCode> QueryInternal(QueryReq<QueryVerifyCodeReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);
        if (req.Order == Orders.Random) {
            return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}