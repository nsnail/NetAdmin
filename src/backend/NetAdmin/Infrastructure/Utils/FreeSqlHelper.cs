using System.Data.Common;
using System.Reflection;
using FreeSql;
using FreeSql.Aop;
using FreeSql.DataAnnotations;
using Furion;
using NetAdmin.Aop.Attributes;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Lang;
using Newtonsoft.Json;
using NSExt.Extensions;
using Yitter.IdGenerator;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     FreeSqlHelper
/// </summary>
public class FreeSqlHelper
{
    private readonly DatabaseOptions        _databaseOptions;
    private readonly ILogger<FreeSqlHelper> _logger;
    private          TimeSpan               _timeOffset;

    private FreeSqlHelper(DatabaseOptions databaseOptions)
    {
        _databaseOptions = databaseOptions;
        _logger          = App.GetService<ILogger<FreeSqlHelper>>();
    }

    /// <summary>
    ///     创建FreeSql
    /// </summary>
    public static IFreeSql Create(DatabaseOptions options)
    {
        return new FreeSqlHelper(options).Build();
    }

    private static Type[] GetEntityTypes()
    {
        //获取所有表实体
        var entityTypes = (from type in App.EffectiveTypes
                           from attr in type.GetCustomAttributes()
                           where attr is TableAttribute { DisableSyncStructure: false }
                           select type).ToArray();
        return entityTypes;
    }

    private static string GetNoParamSql(string sql, IEnumerable<DbParameter> dbParams)
    {
        return dbParams.Where(x => x is not null)
                       .Aggregate(
                           sql, (current, dbParam) => current.Replace(dbParam.ParameterName, dbParam.Value?.ToString()))
                       .RemoveWrapped();
    }

    private static void InitSeedData(IFreeSql freeSql, IEnumerable<Type> entityTypes)
    {
        foreach (var entityType in entityTypes) {
            var path = $"{AppContext.BaseDirectory}/.data/seed-data/{entityType.Name}.json";
            if (!File.Exists(path)) {
                continue;
            }

            dynamic entities = JsonConvert.DeserializeObject( //
                File.ReadAllText(path), typeof(List<>).MakeGenericType(entityType));

            // 如果表存在数据，跳过
            var select = typeof(IFreeSql).GetMethod(nameof(freeSql.Select), 1, Type.EmptyTypes)
                                         ?.MakeGenericMethod(entityType)
                                         .Invoke(freeSql, null);
            if (select?.GetType()
                      .GetMethod(nameof(ISelect<dynamic>.Any), 0, Type.EmptyTypes)
                      ?.Invoke(select, null) as bool? ?? true) {
                continue;
            }

            var rep = typeof(FreeSqlDbContextExtensions).GetMethods()
                                                        .Where(x => x.Name ==
                                                                    nameof(FreeSqlDbContextExtensions.GetRepository))
                                                        .FirstOrDefault(x => x.GetGenericArguments().Length == 1)
                                                        ?.MakeGenericMethod(entityType)
                                                        .Invoke(null, new object[] { freeSql, null });
            if (rep?.GetType().GetProperty(nameof(DbContextOptions))?.GetValue(rep) is DbContextOptions options) {
                options.EnableCascadeSave = true;
                options.NoneParameter     = true;
            }

            var insert = typeof(IBaseRepository<>).MakeGenericType(entityType)
                                                  .GetMethod( //
                                                      nameof(IBaseRepository<dynamic>.Insert)
                                                    , BindingFlags.Public | BindingFlags.Instance, null
                                                    , CallingConventions.Any
                                                    , new[] { typeof(List<>).MakeGenericType(entityType) }, null);

            insert?.Invoke(rep, new[] { entities });
        }
    }

    private IFreeSql Build()
    {
        var freeSql = new FreeSqlBuilder().UseConnectionString(_databaseOptions.DbType, _databaseOptions.ConnStr)
                                          .UseAutoSyncStructure(false)
                                          .Build();

        // 获取服务器时间偏差
        var serverTime = freeSql.Ado.QuerySingle(() => DateTime.UtcNow);
        _timeOffset = DateTime.UtcNow.Subtract(serverTime);

        freeSql.Aop.AuditValue += DataAuditHandler;
        freeSql.Aop.CurdBefore += (_, e) => {
            var sql = GetNoParamSql(e.Sql, e.DbParms);
            _logger.LogInformation("SQL.{HashCode}：{Sql}", (uint)sql.GetHashCode(), sql);
        };
        freeSql.Aop.CurdAfter += (_, e) => {
            var sql = GetNoParamSql(e.Sql, e.DbParms);
            _logger.LogInformation("SQL.{HashCode}：{EElapsedMilliseconds} ms", (uint)sql.GetHashCode()
                                 , e.ElapsedMilliseconds);
        };
        Task.Run(() => {
            var entityTypes = GetEntityTypes();

            SyncStructure(freeSql, entityTypes);
            _logger.LogInformation("{}", Str.The_database_structure_has_been_synchronized);

            InitSeedData(freeSql, entityTypes);
            _logger.LogInformation("{}", Str.The_seed_data_is_fully_initialized);
        });
        return freeSql;
    }

    private void DataAuditHandler(object sender, AuditValueEventArgs e)
    {
        //设置服务器时间字段
        if (e.Property.GetCustomAttribute<ServerTimeAttribute>(false) is { Enable: true }   &&
            (e.Column.CsType == typeof(DateTime) || e.Column.CsType   == typeof(DateTime?)) &&
            (e.Value         == null             || (DateTime)e.Value == default || (DateTime?)e.Value == default)) {
            e.Value = DateTime.Now.Subtract(_timeOffset);
        }

        //设置雪花id字段
        if (e.Column.CsType == typeof(long)                                              &&
            e.Property.GetCustomAttribute<SnowflakeAttribute>(false) is { Enable: true } &&
            (e.Value == null || (long)e.Value == default || (long?)e.Value == default)) {
            e.Value = YitIdHelper.NextId();
        }
    }

    private void SyncStructure(IFreeSql freeSql, Type[] entityTypes)
    {
        if (_databaseOptions.DbType == DataType.Oracle) {
            freeSql.CodeFirst.IsSyncStructureToUpper = true;
        }

        freeSql.CodeFirst.SyncStructure(entityTypes);
    }
}