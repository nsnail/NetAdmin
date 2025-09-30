#if DBTYPE_SQLSERVER
using Microsoft.Data.SqlClient;
#endif
using DataType = FreeSql.DataType;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     FreeSql 构建器
/// </summary>
public sealed class FreeSqlBuilder(DatabaseOptions databaseOptions)
{
    /// <summary>
    ///     构建freeSql对象
    /// </summary>
    public IFreeSql Build(
        FreeSqlInitMethods initMethods
        , Func<int, Task> onSeedDataInserted = null
    ) {
        var freeSql = new FreeSql.FreeSqlBuilder()
            #if DBTYPE_SQLSERVER
            .UseConnectionFactory(databaseOptions.DbType, () => new SqlConnection(databaseOptions.ConnStr))
            .UseAdoConnectionPool(true)
            #else
            .UseConnectionString(databaseOptions.DbType, databaseOptions.ConnStr)
            #endif
            .UseGenerateCommandParameterWithLambda(true)
            .UseAutoSyncStructure(initMethods.HasFlag(FreeSqlInitMethods.SyncStructure))
            .Build();
        _ = InitDbAsync(freeSql, initMethods, onSeedDataInserted); // 初始化数据库 ，异步
        return freeSql;
    }

    private static void CompareStructure(
        IFreeSql freeSql
        , Type[] entityTypes
    ) {
        File.WriteAllText($"{nameof(CompareStructure)}.sql", freeSql.CodeFirst.GetComparisonDDLStatements(entityTypes));
    }

    /// <summary>
    ///     获取所有实体类型定义
    /// </summary>
    private static Type[] GetEntityTypes() {
        return (
            from type in App.EffectiveTypes
            from attr in type.GetCustomAttributes()
            where attr is TableAttribute { DisableSyncStructure: false }
            select type).ToArray();
    }

    private static MethodInfo MakeGetRepositoryMethod(Type entityType) {
        return typeof(FreeSqlDbContextExtensions)
            .GetMethods()
            .Where(x => x.Name == nameof(FreeSqlDbContextExtensions.GetRepository))
            .FirstOrDefault(x => x.GetGenericArguments().Length == 1)
            ?.MakeGenericMethod(entityType);
    }

    private static MethodInfo MakeInsertMethod(Type entityType) {
        return typeof(IBaseRepository<>)
            .MakeGenericType(entityType)
            .GetMethod(
                nameof(IBaseRepository<>.Insert), BindingFlags.Public | BindingFlags.Instance, null, CallingConventions.Any
                , [typeof(IEnumerable<>).MakeGenericType(entityType)], null
            );
    }

    /// <summary>
    ///     初始化数据库
    /// </summary>
    private Task InitDbAsync(
        IFreeSql freeSql
        , FreeSqlInitMethods initMethods
        , Func<int, Task> onSeedDataInserted
    ) {
        return Task.Run(() =>
            {
                if (initMethods == FreeSqlInitMethods.None) {
                    return;
                }

                var entityTypes = GetEntityTypes();
                if (initMethods.HasFlag(FreeSqlInitMethods.SyncStructure)) {
                    SyncStructure(freeSql, entityTypes);
                }

                if (initMethods.HasFlag(FreeSqlInitMethods.InsertSeedData)) {
                    var insertCount = InsertSeedData(freeSql, entityTypes);
                    _ = onSeedDataInserted?.Invoke(insertCount);
                }

                if (initMethods.HasFlag(FreeSqlInitMethods.CompareStructure)) {
                    CompareStructure(freeSql, entityTypes);
                }
            }
        );
    }

    /// <summary>
    ///     插入种子数据
    /// </summary>
    private int InsertSeedData(
        IFreeSql freeSql
        , IEnumerable<Type> entityTypes
    ) {
        var ret = 0;
        foreach (var entityType in entityTypes) {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseOptions.SeedDataRelativePath, $"{entityType.Name}.json");
            if (!File.Exists(file)) {
                continue;
            }

            using var fs = File.OpenRead(file);
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default)
            {
                AllowTrailingCommas = true
                , ReadCommentHandling = JsonCommentHandling.Skip
                , TypeInfoResolver = new DefaultJsonTypeInfoResolver { Modifiers = { JsonIgnoreRemover.RemoveJsonIgnore(entityType) } }
            };
            _ = jsonSerializerOptions.Converters.AddDateTimeTypeConverters();

            var jsonTypeInfo = JsonTypeInfo.CreateJsonTypeInfo(typeof(IEnumerable<>).MakeGenericType(entityType), jsonSerializerOptions);
            dynamic entities = JsonSerializer.Deserialize(fs, jsonTypeInfo);

            // 如果表存在数据，跳过
            var select = typeof(IFreeSql).GetMethod(nameof(freeSql.Select), 1, Type.EmptyTypes)?.MakeGenericMethod(entityType).Invoke(freeSql, null);
            if (select?.GetType().GetMethod(nameof(ISelect<>.Any), 0, Type.EmptyTypes)?.Invoke(select, null) as bool? ?? true) {
                continue;
            }

            var rep = MakeGetRepositoryMethod(entityType)?.Invoke(null, [freeSql, null]);
            if (rep?.GetType().GetProperty(nameof(DbContextOptions))?.GetValue(rep) is DbContextOptions options) {
                options.EnableCascadeSave = true;
                options.NoneParameter = true;
            }

            var insert = MakeInsertMethod(entityType);

            _ = insert?.Invoke(rep, [entities]);
            ret += entities!.Count;
        }

        return ret;
    }

    /// <summary>
    ///     同步数据库结构
    /// </summary>
    private void SyncStructure(
        IFreeSql freeSql
        , Type[] entityTypes
    ) {
        if (databaseOptions.DbType == DataType.Oracle) {
            freeSql.CodeFirst.IsSyncStructureToUpper = true;
        }

        freeSql.CodeFirst.SyncStructure(entityTypes);
    }
}