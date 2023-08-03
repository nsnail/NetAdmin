using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Tpl.Example;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Tpl;
using NetAdmin.SysComponent.Application.Services.Tpl.Dependency;
using NetAdmin.SysComponent.Cache.Tpl.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Tpl;

/// <summary>
///     示例服务
/// </summary>
[ApiDescriptionSettings(nameof(Tpl), Module = nameof(Tpl))]
public sealed class ExampleController : ControllerBase<IExampleCache, IExampleService>, IExampleModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExampleController" /> class.
    /// </summary>
    public ExampleController(IExampleCache cache) //
        : base(cache) { }

    /// <summary>
    ///     批量删除示例
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
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
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryExampleReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个示例
    /// </summary>
    [NonAction]
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

    /// <summary>
    ///     更新示例
    /// </summary>
    [Transaction]
    public Task<QueryExampleRsp> UpdateAsync(UpdateExampleReq req)
    {
        return Cache.UpdateAsync(req);
    }
}