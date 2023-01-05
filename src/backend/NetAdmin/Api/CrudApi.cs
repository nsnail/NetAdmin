using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Repositories;

namespace NetAdmin.Api;

/// <summary>
///     增删改查 Api基类
/// </summary>
public abstract class CrudApi<TEntity, TLogger> : ApiBase<TLogger>
    where TEntity : DataAbstraction, IEntity, new()
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CrudApi{TEntity,TLogger}" /> class.
    /// </summary>
    protected CrudApi(Repository<TEntity> repository)
    {
        Repository = repository;
    }

    /// <summary>
    ///     默认仓储
    /// </summary>
    protected Repository<TEntity> Repository { get; }
}