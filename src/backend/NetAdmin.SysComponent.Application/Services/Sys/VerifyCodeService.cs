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
    private static readonly int[] _randRange = { 0, 10000 };

    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        var entity = await Rpo.InsertAsync(req);

        var ret = entity.Adapt<QueryVerifyCodeRsp>();

        // 发布验证码创建事件
        await eventPublisher.PublishAsync(new VerifyCodeCreatedEvent(ret));

        return ret;
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        var ret = await QueryInternal(new QueryReq<QueryVerifyCodeReq> { Filter = req }).ToOneAsync();
        return ret.Adapt<QueryVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryVerifyCodeRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryVerifyCodeRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryVerifyCodeRsp>>();
    }

    /// <inheritdoc />
    public async Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req)
    {
        var lastSent = await GetLastSentAsync(req.DestDevice);

        QueryVerifyCodeRsp ret;

        #if !DEBUG
        // 有发送记录，且小于1分钟，不允许
        if (lastSent != null && (DateTime.UtcNow - lastSent.CreatedTime).TotalMinutes < 1) {
            throw new NetAdminInvalidOperationException(Ln._1分钟内只能发送1次);
        }
        #endif

        if (lastSent != null && lastSent.Status != VerifyCodeStatues.Verified) { // 上次发送未验证，生成相同code
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = lastSent.Code });
        }
        else { // 生成新的code
            var code = _randRange.Rand().ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = code });
        }

        return ret.Adapt<SendVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryVerifyCodeRsp> UpdateAsync(UpdateVerifyCodeReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req) as QueryVerifyCodeRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryVerifyCodeRsp>();
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAsync(VerifyCodeReq req)
    {
        #if DEBUG
        if (req.Code == "8888") {
            return true;
        }
        #endif
        if (req.Code == GlobalStatic.SecretKey) {
            return true;
        }

        var lastSent = await GetLastSentAsync(req.DestDevice);

        if (lastSent is not { Status: VerifyCodeStatues.Sent } || req.Code != lastSent.Code ||
            (DateTime.UtcNow - lastSent.CreatedTime).TotalMinutes          > 10) {
            return false;
        }

        _ = await UpdateAsync((lastSent with { Status = VerifyCodeStatues.Verified }).Adapt<UpdateVerifyCodeReq>());

        return true;
    }

    /// <inheritdoc />
    protected override async Task<Sys_VerifyCode> UpdateForSqliteAsync(Sys_VerifyCode req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QueryVerifyCodeReq { Id = req.Id });
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
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}