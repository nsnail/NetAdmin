using CsvHelper;
using Microsoft.Net.Http.Headers;
using NetAdmin.Application.Repositories;
using NetAdmin.Domain;
using NetAdmin.Domain.Dto.Dependency;

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
    ///     导出实体
    /// </summary>
    protected async Task<IActionResult> ExportAsync<TQuery, TExport>( //
        Func<QueryReq<TQuery>, ISelectGrouping<TEntity, TEntity>> selector, QueryReq<TQuery> query, string fileName
      , Expression<Func<ISelectGroupingAggregate<TEntity, TEntity>, object>> listExp = null)
        where TQuery : DataAbstraction, new()
    {
        var list = await selector(query).Take(Numbers.MAX_LIMIT_EXPORT).ToListAsync(listExp).ConfigureAwait(false);
        return await GetExportFileStreamAsync<TExport>(fileName, list).ConfigureAwait(false);
    }

    /// <summary>
    ///     导出实体
    /// </summary>
    protected async Task<IActionResult> ExportAsync<TQuery, TExport>( //
        Func<QueryReq<TQuery>, ISelect<TEntity>> selector, QueryReq<TQuery> query, string fileName
      , Expression<Func<TEntity, object>>        listExp = null)
        where TQuery : DataAbstraction, new()
    {
        var select = selector(query)
            #if DBTYPE_SQLSERVER
                     .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .Take(Numbers.MAX_LIMIT_EXPORT);

        object list = listExp == null
            ? await select.ToListAsync().ConfigureAwait(false)
            : await select.ToListAsync(listExp).ConfigureAwait(false);

        return await GetExportFileStreamAsync<TExport>(fileName, list).ConfigureAwait(false);
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
    /// <param name="whereSql">查询sql</param>
    /// <param name="ignoreVersion">是否忽略版本锁</param>
    /// <returns>更新后的实体列表</returns>
    protected Task<List<TEntity>> UpdateReturnListAsync(     //
        TEntity                         newValue             //
      , IEnumerable<string>             includeFields        //
      , string[]                        excludeFields = null //
      , Expression<Func<TEntity, bool>> whereExp = null //
      , string                          whereSql = null //
      , bool                            ignoreVersion = false)
    {
        // 默认匹配主键
        whereExp ??= a => a.Id.Equals(newValue.Id);
        return BuildUpdate(newValue, includeFields, excludeFields, ignoreVersion)
               .Where(whereExp)
               .Where(whereSql)
               .ExecuteUpdatedAsync();
    }
    #endif

    private static async Task<IActionResult> GetExportFileStreamAsync<TExport>(string fileName, object list)
    {
        var listTyped = list.Adapt<List<TExport>>();
        var stream    = new MemoryStream();
        var writer    = new StreamWriter(stream);
        var csv       = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteHeader<TExport>();
        await csv.NextRecordAsync().ConfigureAwait(false);

        foreach (var item in listTyped) {
            csv.WriteRecord(item);
            await csv.NextRecordAsync().ConfigureAwait(false);
        }

        await csv.FlushAsync().ConfigureAwait(false);
        _ = stream.Seek(0, SeekOrigin.Begin);

        App.HttpContext.Response.Headers.ContentDisposition
            = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT) {
                  FileNameStar = $"{fileName}_{DateTime.Now:yyyy.MM.dd-HH.mm.ss}.csv"
              }.ToString();

        return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

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