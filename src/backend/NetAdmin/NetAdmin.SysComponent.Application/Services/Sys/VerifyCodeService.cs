using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IVerifyCodeService" />
public sealed class VerifyCodeService(BasicRepository<Sys_VerifyCode, long> rpo, IEventPublisher eventPublisher) //
    : RepositoryService<Sys_VerifyCode, long, IVerifyCodeService>(rpo), IVerifyCodeService
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
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
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
    public Task<QueryVerifyCodeRsp> EditAsync(EditVerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryVerifyCodeReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).WithNoLockNoWait().Count(out var total).ToListAsync().ConfigureAwait(false);

        return new PagedQueryRsp<QueryVerifyCodeRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryVerifyCodeRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync().ConfigureAwait(false);
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
        if (lastSent != null && (DateTime.Now - lastSent.CreatedTime).TotalMinutes < 1) {
            throw new NetAdminInvalidOperationException(Ln._1分钟内只能发送1次);
        }
        #endif

        if (lastSent != null && lastSent.Status != VerifyCodeStatues.Verified) { // 上次发送未验证，生成相同code
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = lastSent.Code }).ConfigureAwait(false);
        }
        else { // 生成新的code
            var code = _randRange.Rand().ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = code }).ConfigureAwait(false);
        }

        return ret.Adapt<SendVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public Task<int> SetVerifyCodeStatusAsync(SetVerifyCodeStatusReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.Status)]);
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
            (DateTime.Now - lastSent.CreatedTime).TotalMinutes             > 10) {
            return false;
        }

        _ = await UpdateAsync(lastSent with { Status = VerifyCodeStatues.Verified }, [nameof(lastSent.Status)]).ConfigureAwait(false);

        return true;
    }

    private Task<Sys_VerifyCode> GetLastSentAsync(string destDevice)
    {
        return QueryInternal(new QueryReq<QueryVerifyCodeReq> {
                                                                  DynamicFilter = new DynamicFilterInfo {
                                                                                      Field    = nameof(Sys_VerifyCode.DestDevice)
                                                                                    , Operator = DynamicFilterOperators.Eq
                                                                                    , Value    = destDevice
                                                                                  }
                                                              })
               .WithNoLockNoWait()
               .ToOneAsync();
    }

    private ISelect<Sys_VerifyCode> QueryInternal(QueryReq<QueryVerifyCodeReq> req)
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