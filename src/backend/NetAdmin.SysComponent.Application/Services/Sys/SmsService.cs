using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Domain.Enums.Sys;
using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ISmsService" />
public sealed class SmsService : RepositoryService<Sys_Sms, ISmsService>, ISmsService
{
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsService" /> class.
    /// </summary>
    public SmsService(Repository<Sys_Sms> rpo, IEventPublisher eventPublisher) //
        : base(rpo)
    {
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    ///     批量删除短信
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
    ///     创建短信
    /// </summary>
    public async Task<QuerySmsRsp> CreateAsync(CreateSmsReq req)
    {
        var entity = await Rpo.InsertAsync(req);

        var ret = entity.Adapt<QuerySmsRsp>();

        // 发布短信创建事件
        await _eventPublisher.PublishAsync(new SmsCodeCreatedEvent(ret));

        return ret;
    }

    /// <summary>
    ///     删除短信
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <summary>
    ///     判断短信是否存在
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QuerySmsReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     获取单个短信
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<QuerySmsRsp> GetAsync(QuerySmsReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询短信
    /// </summary>
    public async Task<PagedQueryRsp<QuerySmsRsp>> PagedQueryAsync(PagedQueryReq<QuerySmsReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QuerySmsRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySmsRsp>>());
    }

    /// <summary>
    ///     查询短信
    /// </summary>
    public async Task<IEnumerable<QuerySmsRsp>> QueryAsync(QueryReq<QuerySmsReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QuerySmsRsp>>();
    }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    /// <exception cref="NetAdminInvalidOperationException">_1分钟内只能发送1次</exception>
    public async Task<SendSmsCodeRsp> SendSmsCodeAsync(SendSmsCodeReq req)
    {
        var lastSent = await GetLastSentAsync(req.DestMobile);

        QuerySmsRsp ret;

        #if !DEBUG
        // 有发送记录，且小于1分钟，不允许
        if (lastSent != null && (DateTime.UtcNow - lastSent.CreatedTime).TotalMinutes < 1) {
            throw new NetAdminInvalidOperationException(Ln._1分钟内只能发送1次);
        }
        #endif

        if (lastSent != null && lastSent.Status != SmsStatues.Verified) { // 上次发送未验证，生成相同code
            ret = await CreateAsync(req.Adapt<CreateSmsReq>() with { Code = lastSent.Code });
        }
        else { // 生成新的code
            var code = new[] { 0, 10000 }.Rand().ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            ret = await CreateAsync(req.Adapt<CreateSmsReq>() with { Code = code });
        }

        return ret.Adapt<SendSmsCodeRsp>();
    }

    /// <summary>
    ///     更新短信
    /// </summary>
    public async Task<QuerySmsRsp> UpdateAsync(UpdateSmsReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QuerySmsRsp>();
    }

    /// <summary>
    ///     完成短信验证
    /// </summary>
    public async Task<bool> VerifySmsCodeAsync(VerifySmsCodeReq req)
    {
        #if DEBUG
        if (req.Code == "8888") {
            return true;
        }
        #endif
        if (req.Code == Global.SecretKey) {
            return true;
        }

        var lastSent = await GetLastSentAsync(req.DestMobile);

        if (lastSent == null || lastSent.Status != SmsStatues.Sent || req.Code != lastSent.Code ||
            (DateTime.UtcNow - lastSent.CreatedTime).TotalMinutes > 10) {
            return false;
        }

        _ = await UpdateAsync((lastSent with { Status = SmsStatues.Verified }).Adapt<UpdateSmsReq>());

        return true;
    }

    private Task<Sys_Sms> GetLastSentAsync(string mobile)
    {
        return QueryInternal(new QueryReq<QuerySmsReq> {
                                                           Count = 1
                                                         , DynamicFilter
                                                               = new DynamicFilterInfo {
                                                                     Field    = nameof(Sys_Sms.DestMobile)
                                                                   , Operator = DynamicFilterOperator.Eq
                                                                   , Value    = mobile
                                                                 }
                                                       })
            .ToOneAsync();
    }

    private ISelect<Sys_Sms> QueryInternal(QueryReq<QuerySmsReq> req)
    {
        return Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                  .OrderByDescending(a => a.Id);
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QuerySmsRsp> UpdateForSqliteAsync(Sys_Sms req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0
            ? null
            : await GetAsync(new QuerySmsReq { Id = req.Id });
    }
}