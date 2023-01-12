using NetAdmin.DataContract;
using NetAdmin.DataContract.Dto.Pub;

namespace NetAdmin.Application.Service;

/// <summary>
///     增删改查服务接口
/// </summary>
/// <typeparam name="TCreateIn">创建请求类型</typeparam>
/// <typeparam name="TCreateOut">创建响应类型</typeparam>
/// <typeparam name="TQueryIn">查询请求类型</typeparam>
/// <typeparam name="TQueryOut">查询响应类型</typeparam>
/// <typeparam name="TUpdateIn">修改请求类型</typeparam>
/// <typeparam name="TUpdateOut">修改响应类型</typeparam>
/// <typeparam name="TDelIn">删除请求类型</typeparam>
public interface ICrudService<in TCreateIn, TCreateOut, TQueryIn, TQueryOut, in TUpdateIn, TUpdateOut, in TDelIn>
    where TCreateIn : DataAbstraction, new()
    where TCreateOut : DataAbstraction
    where TQueryIn : DataAbstraction, new()
    where TQueryOut : DataAbstraction
    where TUpdateIn : DataAbstraction, new()
    where TDelIn : DataAbstraction, new()
{
    /// <summary>
    ///     创建实体
    /// </summary>
    Task<TCreateOut> Create(TCreateIn @in);

    /// <summary>
    ///     删除实体
    /// </summary>
    Task<int> Delete(TDelIn req);

    /// <summary>
    ///     分页查询实体
    /// </summary>
    Task<PagedQueryRsp<TQueryOut>> PagedQuery(PagedQueryReq<TQueryIn> req);

    /// <summary>
    ///     查询实体
    /// </summary>
    Task<List<TQueryOut>> Query(QueryReq<TQueryIn> req);

    /// <summary>
    ///     更新实体
    /// </summary>
    Task<TUpdateOut> Update(TUpdateIn req);
}