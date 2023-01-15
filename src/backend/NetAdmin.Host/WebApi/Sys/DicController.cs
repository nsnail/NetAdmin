using NetAdmin.DataContract.Dto.Sys.Dic;
using NetAdmin.Host.Aop;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     字典服务
/// </summary>
public class DicController : ControllerBase<IDicService>, IDicModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicController" /> class.
    /// </summary>
    public DicController(IDicService service) //
        : base(service) { }

    /// <summary>
    ///     创建字典
    /// </summary>
    [Transaction]
    public ValueTask<QueryDicRsp> Create(CreateDicReq req)
    {
        return Service.Create(req);
    }

    /// <summary>
    ///     删除字典
    /// </summary>
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询字典
    /// </summary>
    public ValueTask<PagedQueryRsp<QueryDicRsp>> PagedQuery(PagedQueryReq<QueryDicReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询字典
    /// </summary>
    public ValueTask<List<QueryDicRsp>> Query(QueryReq<QueryDicReq> req)
    {
        return Service.Query(req);
    }

    /// <summary>
    ///     更新字典
    /// </summary>
    public ValueTask<QueryDicRsp> Update(UpdateDicReq req)
    {
        throw new NotImplementedException();
    }
}