using NetAdmin.DataContract.Dto.Dependency;

namespace NetAdmin.Application.Modules;

/// <summary>
///     增删改查模块接口
/// </summary>
/// <typeparam name="TCreateReq">创建请求类型</typeparam>
/// <typeparam name="TCreateRsp">创建响应类型</typeparam>
/// <typeparam name="TQueryReq">查询请求类型</typeparam>
/// <typeparam name="TQueryRsp">查询响应类型</typeparam>
/// <typeparam name="TUpdateReq">修改请求类型</typeparam>
/// <typeparam name="TUpdateRsp">修改响应类型</typeparam>
/// <typeparam name="TDelReq">删除请求类型</typeparam>
public interface ICrudModule<in TCreateReq, TCreateRsp, TQueryReq, TQueryRsp, in TUpdateReq, TUpdateRsp, TDelReq>
    where TCreateReq : DataAbstraction, new()
    where TCreateRsp : DataAbstraction
    where TQueryReq : DataAbstraction, new()
    where TQueryRsp : DataAbstraction
    where TUpdateReq : DataAbstraction, new()
    where TUpdateRsp : DataAbstraction
    where TDelReq : DataAbstraction, new()
{
    /// <summary>
    ///     批量删除实体
    /// </summary>
    Task<int> BulkDelete(BulkReq<TDelReq> req);

    /// <summary>
    ///     创建实体
    /// </summary>
    Task<TCreateRsp> Create(TCreateReq req);

    /// <summary>
    ///     删除实体
    /// </summary>
    Task<int> Delete(TDelReq req);

    /// <summary>
    ///     分页查询实体
    /// </summary>
    Task<PagedQueryRsp<TQueryRsp>> PagedQuery(PagedQueryReq<TQueryReq> req);

    /// <summary>
    ///     查询实体
    /// </summary>
    Task<List<TQueryRsp>> Query(QueryReq<TQueryReq> req);

    /// <summary>
    ///     更新实体
    /// </summary>
    Task<TUpdateRsp> Update(TUpdateReq req);
}