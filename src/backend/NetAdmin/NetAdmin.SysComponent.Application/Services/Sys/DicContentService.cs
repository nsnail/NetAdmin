using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicContentService" />
public sealed class DicContentService(BasicRepository<Sys_DicContent, long> rpo) //
    : RepositoryService<Sys_DicContent, long, IDicContentService>(rpo), IDicContentService
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
    public Task<long> CountAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_DicContent>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y, y => typeof(Sys_DicContent).GetProperty(y)!.GetValue(x.Key)!.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    public async Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DicCatalog>().Where(a => a.Id == req.CatalogId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    public async Task<QueryDicContentRsp> EditAsync(EditDicContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DicCatalog>().Where(a => a.Id == req.CatalogId).WithNoLockNoWait().AnyAsync().ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryDicContentRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0 ? await GetAsync(new QueryDicContentReq { Id = req.Id }).ConfigureAwait(false) : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryDicContentReq, ExportDicContentRsp>(QueryInternal, req, Ln.字典内容导出);
    }

    /// <inheritdoc />
    public async Task<QueryDicContentRsp> GetAsync(QueryDicContentReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDicContentReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryDicContentRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryDicContentRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDicContentRsp>> QueryAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDicContentRsp>>();
    }

    /// <inheritdoc />
    public async Task<List<QueryDicContentRsp>> QueryByCatalogCodeAsync(string catalogCode)
    {
        var ret = await Rpo.Orm.Select<Sys_DicContent>()
                           .WithNoLockNoWait()
                           .Include(a => a.Catalog)
                           .Where(a => a.Catalog.Code == catalogCode)
                           .Where(a => a.Enabled)
                           .ToListAsync()
                           .ConfigureAwait(false);
        return ret.Adapt<List<QueryDicContentRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDicContentEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(Sys_DicContent.Enabled)]);
    }

    private ISelect<Sys_DicContent> QueryInternal(QueryReq<QueryDicContentReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.Key.Contains(req.Keywords) || a.Value.Contains(req.Keywords) || a.Summary.Contains(req.Keywords));

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret.AppendOtherFilters(req);
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret.AppendOtherFilters(req);
    }
}