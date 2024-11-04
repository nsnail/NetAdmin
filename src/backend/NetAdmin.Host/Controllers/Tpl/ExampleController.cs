using NetAdmin.Application.Modules.Tpl;
using NetAdmin.Application.Services.Tpl.Dependency;
using NetAdmin.Cache.Tpl.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Tpl.Example;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.Controllers.Tpl;

/// <summary>
///     示例服务
/// </summary>
[ApiDescriptionSettings(nameof(Tpl), Module = nameof(Tpl))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class ExampleController(IExampleCache cache) : ControllerBase<IExampleCache, IExampleService>(cache), IExampleModule
{
    /// <summary>
    ///     批量删除示例
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     示例计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryExampleReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建示例
    /// </summary>
    [Transaction]
    public Task<QueryExampleRsp> CreateAsync(CreateExampleReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除示例
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     示例是否存在
    /// </summary>
    public Task<bool> ExistAsync(QueryReq<QueryExampleReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     导出示例
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryExampleReq> req)
    {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个示例
    /// </summary>
    public Task<QueryExampleRsp> GetAsync(QueryExampleReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询示例
    /// </summary>
    public Task<PagedQueryRsp<QueryExampleRsp>> PagedQueryAsync(PagedQueryReq<QueryExampleReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询示例
    /// </summary>
    public Task<IEnumerable<QueryExampleRsp>> QueryAsync(QueryReq<QueryExampleReq> req)
    {
        return Cache.QueryAsync(req);
    }
}