using NetAdmin.Application.Repositories;

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
    /// <param name="newValue">新的值</param>
    /// <param name="includeFields">包含的属性</param>
    /// <param name="excludeFields">排除的属性</param>
    /// <param name="whereExp">查询表达式</param>
    /// <param name="whereSql">查询sql</param>
    /// <param name="ignoreVersion">是否忽略版本锁</param>
    /// <returns>更新行数</returns>
    protected Task<int> UpdateAsync(                         //
        TEntity                         newValue             //
      , IEnumerable<string>             includeFields        //
      , string[]                        excludeFields = null //
      , Expression<Func<TEntity, bool>> whereExp      = null //
      , string                          whereSql      = null //
      , bool                            ignoreVersion = false)
    {
        // 默认匹配主键
        whereExp ??= a => a.Id.Equals(newValue.Id);
        var update = BuildUpdate(newValue, includeFields, excludeFields, ignoreVersion).Where(whereExp).Where(whereSql);
        return update.ExecuteAffrowsAsync();
    }

    #if DBTYPE_SQLSERVER
    /// <summary>
    ///     更新实体
    /// </summary>
    /// <param name="newValue">新的值</param>
    /// <param name="includeFields">包含的属性</param>
    /// <param name="excludeFields">排除的属性</param>
    /// <param name="whereExp">查询表达式</param>
    /// <param name="ignoreVersion">是否忽略版本锁</param>
    /// <returns>更新后的实体列表</returns>
    protected Task<List<TEntity>> UpdateReturnListAsync(     //
        TEntity                         newValue             //
      , IEnumerable<string>             includeFields        //
      , string[]                        excludeFields = null //
      , Expression<Func<TEntity, bool>> whereExp = null //
      , bool                            ignoreVersion = false)
    {
        // 默认匹配主键
        whereExp ??= a => a.Id.Equals(newValue.Id);
        return BuildUpdate(newValue, includeFields, excludeFields, ignoreVersion).Where(whereExp).ExecuteUpdatedAsync();
    }
    #endif

    private IUpdate<TEntity> BuildUpdate(        //
        TEntity             entity               //
      , IEnumerable<string> includeFields        //
      , string[]            excludeFields = null //
      , bool                ignoreVersion = false)
    {
        var updateExp = includeFields == null
            ? Rpo.UpdateDiy.SetSource(entity)
            : Rpo.UpdateDiy.SetDto(includeFields!.ToDictionary(
                                       x => x
                                     , x => typeof(TEntity).GetProperty(x, BindingFlags.Public | BindingFlags.Instance)!
                                                           .GetValue(entity)));
        if (excludeFields != null) {
            updateExp = updateExp.IgnoreColumns(excludeFields);
        }

        if (!ignoreVersion && entity is IFieldVersion ver) {
            updateExp = updateExp.Where($"{nameof(IFieldVersion.Version)} = @version", new { version = ver.Version });
        }

        return updateExp;
    }
}