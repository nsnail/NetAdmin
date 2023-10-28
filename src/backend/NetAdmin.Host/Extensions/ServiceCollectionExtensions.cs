using Furion.Logging;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Events;
using NetAdmin.Host.Filters;
using NetAdmin.Host.Utils;
using Spectre.Console;
using StackExchange.Redis;
using Yitter.IdGenerator;
using FreeSqlBuilder = NetAdmin.Infrastructure.Utils.FreeSqlBuilder;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    private const int _CONSOLE_LINE_LEN_LIMIT = 8192;

    private static readonly Dictionary<Regex, string> _consoleColors //
        = new() {
                    {
                        new Regex( //
                            @"(\d{2,}\.\d+ ?ms)", RegexOptions.Compiled)
                      , $"[{nameof(ConsoleColor.Magenta)}]$1[/]"
                    }
                  , {
                        new Regex( //
                            "(Tb[a-zA-Z0-9]+)", RegexOptions.Compiled)
                      , $"[{nameof(ConsoleColor.Cyan)}]$1[/]"
                    }
                  , {
                        new Regex( //
                            "(INSERT) ", RegexOptions.Compiled)
                      , $"[{nameof(ConsoleColor.Blue)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            "(SELECT) ", RegexOptions.Compiled)
                      , $"[{nameof(ConsoleColor.Green)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            "(UPDATE) ", RegexOptions.Compiled)
                      , $"[{nameof(ConsoleColor.Yellow)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            "(DELETE) ", RegexOptions.Compiled)
                      , $"[{nameof(ConsoleColor.Red)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            "(<s:.+?>)", RegexOptions.Compiled)
                      , $"[underline {nameof(ConsoleColor.Gray)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            "(ResponseBody)", RegexOptions.Compiled)
                      , $"[underline {nameof(ConsoleColor.Cyan)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            "(RequestBody)", RegexOptions.Compiled)
                      , $"[underline {nameof(ConsoleColor.Magenta)}]$1[/] "
                    }
                  , {
                        new Regex( //
                            @"(\[\[dbo\]\]\.)(\[\[.+?\]\]) ", RegexOptions.Compiled)
                      , $"$1[{nameof(ConsoleColor.Magenta)}]$2[/] "
                    }
                };

    /// <summary>
    ///     扫描程序集中继承自IConfigurableOptions的选项，注册
    /// </summary>
    public static IServiceCollection AddAllOptions( //
        this IServiceCollection me)
    {
        var optionsTypes
            = from type in App.EffectiveTypes.Where(x => !x.IsAbstract && !x.FullName!.Contains(nameof(Furion)) &&
                                                         x.GetInterfaces().Contains(typeof(IConfigurableOptions)))
              select type;

        var sbLog = new StringBuilder();
        foreach (var type in optionsTypes) {
            var configureMethod = typeof(ConfigurableOptionsServiceCollectionExtensions).GetMethod(
                nameof(ConfigurableOptionsServiceCollectionExtensions.AddConfigurableOptions)
              , BindingFlags.Public | BindingFlags.Static, new[] { typeof(IServiceCollection) });
            _ = configureMethod!.MakeGenericMethod(type).Invoke(me, new object[] { me });
            _ = sbLog.Append(CultureInfo.InvariantCulture, $", {type.Name}");
        }

        LogHelper.Get<IServiceCollection>()?.Info($"{nameof(IConfigurableOptions)} {Ln.初始化完毕} {sbLog}");
        return me;
    }

    /// <summary>
    ///     注册控制台日志模板
    /// </summary>
    public static IServiceCollection AddConsoleFormatter(this IServiceCollection me)
    {
        return me.AddConsoleFormatter(options => {
            var logLevels = Enum.GetValues(typeof(LogLevels))
                                .Cast<LogLevels>()
                                .ToDictionary(x => x, x => x.GetDisplay());

            if (!App.WebHostEnvironment.IsProduction()) {
                static void MarkupLine(string                                           msg, LogMessage message
                                     , IReadOnlyDictionary<LogLevels, DisplayAttribute> logLevels)
                {
                    // 日志过长
                    if (msg.Length > _CONSOLE_LINE_LEN_LIMIT) {
                        msg = $"{Ln.日志长度超过限制} {_CONSOLE_LINE_LEN_LIMIT}";
                    }

                    msg = _consoleColors.Aggregate( //
                        msg, (current, regex) => regex.Key.Replace(current, regex.Value));
                    msg = msg.ReplaceLineEndings(string.Empty);
                    var colorName = logLevels[(LogLevels)message.LogLevel].Name!;
                    var (date, logName, logFormat) = ParseMessage(message, true);
                    AnsiConsole.MarkupLine( //
                        CultureInfo.InvariantCulture, logFormat, date, colorName, logName, message.ThreadId, msg);
                }

                options.WriteHandler = (message, _, _, _, _) => {
                    MarkupLine(message.Message.EscapeMarkup(), message, logLevels);
                    if (message.Exception != null) {
                        MarkupLine(message.Exception.ToString().EscapeMarkup(), message, logLevels);
                    }
                };
            }
            else {
                options.WriteHandler = (message, _, _, _, _) => {
                    var msg = message.Message.ReplaceLineEndings(string.Empty);
                    var (date, logName, logFormat) = ParseMessage(message, false);
                    Console.WriteLine( //
                        logFormat, date, logLevels[(LogLevels)message.LogLevel].ShortName, logName, message.ThreadId
                      , msg);
                };
            }
        });

        static (string Date, string LogName, string LogFormat) ParseMessage(LogMessage message, bool showColor)
        {
            var date    = message.LogDateTime.ToString(Chars.TPL_DATE_HH_MM_SS_FFFFFF, CultureInfo.InvariantCulture);
            var logName = message.LogName.PadRight(64, ' ')[^64..];
            var format = showColor
                ? $"[{nameof(ConsoleColor.Gray)}][[{{0}} {{1}} {{2,-{64}}} #{{3,4}}]][/] {{4}}"
                : $"[{{0}} {{1}} {{2,-{64}}} #{{3,4}}] {{4}}";

            return (date, logName, format);
        }
    }

    /// <summary>
    ///     注册上下文用户
    /// </summary>
    public static IServiceCollection AddContextUser(this IServiceCollection me)
    {
        _ = me.AddScoped(typeof(ContextUserToken), _ => ContextUserToken.Create());
        _ = me.AddScoped(typeof(ContextUserInfo),  _ => ContextUserInfo.Create());

        return me;
    }

    /// <summary>
    ///     注册事件总线
    /// </summary>
    public static IServiceCollection AddEventBus(this IServiceCollection me)
    {
        _ = me.AddEventBus(builder => builder.AddSubscribers(App.Assemblies.ToArray()));
        return me;
    }

    /// <summary>
    ///     注册freeSql orm工具
    /// </summary>
    public static IServiceCollection AddFreeSql( //
        this IServiceCollection me, FreeSqlInitOptions initOptions = FreeSqlInitOptions.None
      , Action<IFreeSql>        freeSqlConfig = null)
    {
        // // 非调试模式下禁止同步数据库
        // #if !DEBUG
        // initOptions = FreeSqlInitOptions.None;
        // #endif
        var freeSql = new FreeSqlBuilder(App.GetOptions<DatabaseOptions>()).Build(initOptions);
        _ = me.AddSingleton(freeSql);

        var sqlAuditor = App.GetService<SqlAuditor>();

        freeSql.Aop.AuditValue += sqlAuditor.DataAuditHandler; // Insert/Update自动值处理

        // AOP事件发布（异步）
        freeSql.Aop.CommandBefore
            += (_, e) => App.GetService<IEventPublisher>().PublishAsync(new SqlCommandBeforeEvent(e)); // 增删查改，执行命令之前触发
        freeSql.Aop.CommandAfter
            += (_, e) => App.GetService<IEventPublisher>().PublishAsync(new SqlCommandAfterEvent(e)); // 增删查改，执行命令完成后触发

        freeSql.Aop.SyncStructureBefore += (_, e) =>
            App.GetService<IEventPublisher>().PublishAsync(new SyncStructureBeforeEvent(e)); // CodeFirst迁移，执行之前触发

        freeSql.Aop.SyncStructureAfter += (_, e) =>
            App.GetService<IEventPublisher>().PublishAsync(new SyncStructureAfterEvent(e)); // CodeFirst迁移，执行完成触发

        // 全局过滤器设置
        freeSqlConfig?.Invoke(freeSql);

        _ = me.AddScoped<UnitOfWorkManager>();                    // 注入工作单元管理器
        _ = me.AddFreeRepository(null, App.Assemblies.ToArray()); // 批量注入 Repository
        _ = me.AddMvcFilter<TransactionInterceptor>();            // 注入事务拦截器

        return me;
    }

    /// <summary>
    ///     注册内存缓存
    /// </summary>
    public static IServiceCollection AddMemCache(this IServiceCollection me)
    {
        _ = me.AddMemoryCache(options => options.TrackStatistics = true);
        return me;
    }

    /// <summary>
    ///     OpenTelemetry数据监控
    /// </summary>
    public static IServiceCollection AddOpenTelemetryNet(this IServiceCollection me)
    {
        // _ = me.AddOpenTelemetry()
        //       .WithMetrics(builder => builder.AddAspNetCoreInstrumentation()
        //                                      .AddHttpClientInstrumentation()
        //                                      .AddRuntimeInstrumentation()
        //                                      .AddProcessInstrumentation()
        //                                      .AddPrometheusExporter());
        return me;
    }

    /// <summary>
    ///     注册Redis缓存
    /// </summary>
    public static IServiceCollection AddRedisCache(this IServiceCollection me)
    {
        var redisOptions = App.GetOptions<RedisOptions>()
                              .Instances.First(x => x.Name == Chars.FLG_REDIS_INSTANCE_DATA_CACHE);

        // IDistributedCache 分布式缓存通用接口
        _ = me.AddStackExchangeRedisCache(options => {
            // 连接字符串
            options.Configuration = redisOptions.ConnStr;
        });

        // Redis原生接口
        _ = me.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions.ConnStr));
        return me;
    }

    /// <summary>
    ///     注册雪花编号生成器
    /// </summary>
    public static IServiceCollection AddSnowflake(this IServiceCollection me)
    {
        // 雪花漂移算法
        var workerId           = Environment.GetEnvironmentVariable(Chars.FLG_SNOWFLAKE_WORK_ID).Int32Try(0);
        var idGeneratorOptions = new IdGeneratorOptions((ushort)workerId) { WorkerIdBitLength = 6 };
        YitIdHelper.SetIdGenerator(idGeneratorOptions);
        return me;
    }
}