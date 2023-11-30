using Newtonsoft.Json;
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
    public IFreeSql Build(FreeSqlInitMethods initMethods)
    {
        var freeSql = new FreeSql.FreeSqlBuilder().UseConnectionString(databaseOptions.DbType, databaseOptions.ConnStr)
                                                  .UseAutoSyncStructure(false)
                                                  .Build();

        _ = InitDbAsync(freeSql, initMethods); // 初始化数据库 ，异步
        return freeSql;
    }

    private static void CompareStructure(IFreeSql freeSql, Type[] entityTypes)
    {
        File.WriteAllText( //
            $"{nameof(CompareStructure)}.sql", freeSql.CodeFirst.GetComparisonDDLStatements(entityTypes));
    }

    /// <summary>
    ///     获取所有实体类型定义
    /// </summary>
    private static Type[] GetEntityTypes()
    {
        return (from type in App.EffectiveTypes
                from attr in type.GetCustomAttributes()
                where attr is TableAttribute { DisableSyncStructure: false }
                select type).ToArray();
    }

    private static MethodInfo MakeGetRepositoryMethod(Type entityType)
    {
        return typeof(FreeSqlDbContextExtensions).GetMethods()
                                                 .Where(x => x.Name == nameof(FreeSqlDbContextExtensions.GetRepository))
                                                 .FirstOrDefault(x => x.GetGenericArguments().Length == 1)
                                                 ?.MakeGenericMethod(entityType);
    }

    private static MethodInfo MakeInsertMethod(Type entityType)
    {
        return typeof(IBaseRepository<>).MakeGenericType(entityType)
                                        .GetMethod( //
                                            nameof(IBaseRepository<dynamic>.Insert)
                                          , BindingFlags.Public | BindingFlags.Instance, null
                                          , CallingConventions.Any
                                          , new[] { typeof(IEnumerable<>).MakeGenericType(entityType) }, null);
    }

    /// <summary>
    ///     初始化数据库
    /// </summary>
    private Task InitDbAsync(IFreeSql freeSql, FreeSqlInitMethods initMethods)
    {
        return Task.Run(() => {
            if (initMethods == FreeSqlInitMethods.None) {
                return;
            }

            var entityTypes = GetEntityTypes();
            if (initMethods.HasFlag(FreeSqlInitMethods.SyncStructure)) {
                SyncStructure(freeSql, entityTypes);
            }

            if (initMethods.HasFlag(FreeSqlInitMethods.InsertSeedData)) {
                InsertSeedData(freeSql, entityTypes);
            }

            if (initMethods.HasFlag(FreeSqlInitMethods.CompareStructure)) {
                CompareStructure(freeSql, entityTypes);
            }
        });
    }

    /// <summary>
    ///     插入种子数据
    /// </summary>
    private void InsertSeedData(IFreeSql freeSql, IEnumerable<Type> entityTypes)
    {
        foreach (var entityType in entityTypes) {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseOptions.SeedDataRelativePath
                                  , $"{entityType.Name}.json");
            if (!File.Exists(file)) {
                continue;
            }

            var fileContent = File.ReadAllText(file);

            dynamic entities = JsonConvert.DeserializeObject( //
                fileContent, typeof(IEnumerable<>).MakeGenericType(entityType));

            // 如果表存在数据，跳过
            var select = typeof(IFreeSql).GetMethod(nameof(freeSql.Select), 1, Type.EmptyTypes)
                                         ?.MakeGenericMethod(entityType)
                                         .Invoke(freeSql, null);
            if (select?.GetType()
                      .GetMethod(nameof(ISelect<dynamic>.Any), 0, Type.EmptyTypes)
                      ?.Invoke(select, null) as bool? ?? true) {
                continue;
            }

            var rep = MakeGetRepositoryMethod(entityType)?.Invoke(null, new object[] { freeSql, null });
            if (rep?.GetType().GetProperty(nameof(DbContextOptions))?.GetValue(rep) is DbContextOptions options) {
                options.EnableCascadeSave = true;
                options.NoneParameter     = true;
            }

            var insert = MakeInsertMethod(entityType);

            _ = insert?.Invoke(rep, new[] { entities });
        }
    }

    /// <summary>
    ///     同步数据库结构
    /// </summary>
    private void SyncStructure(IFreeSql freeSql, Type[] entityTypes)
    {
        if (databaseOptions.DbType == DataType.Oracle) {
            freeSql.CodeFirst.IsSyncStructureToUpper = true;
        }

        freeSql.CodeFirst.SyncStructure(entityTypes);
    }
}