using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Repositories;

namespace NetAdmin.Api;

/// <summary>
///     仓储接口基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TLogger">日志类型</typeparam>
public abstract class RepositoryApi<TEntity, TLogger> : ApiBase<TLogger>
    where TEntity : EntityBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RepositoryApi{TEntity, TLogger}" /> class.
    /// </summary>
    protected RepositoryApi(Repository<TEntity> repository)
    {
        Repository = repository;
    }

    /// <summary>
    ///     默认仓储
    /// </summary>
    protected Repository<TEntity> Repository { get; }
}