using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using FreeSql;
using Furion;
using Furion.ConfigurableOptions;
using Furion.DependencyInjection;
using Furion.EventBus;
using NetAdmin.Aop.Pipelines;
using NetAdmin.Events.Sources;
using NetAdmin.Infrastructure.Configuration;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;
using Spectre.Console;
using Yitter.IdGenerator;
using FreeSqlBuilder = NetAdmin.Infrastructure.Utils.FreeSqlBuilder;

namespace NetAdmin.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    private const int _LOGGER_WIDTH = 64;

    private static readonly Dictionary<Regex, string> _logKeywords //
        = new() {
                    {
                        new Regex( //
                            @"(\d+ *ms)", RegexOptions.Compiled)
                      , @$"[{nameof(ConsoleColor.Magenta)}]$1[/]"
                    }
                  , {
                        new Regex( //
                            @"(Tb[a-zA-Z0-9]+)", RegexOptions.Compiled)
                      , @$"[{nameof(ConsoleColor.Cyan)}]$1[/]"
                    }
                  , {
                        new Regex( //
                            @"(INSERT) ", RegexOptions.Compiled)
                      , @$"[underline {nameof(ConsoleColor.Blue)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            @"(SELECT) ", RegexOptions.Compiled)
                      , @$"[underline {nameof(ConsoleColor.Green)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            @"(UPDATE) ", RegexOptions.Compiled)
                      , @$"[underline {nameof(ConsoleColor.Yellow)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            @"(DELETE) ", RegexOptions.Compiled)
                      , @$"[underline {nameof(ConsoleColor.Red)}]$1[/] "
                    }
                };

    /// <summary>
    ///     扫描程序集中继承自IConfigurableOptions的选项，注册
    /// </summary>
    public static IServiceCollection AddAllOptions(this IServiceCollection me)
    {
        var optionsTypes
            = from type in App.EffectiveTypes.Where(x => !x.FullName!.Contains(nameof(Furion)) &&
                                                         x.GetInterfaces().Contains(typeof(IConfigurableOptions)))
              select type;

        foreach (var type in optionsTypes) {
            var configureMethod = typeof(ConfigurableOptionsServiceCollectionExtensions).GetMethod(
                nameof(ConfigurableOptionsServiceCollectionExtensions.AddConfigurableOptions)
              , BindingFlags.Public | BindingFlags.Static, new[] { typeof(IServiceCollection) });
            configureMethod!.MakeGenericMethod(type).Invoke(me, new object[] { me });
        }

        return me;
    }

    /// <summary>
    ///     注册控制台日志模板
    /// </summary>
    public static IServiceCollection AddConsoleFormatter(this IServiceCollection me)
    {
        return me.AddConsoleFormatter(options => {
            options.WriteHandler = (message, _, _, _, _) => {
                var msg = _logKeywords.Aggregate(
                    message.Message.EscapeMarkup()
                  , (current, regex) => regex.Key.Replace(current, regex.Value));

                AnsiConsole.MarkupLine( //
                    CultureInfo.InvariantCulture
                  , $"[{nameof(ConsoleColor.Gray)}][[{{0}} {{1}} {{2,-{_LOGGER_WIDTH}}} #{{3,4}}]][/] {{4}}"
                  , message.LogDateTime.ToString(
                        Strings.FMT_DATE_HH_MM_SS_FFFFFF, CultureInfo.InvariantCulture)
                  , ((Enums.LogLevels)message.LogLevel).Desc()
                  , message.LogName.PadRight(_LOGGER_WIDTH, ' ')[^_LOGGER_WIDTH..]
                  , message.ThreadId, msg);
            };
        });
    }

    /// <summary>
    ///     注册freeSql orm工具
    /// </summary>
    public static IServiceCollection AddFreeSql(this IServiceCollection me)
    {
        var options = App.GetOptions<DatabaseOptions>();
        var freeSql = new FreeSqlBuilder(options).Build();
        me.AddSingleton(freeSql);

        var sqlAuditor = App.GetRequiredService<SqlAuditor>();

        freeSql.Aop.AuditValue += sqlAuditor.DataAuditHandler; // Insert/Update自动值处理

        // AOP事件发布（异步）
        freeSql.Aop.CommandBefore += (_, e) => {
            App.GetService<IEventPublisher>()?.PublishAsync(new SqlCommandBeforeEvent(e));
        }; // 增删查改，执行命令之前触发
        freeSql.Aop.CommandAfter += (_, e) => {
            App.GetService<IEventPublisher>()?.PublishAsync(new SqlCommandAfterEvent(e));
        }; // 增删查改，执行命令完成后触发

        freeSql.Aop.SyncStructureBefore += (_, e) => {
            App.GetService<IEventPublisher>()?.PublishAsync(new SyncStructureBeforeEvent(e));
        }; // CodeFirst迁移，执行之前触发

        freeSql.Aop.SyncStructureAfter += (_, e) => {
            App.GetService<IEventPublisher>()?.PublishAsync(new SyncStructureAfterEvent(e));
        }; // CodeFirst迁移，执行完成触发

        // 全局过滤器设置
        // freeSql.GlobalFilter.ApplyOnly<IFieldBitSet>( // 启用/禁用
        //     Strings.FLG_FILTER_BITSET, a => a.BitSet.HasFlag(Enums.BitSets.Enabled));
        me.AddScoped<UnitOfWorkManager>();                    // 注入工作单元管理器
        me.AddFreeRepository(null, App.Assemblies.ToArray()); // 批量注入 Repository
        me.AddMvcFilter<TransactionInterceptor>();            // 注入事务拦截器

        return me;
    }

    /// <summary>
    ///     注册Furion
    /// </summary>
    public static IMvcBuilder AddFurion(this IMvcBuilder me)
    {
        return me.AddInjectWithUnifyResult<ApiResultHandler>(injectOptions => {
            // 替换自定义的EnumSchemaFilter，支持多语言Resx资源 （需将SpecificationDocumentSettings.EnableEnumSchemaFilter配置为false)
            injectOptions.ConfigureSwaggerGen(genOptions => { genOptions.SchemaFilter<SwaggerEnumSchemaFilter>(); });
        });
    }

    /// <summary>
    ///     注册雪花id生成器
    /// </summary>
    public static IServiceCollection AddSnowflake(this IServiceCollection me)
    {
        // 雪花漂移算法
        var idGeneratorOptions = new IdGeneratorOptions(1) { WorkerIdBitLength = 6 };
        YitIdHelper.SetIdGenerator(idGeneratorOptions);
        return me;
    }
}