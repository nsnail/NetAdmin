using NetAdmin.Application.Extensions;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.CodeTemplate;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICodeTemplateService" />
public sealed class CodeTemplateService(BasicRepository<Sys_CodeTemplate, long> rpo) //
    : RepositoryService<Sys_CodeTemplate, long, ICodeTemplateService>(rpo), ICodeTemplateService
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
    public Task<long> CountAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
    }

    /// <inheritdoc />
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req with { Order = Orders.None })
                        .WithNoLockNoWait()
                        .GroupBy(req.GetToListExp<Sys_CodeTemplate>())
                        .ToDictionaryAsync(a => a.Count())
                        .ConfigureAwait(false);
        return ret.Select(x => new KeyValuePair<IImmutableDictionary<string, string>, int>(
                              req.RequiredFields.ToImmutableDictionary(
                                  y => y
                                , y => y.Contains('.')
                                      ? typeof(Sys_CodeTemplate).GetRecursiveProperty(y)!.GetValue(
                                                                    x.Key.GetType().GetRecursiveProperty(y[..y.LastIndexOf('.')]).GetValue(x.Key))
                                                                ?.ToString()
                                      : typeof(Sys_CodeTemplate).GetProperty(y)!.GetValue(x.Key)?.ToString()), x.Value))
                  .OrderByDescending(x => x.Value);
    }

    /// <inheritdoc />
    public async Task<QueryCodeTemplateRsp> CreateAsync(CreateCodeTemplateReq req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryCodeTemplateRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public async Task<QueryCodeTemplateRsp> EditAsync(EditCodeTemplateReq req)
    {
        req.ThrowIfInvalid();
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryCodeTemplateRsp>();
        #else
        return await UpdateAsync(req).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryCodeTemplateReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryCodeTemplateReq, QueryCodeTemplateRsp>(QueryInternal, req, Ln.代码模板导出);
    }

    /// <inheritdoc />
    public async Task<QueryCodeTemplateRsp> GetAsync(QueryCodeTemplateReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryCodeTemplateReq> { Filter = req, Order = Orders.None }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryCodeTemplateRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryCodeTemplateRsp>> PagedQueryAsync(PagedQueryReq<QueryCodeTemplateReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .WithNoLockNoWait()
                         .Count(out var total)
                         .ToListAsync(req)
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryCodeTemplateRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryCodeTemplateRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryCodeTemplateRsp>> QueryAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req).WithNoLockNoWait().Take(req.Count).ToListAsync(req).ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryCodeTemplateRsp>>();
    }

    /// <inheritdoc />
    public Task<decimal> SumAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req with { Order = Orders.None }).WithNoLockNoWait().SumAsync(req.GetSumExp<Sys_CodeTemplate>());
    }

    private ISelect<Sys_CodeTemplate> QueryInternal(QueryReq<QueryCodeTemplateReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);

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