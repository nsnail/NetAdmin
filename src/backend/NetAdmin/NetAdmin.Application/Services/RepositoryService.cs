using NetAdmin.Application.Repositories;
using NetAdmin.Domain;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Extensions;

namespace NetAdmin.Application.Services;

/// <summary>
///     仓储服务基类
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TPrimary">主键类型</typeparam>
/// <typeparam name="TLogger">日志类型</typeparam>
public abstract class RepositoryService<TEntity, TPrimary, TLogger>(BasicRepository<TEntity, TPrimary> rpo) : ServiceBase<TLogger>
    where TEntity : EntityBase<TPrimary>
    where TPrimary : IEquatable<TPrimary>
{
    /// <summary>
    ///     默认仓储
    /// </summary>
    protected BasicRepository<TEntity, TPrimary> Rpo => rpo;

    /// <summary>
    ///     启用级联保存
    /// </summary>
    protected bool EnableCascadeSave
    {
        get => Rpo.DbContextOptions.EnableCascadeSave;
        set => Rpo.DbContextOptions.EnableCascadeSave = value;
    }

    /// <summary>
    ///     导出实体
    /// </summary>
    protected static async Task<IActionResult> ExportAsync<TQuery, TExport>(
        Func<QueryReq<TQuery>, ISelectGrouping<TEntity, TEntity>> selector
        , QueryReq<TQuery> query
        , string fileName
        , Expression<Func<ISelectGroupingAggregate<TEntity, TEntity>, object>> listExp = null
    )
        where TQuery : DataAbstraction, new() {
        var list = await selector(query).Take(Numbers.MAX_LIMIT_EXPORT).ToListAsync(listExp).ConfigureAwait(false);
        return await GetExportFileStreamAsync<TExport>(fileName, list).ConfigureAwait(false);
    }

    /// <summary>
    ///     导出实体
    /// </summary>
    protected static async Task<IActionResult> ExportAsync<TQuery, TExport>(
        Func<QueryReq<TQuery>, ISelect<TEntity>> selector
        , QueryReq<TQuery> query
        , string fileName
        , Expression<Func<TEntity, object>> listExp = null
        , Func<object, object> listHandle = null
    )
        where TQuery : DataAbstraction, new() {
        var select = selector(query).WithNoLockNoWait().Take(Numbers.MAX_LIMIT_EXPORT);

        object list = listExp == null ? await select.ToListAsync().ConfigureAwait(false) : await select.ToListAsync(listExp).ConfigureAwait(false);

        list = listHandle?.Invoke(list) ?? list;

        return await GetExportFileStreamAsync<TExport>(fileName, list).ConfigureAwait(false);
    }

    /// <summary>
    ///     唯一索引冲突处理
    /// </summary>
    protected static async Task OnUniqueIndexConflictAsync(
        Func<Task> actionTry
        , Func<Task> actionCatch = null
    ) {
        try {
            await actionTry().ConfigureAwait(false);
        }
        catch (Exception ex) when (ex.Message.Contains(Chars.FLG_DB_EXCEPTION_PRIMARY_KEY_CONFLICT)
                                   || ex.Message.Contains(Chars.FLG_DB_EXCEPTION_UNIQUE_CONSTRAINT_CONFLICT)
                                   || ex.Message.Contains(Chars.FLG_DB_EXCEPTION_IDX)) {
            if (actionCatch != null) {
                await actionCatch().ConfigureAwait(false);
            }
        }
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
    /// <param name="disableGlobalDataFilter">是否忽略全局数据权限过滤</param>
    /// <returns>更新行数</returns>
    protected Task<int> UpdateAsync(
        TEntity newValue
        , List<string> includeFields = null
        , List<string> excludeFields = null
        , Expression<Func<TEntity, bool>> whereExp = null
        , string whereSql = null
        , bool ignoreVersion = false
        , bool disableGlobalDataFilter = false
    ) {
        // 默认匹配主键
        whereExp ??= a => a.Id.Equals(newValue.Id);
        var update = BuildUpdate(newValue, includeFields, excludeFields, ignoreVersion).Where(whereExp).Where(whereSql);
        if (disableGlobalDataFilter) {
            update = update.DisableGlobalFilter(
                Chars.FLG_FREE_SQL_GLOBAL_FILTER_SELF, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT, Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN
                , Chars.FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON
            );
        }

        return update.ExecuteEffectsAsync();
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
    protected Task<List<TEntity>> UpdateReturnListAsync(
        TEntity newValue
        , List<string> includeFields = null
        , List<string> excludeFields = null
        , Expression<Func<TEntity, bool>> whereExp = null
        , string whereSql = null
        , bool ignoreVersion = false
    ) {
        // 默认匹配主键
        whereExp ??= a => a.Id.Equals(newValue.Id);
        return BuildUpdate(newValue, includeFields, excludeFields, ignoreVersion).Where(whereExp).Where(whereSql).ExecuteUpdatedAsync();
    }
    #endif

    #pragma warning disable S2326
    // ReSharper disable UnusedTypeParameter
    // ReSharper disable UnusedParameter.Local
    private static Task<IActionResult> GetExportFileStreamAsync<TExport>(
        string fileName
        , object list
    ) {
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore UnusedTypeParameter
        #pragma warning restore S2326
        throw new NotImplementedException();

        // var listTyped = list.Adapt<List<TExport>>();
        // var stream = new MemoryStream();
        // var writer = new StreamWriter(stream);
        // var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        // csv.WriteHeader<TExport>();
        // await csv.NextRecordAsync().ConfigureAwait(false);
        //
        // foreach (var item in listTyped) {
        //     csv.WriteRecord(item);
        //     await csv.NextRecordAsync().ConfigureAwait(false);
        // }
        //
        // await csv.FlushAsync().ConfigureAwait(false);
        // _ = stream.Seek(0, SeekOrigin.Begin);
        //
        // App.HttpContext.Response.Headers.ContentDisposition
        //     = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT)
        //     {
        //         FileNameStar = $"{fileName}_{DateTime.Now:yyyy.MM.dd-HH.mm.ss}.csv"
        //     }.ToString();
        //
        // return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

    private static Dictionary<string, object> IncludeToDictionary(
        TEntity entity
        , List<string> includeFields
    ) {
        var ret = includeFields!.ToDictionary(
            x => x, x => typeof(TEntity).GetProperty(x, BindingFlags.Public | BindingFlags.Instance)!.GetValue(entity)
        );

        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (entity is IFieldModifiedUser) {
            var userInfo = App.GetService<ContextUserInfo>();
            if (userInfo == null) {
                return ret;
            }

            ret.Add(nameof(IFieldModifiedUser.ModifiedUserId), userInfo.Id);
            ret.Add(nameof(IFieldModifiedUser.ModifiedUserName), userInfo.UserName);
        }

        // ReSharper disable once SuspiciousTypeConversion.Global
        if (entity is IFieldModifiedClientIp) {
            ret.Add(nameof(IFieldModifiedClientIp.ModifiedClientIp), App.HttpContext?.GetRealIpAddress()?.MapToIPv4().ToString().IpV4ToInt32());
        }

        return ret;
    }

    private IUpdate<TEntity> BuildUpdate(
        TEntity entity
        , List<string> includeFields
        , List<string> excludeFields
        , bool ignoreVersion
    ) {
        IUpdate<TEntity> updateExp;
        if (includeFields.NullOrEmpty()) {
            updateExp = Rpo.UpdateDiy.SetSource(entity);
            if (!excludeFields.NullOrEmpty()) {
                updateExp = updateExp.IgnoreColumns(excludeFields.ToArray());
            }
        }
        else {
            updateExp = Rpo.UpdateDiy.SetDto(IncludeToDictionary(entity, includeFields));
            if (!ignoreVersion && entity is IFieldVersion ver) {
                updateExp = updateExp.Where($"{nameof(IFieldVersion.Version)} = {ver.Version}");
            }
        }

        return updateExp;
    }
}