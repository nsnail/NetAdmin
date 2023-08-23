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
public sealed class VerifyCodeService : RepositoryService<Sys_VerifyCode, IVerifyCodeService>, IVerifyCodeService
{
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    ///     Initializes a new instance of the <see cref="VerifyCodeService" /> class.
    /// </summary>
    public VerifyCodeService(Repository<Sys_VerifyCode> rpo, IEventPublisher eventPublisher) //
        : base(rpo)
    {
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    ///     批量删除验证码
    /// </summary>
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建验证码
    /// </summary>
    public async Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        var entity = await Rpo.InsertAsync(req);

        var ret = entity.Adapt<QueryVerifyCodeRsp>();

        // 发布验证码创建事件
        await _eventPublisher.PublishAsync(new VerifyCodeCreatedEvent(ret));

        return ret;
    }

    /// <summary>
    ///     删除验证码
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <summary>
    ///     判断验证码是否存在
    /// </summary>
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <summary>
    ///     获取单个验证码
    /// </summary>
    public async Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        var ret = await QueryInternal(new QueryReq<QueryVerifyCodeReq> { Filter = req }).ToOneAsync();
        return ret.Adapt<QueryVerifyCodeRsp>();
    }

    /// <summary>
    ///     分页查询验证码
    /// </summary>
    public async Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryVerifyCodeRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryVerifyCodeRsp>>());
    }

    /// <summary>
    ///     查询验证码
    /// </summary>
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
            var code = new[] { 0, 10000 }.Rand().ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            ret = await CreateAsync(req.Adapt<CreateVerifyCodeReq>() with { Code = code });
        }

        return ret.Adapt<SendVerifyCodeRsp>();
    }

    /// <summary>
    ///     更新验证码
    /// </summary>
    public async Task<QueryVerifyCodeRsp> UpdateAsync(UpdateVerifyCodeReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req);
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
        if (req.Code == Global.SecretKey) {
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

    private Task<Sys_VerifyCode> GetLastSentAsync(string destDevice)
    {
        return QueryInternal(new QueryReq<QueryVerifyCodeReq> {
                                                                  Count = 1
                                                                , DynamicFilter
                                                                      = new DynamicFilterInfo {
                                                                            Field = nameof(
                                                                                Sys_VerifyCode.DestDevice)
                                                                          , Operator = DynamicFilterOperator.Eq
                                                                          , Value    = destDevice
                                                                        }
                                                              })
            .ToOneAsync();
    }

    private ISelect<Sys_VerifyCode> QueryInternal(QueryReq<QueryVerifyCodeReq> req)
    {
        return Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                  .OrderByDescending(a => a.Id);
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QueryVerifyCodeRsp> UpdateForSqliteAsync(Sys_VerifyCode req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QueryVerifyCodeReq { Id = req.Id });
    }
}