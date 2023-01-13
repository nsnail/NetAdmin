using NetAdmin.Application.Repositories;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.Application.Service;

/// <summary>
///     仓储服务基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TLogger">日志类型</typeparam>
public abstract class RepositoryService<TEntity, TLogger> : ServiceBase<TLogger>
    where TEntity : EntityBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RepositoryService{TEntity, TLogger}" /> class.
    /// </summary>
    protected RepositoryService(Repository<TEntity> rpo) //
    {
        Rpo = rpo;
    }

    /// <summary>
    ///     默认仓储
    /// </summary>
    protected Repository<TEntity> Rpo { get; }

    /// <summary>
    ///     启用级联保存
    /// </summary>
    protected bool EnableCascadeSave {
        get => Rpo.DbContextOptions.EnableCascadeSave;
        set => Rpo.DbContextOptions.EnableCascadeSave = value;
    }
}