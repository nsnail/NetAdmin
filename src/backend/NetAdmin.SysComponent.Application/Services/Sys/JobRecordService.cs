using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IJobRecordService" />
public sealed class JobRecordService(DefaultRepository<Sys_JobRecord> rpo) //
    : RepositoryService<Sys_JobRecord, IJobRecordService>(rpo), IJobRecordService
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
    public async Task<QueryJobRecordRsp> CreateAsync(CreateJobRecordReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryJobRecordRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryJobRecordRsp> GetAsync(QueryJobRecordReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryJobRecordReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryJobRecordRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryJobRecordRsp>(req.Page, req.PageSize, total
                                                  , list.Adapt<IEnumerable<QueryJobRecordRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryJobRecordRsp>> QueryAsync(QueryReq<QueryJobRecordReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryJobRecordRsp>>();
    }

    /// <inheritdoc />
    public async Task<QueryJobRecordRsp> UpdateAsync(UpdateJobRecordReq req)
    {
        req.ThrowIfInvalid();
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(req).ConfigureAwait(false) as QueryJobRecordRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(req).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QueryJobRecordRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_JobRecord> UpdateForSqliteAsync(Sys_JobRecord req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QueryJobRecordReq { Id = req.Id }).ConfigureAwait(false);
    }

    private ISelect<Sys_JobRecord> QueryInternal(QueryReq<QueryJobRecordReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.JobId == req.Keywords.Int64Try(0) || a.Id == req.Keywords.Int64Try(0))
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}