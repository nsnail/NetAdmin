using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRequestLogService" />
public sealed class RequestLogService
    (Repository<Sys_RequestLog> rpo) : RepositoryService<Sys_RequestLog, IRequestLogService>(rpo), IRequestLogService
{
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
    public async Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public async Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        var ret = await QueryInternal(new QueryReq<QueryRequestLogReq> { Filter = req }).ToOneAsync();
        return ret.Adapt<QueryRequestLogRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync(a => new {
                                                   a.ApiId
                                                 , ApiSummary = a.Api.Summary
                                                 , a.ExtraData
                                                 , a.CreatedClientIp
                                                 , a.CreatedTime
                                                 , a.CreatedUserName
                                                 , a.Duration
                                                 , a.Method
                                                 , a.CreatedUserAgent
                                                 , a.HttpStatusCode
                                                 , a.Id
                                               });

        return new PagedQueryRsp<QueryRequestLogRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryRequestLogRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryRequestLogRsp>>();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    protected override Task<Sys_RequestLog> UpdateForSqliteAsync(Sys_RequestLog req)
    {
        throw new NotImplementedException();
    }

    private ISelect<Sys_RequestLog> QueryInternal(QueryReq<QueryRequestLogReq> req)
    {
        var ret = Rpo.Select.Include(a => a.Api)
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}