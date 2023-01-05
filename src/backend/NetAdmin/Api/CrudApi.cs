using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.Repositories;

namespace NetAdmin.Api;

/// <summary>
///     增删改查 Api基类
/// </summary>
public abstract class
    CrudApi<TEntity, TCreateReq, TUpdateReq, TQueryReq, TQueryRsp, TDelReq, TLogger> : ApiBase<TLogger>
    where TEntity : DataAbstraction, IEntity  //
    where TCreateReq : DataAbstraction, new() //
    where TUpdateReq : DataAbstraction, new() //
    where TQueryReq : DataAbstraction, new()  //
    where TQueryRsp : DataAbstraction         //
    where TDelReq : DataAbstraction, new()
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see cref="CrudApi{TEntity, TCreateReq, TUpdateReq, TQueryReq, TQueryRsp, TDelReq, TLogger}" /> class.
    /// </summary>
    protected CrudApi(Repository<TEntity> repository)
    {
        Repository = repository;
    }

    /// <summary>
    ///     默认仓储
    /// </summary>
    protected Repository<TEntity> Repository { get; }

    /// <summary>
    ///     创建实体
    /// </summary>
    public abstract Task Create(TCreateReq req);

    /// <summary>
    ///     删除实体
    /// </summary>
    public abstract Task<int> Delete(TDelReq req);

    /// <summary>
    ///     分页查询实体
    /// </summary>
    public abstract Task<PagedQueryRsp<TQueryRsp>> PagedQuery(PagedQueryReq<TQueryReq> req);

    /// <summary>
    ///     查询实体
    /// </summary>
    public abstract Task<List<TQueryRsp>> Query(QueryReq<TQueryReq> req);

    /// <summary>
    ///     更新实体
    /// </summary>
    public abstract Task<int> Update(TUpdateReq req);
}