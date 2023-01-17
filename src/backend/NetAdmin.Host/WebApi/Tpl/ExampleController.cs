#if DEBUG
using NetAdmin.Application.Modules.Tpl;
using NetAdmin.Application.Services.Tpl.Dependency;
using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Tpl.Example;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.WebApi.Tpl;

/// <summary>
///     示例服务
/// </summary>
public class ExampleController : ControllerBase<IExampleService>, IExampleModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExampleController" /> class.
    /// </summary>
    public ExampleController(IExampleService service) //
        : base(service) { }

    /// <summary>
    ///     批量删除示例
    /// </summary>
    [Transaction]
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建示例
    /// </summary>
    [Transaction]
    public async Task<QueryExampleRsp> Create(CreateExampleReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除示例
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询示例
    /// </summary>
    public async Task<PagedQueryRsp<QueryExampleRsp>> PagedQuery(PagedQueryReq<QueryExampleReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询示例
    /// </summary>
    public async Task<List<QueryExampleRsp>> Query(QueryReq<QueryExampleReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新示例
    /// </summary>
    [Transaction]
    public async Task<QueryExampleRsp> Update(UpdateExampleReq req)
    {
        return await Service.Update(req);
    }
}
#endif