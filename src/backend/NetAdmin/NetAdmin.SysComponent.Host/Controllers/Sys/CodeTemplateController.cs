using NetAdmin.Domain.Dto.Sys.CodeTemplate;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     代码模板服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class CodeTemplateController(ICodeTemplateCache cache)
    : ControllerBase<ICodeTemplateCache, ICodeTemplateService>(cache), ICodeTemplateModule
{
    /// <summary>
    ///     批量删除代码模板
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     代码模板计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryCodeTemplateReq> req) {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     代码模板分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryCodeTemplateReq> req) {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建代码模板
    /// </summary>
    [Transaction]
    public Task<QueryCodeTemplateRsp> CreateAsync(CreateCodeTemplateReq req) {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除代码模板
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req) {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑代码模板
    /// </summary>
    [Transaction]
    public Task<QueryCodeTemplateRsp> EditAsync(EditCodeTemplateReq req) {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出代码模板
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryCodeTemplateReq> req) {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个代码模板
    /// </summary>
    public Task<QueryCodeTemplateRsp> GetAsync(QueryCodeTemplateReq req) {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询代码模板
    /// </summary>
    public Task<PagedQueryRsp<QueryCodeTemplateRsp>> PagedQueryAsync(PagedQueryReq<QueryCodeTemplateReq> req) {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询代码模板
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryCodeTemplateRsp>> QueryAsync(QueryReq<QueryCodeTemplateReq> req) {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     代码模板求和
    /// </summary>
    [NonAction]
    public Task<decimal> SumAsync(QueryReq<QueryCodeTemplateReq> req) {
        return Cache.SumAsync(req);
    }
}