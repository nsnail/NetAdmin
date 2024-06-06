using NetAdmin.Application.Repositories;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Application.Services;

/// <summary>
///     仓储服务基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TPrimary">主键类型</typeparam>
/// <typeparam name="TLogger">日志类型</typeparam>
public abstract class RepositoryService<TEntity, TPrimary, TLogger>(BasicRepository<TEntity, TPrimary> rpo)
    : ServiceBase<TLogger>
    where TEntity : EntityBase<TPrimary> //
    where TPrimary : IEquatable<TPrimary>
{
    /// <summary>
    ///     默认仓储
    /// </summary>
    protected BasicRepository<TEntity, TPrimary> Rpo => rpo;

    /// <summary>
    ///     启用级联保存
    /// </summary>
    protected bool EnableCascadeSave {
        get => Rpo.DbContextOptions.EnableCascadeSave;
        set => Rpo.DbContextOptions.EnableCascadeSave = value;
    }

    /// <summary>
    ///     更新实体
    /// </summary>
    protected Task<int> UpdateAsync(TEntity dto, IEnumerable<string> includeFields, string[] excludeFields = null
                                  , Expression<Func<TEntity, bool>> whereExp = null)
    {
        whereExp ??= a => a.Id.Equals(dto.Id);
        var update = BuildUpdate(dto, includeFields, excludeFields).Where(whereExp);

        return update.ExecuteAffrowsAsync();
    }
    #if DBTYPE_SQLSERVER
    /// <summary>
    ///     更新实体
    /// </summary>
    protected Task<List<TEntity>> UpdateEntityAsync(TEntity dto, IEnumerable<string> includeFields
                                                  , string[] excludeFields = null
                                                  , Expression<Func<TEntity, bool>> whereExp = null)
    {
        whereExp ??= a => a.Id.Equals(dto.Id);
        return BuildUpdate(dto, includeFields, excludeFields).Where(whereExp).ExecuteUpdatedAsync();
    }
    #endif

    private IUpdate<TEntity> BuildUpdate(TEntity dto, IEnumerable<string> includeFields, string[] excludeFields = null)
    {
        var ret = includeFields == null
            ? Rpo.UpdateDiy.SetSource(dto)
            : Rpo.UpdateDiy.SetDto(includeFields!.ToDictionary(
                                       x => x
                                     , x => typeof(TEntity).GetProperty(x, BindingFlags.Public | BindingFlags.Instance)!
                                                           .GetValue(dto)));
        if (excludeFields != null) {
            ret = ret.IgnoreColumns(excludeFields);
        }

        if (dto is IFieldVersion version) {
            ret = ret.Where($"{nameof(IFieldVersion.Version)} = @version", new { version = version.Version });
        }

        return ret;
    }
}