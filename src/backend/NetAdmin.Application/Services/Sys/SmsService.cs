using System.Globalization;
using FreeSql;
using FreeSql.Internal.Model;
using Furion.EventBus;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Domain.Events;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="ISmsService" />
public class SmsService : RepositoryService<TbSysSms, ISmsService>, ISmsService
{
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsService" /> class.
    /// </summary>
    public SmsService(Repository<TbSysSms> rpo, IEventPublisher eventPublisher) //
        : base(rpo)
    {
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    ///     批量删除短信
    /// </summary>
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建短信
    /// </summary>
    public async Task<QuerySmsRsp> Create(CreateSmsReq req)
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
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <summary>
    ///     分页查询短信
    /// </summary>
    public async Task<PagedQueryRsp<QuerySmsRsp>> PagedQuery(PagedQueryReq<QuerySmsReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QuerySmsRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QuerySmsRsp>>());
    }

    /// <summary>
    ///     查询短信
    /// </summary>
    public async Task<IEnumerable<QuerySmsRsp>> Query(QueryReq<QuerySmsReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QuerySmsRsp>>();
    }

    /// <inheritdoc />
    public async Task<SendSmsCodeRsp> SendSmsCode(SendSmsCodeReq req)
    {
        var lastSent = await GetLastSent(req.DestMobile);

        QuerySmsRsp ret;

        // 有发送记录，且小于1分钟，不允许
        if (lastSent is not null && (DateTime.Now - lastSent.CreatedTime).TotalMinutes < 1) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Only_once_in_1_minute);
        }

        if (lastSent is not null && lastSent.Status != TbSysSms.Statues.Verified) { // 上次发送未验证，生成相同code
            ret = await Create(req.Adapt<CreateSmsReq>() with { Code = lastSent.Code });
        }
        else { // 生成新的code
            var code = new[] { 0, 10000 }.Rand().ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            ret = await Create(req.Adapt<CreateSmsReq>() with { Code = code });
        }

        return ret.Adapt<SendSmsCodeRsp>();
    }

    /// <summary>
    ///     更新短信
    /// </summary>
    public async Task<QuerySmsRsp> Update(UpdateSmsReq req)
    {
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqlite(req);
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QuerySmsRsp>();
    }

    /// <inheritdoc />
    public async Task<bool> VerifySmsCode(VerifySmsCodeReq req)
    {
        var lastSent = await GetLastSent(req.DestMobile);

        if (lastSent is null || lastSent.Status                != TbSysSms.Statues.Sent || req.Code != lastSent.Code ||
            (DateTime.Now - lastSent.CreatedTime).TotalMinutes > 10) {
            return false;
        }

        await Update((lastSent with { Status = TbSysSms.Statues.Verified }).Adapt<UpdateSmsReq>());

        return true;
    }

    private async Task<TbSysSms> GetLastSent(string mobile)
    {
        var ret = await QueryInternal(new QueryReq<QuerySmsReq> {
                                                                    Count = 1
                                                                  , DynamicFilter
                                                                        = new DynamicFilterInfo {
                                                                              Field    = nameof(TbSysSms.DestMobile)
                                                                            , Operator = DynamicFilterOperator.Eq
                                                                            , Value    = mobile
                                                                          }
                                                                  , Order = Enums.Orders.Descending
                                                                  , Prop  = nameof(TbSysSms.Id)
                                                                })
                        .ForUpdate()
                        .ToOneAsync();
        return ret;
    }

    private ISelect<TbSysSms> QueryInternal(QueryReq<QuerySmsReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }

    /// <summary>
    ///     非sqlite数据库请删掉
    /// </summary>
    private async Task<QuerySmsRsp> UpdateForSqlite(UpdateSmsReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            return null;
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QuerySmsRsp>();
    }
}