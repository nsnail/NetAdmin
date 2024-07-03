using NetAdmin.Domain;
using NetAdmin.Domain.Dto.Dependency;

namespace NetAdmin.Application.Modules;

/// <summary>
///     增删改查模块接口
/// </summary>
/// <typeparam name="TCreateReq">创建请求类型</typeparam>
/// <typeparam name="TCreateRsp">创建响应类型</typeparam>
/// <typeparam name="TQueryReq">查询请求类型</typeparam>
/// <typeparam name="TQueryRsp">查询响应类型</typeparam>
/// <typeparam name="TDelReq">删除请求类型</typeparam>
public interface ICrudModule<in TCreateReq, TCreateRsp, TQueryReq, TQueryRsp, TDelReq>
    where TCreateReq : DataAbstraction, new()
    where TCreateRsp : DataAbstraction
    where TQueryReq : DataAbstraction, new()
    where TQueryRsp : DataAbstraction
    where TDelReq : DataAbstraction, new()
{
    /// <summary>
    ///     批量删除实体
    /// </summary>
    Task<int> BulkDeleteAsync(BulkReq<TDelReq> req);

    /// <summary>
    ///     实体计数
    /// </summary>
    Task<long> CountAsync(QueryReq<TQueryReq> req);

    /// <summary>
    ///     创建实体
    /// </summary>
    Task<TCreateRsp> CreateAsync(TCreateReq req);

    /// <summary>
    ///     删除实体
    /// </summary>
    Task<int> DeleteAsync(TDelReq req);

    /// <summary>
    ///     判断实体是否存在
    /// </summary>
    Task<bool> ExistAsync(QueryReq<TQueryReq> req);

    /// <summary>
    ///     导出实体
    /// </summary>
    Task<IActionResult> ExportAsync(QueryReq<TQueryReq> req);

    /// <summary>
    ///     获取单个实体
    /// </summary>
    Task<TQueryRsp> GetAsync(TQueryReq req);

    /// <summary>
    ///     分页查询实体
    /// </summary>
    Task<PagedQueryRsp<TQueryRsp>> PagedQueryAsync(PagedQueryReq<TQueryReq> req);

    /// <summary>
    ///     查询实体
    /// </summary>
    Task<IEnumerable<TQueryRsp>> QueryAsync(QueryReq<TQueryReq> req);
}