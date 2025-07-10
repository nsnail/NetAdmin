using NetAdmin.Domain.Dto.Sys.CodeTemplate;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ICodeTemplateCache" />
public sealed class CodeTemplateCache(IDistributedCache cache, ICodeTemplateService service)
    : DistributedCache<ICodeTemplateService>(cache, service), IScoped, ICodeTemplateCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        return Service.CountByAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryCodeTemplateRsp> CreateAsync(CreateCodeTemplateReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryCodeTemplateRsp> EditAsync(EditCodeTemplateReq req)
    {
        return Service.EditAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryCodeTemplateRsp> GetAsync(QueryCodeTemplateReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryCodeTemplateRsp>> PagedQueryAsync(PagedQueryReq<QueryCodeTemplateReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryCodeTemplateRsp>> QueryAsync(QueryReq<QueryCodeTemplateReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetCodeTemplateEnabledReq req)
    {
        return Service.SetEnabledAsync(req);
    }
}