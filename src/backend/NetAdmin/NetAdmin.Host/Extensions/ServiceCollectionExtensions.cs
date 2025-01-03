using Gurion.Logging;
using NetAdmin.Domain.Contexts;
using StackExchange.Redis;
using Yitter.IdGenerator;
#if DEBUG
using Spectre.Console;
#endif

namespace NetAdmin.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    #if DEBUG
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

    #endif

    /// <summary>
    ///     扫描程序集中继承自IConfigurableOptions的选项，注册
    /// </summary>
    public static IServiceCollection AddAllOptions( //
        this IServiceCollection me)
    {
        var optionsTypes
            = from type in App.EffectiveTypes.Where(x => !x.IsAbstract && !x.FullName!.Contains(nameof(Gurion)) &&
                                                         x.GetInterfaces().Contains(typeof(IConfigurableOptions)))
              select type;

        var sbLog = new StringBuilder();
        foreach (var type in optionsTypes) {
            var configureMethod = typeof(ConfigurableOptionsServiceCollectionExtensions).GetMethod(
                nameof(ConfigurableOptionsServiceCollectionExtensions.AddConfigurableOptions), BindingFlags.Public | BindingFlags.Static
              , [typeof(IServiceCollection)]);
            _ = configureMethod!.MakeGenericMethod(type).Invoke(me, [me]);
            _ = sbLog.Append(CultureInfo.InvariantCulture, $" {type.Name}");
        }

        LogHelper.Get<IServiceCollection>()?.Info($"{Ln.配置文件初始化完毕} {sbLog}");
        return me;
    }

    /// <summary>
    ///     添加控制台日志模板
    /// </summary>
    public static IServiceCollection AddConsoleFormatter(this IServiceCollection me)
    {
        return me.AddConsoleFormatter(options => {
            var logLevels = Enum.GetValues<LogLevels>().ToDictionary(x => x, x => x.GetDisplay());

            options.WriteHandler = (message, _, _, _, _) => {
                #if DEBUG
                MarkupLine(message.Message.EscapeMarkup(), message, logLevels);
                if (message.Exception != null) {
                    MarkupLine(message.Exception.ToString().EscapeMarkup(), message, logLevels);
                }
                #else
                var msg = message.Message.ReplaceLineEndings(string.Empty);
                var (date, logName, logFormat) = ParseMessage(message, false);
                Console.WriteLine( //
                    logFormat, date, logLevels[(LogLevels)message.LogLevel].ShortName, logName, message.ThreadId, msg);
                #endif
                GlobalStatic.IncrementLogCounter();
            };
        });
    }

    /// <summary>
    ///     添加上下文用户令牌
    /// </summary>
    public static IServiceCollection AddContextUserToken(this IServiceCollection me)
    {
        return me.AddScoped(typeof(ContextUserToken), _ => ContextUserToken.Create());
    }

    /// <summary>
    ///     添加事件总线
    /// </summary>
    public static IServiceCollection AddEventBus(this IServiceCollection me)
    {
        foreach (var type in App.EffectiveTypes.Where(x => typeof(IEventSubscriber).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)) {
            _ = me.AddSingleton(type);
        }

        return me.AddSingleton<IEventPublisher>(new DefaultEventPublisher());
    }

    /// <summary>
    ///     添加内存缓存
    /// </summary>
    public static IServiceCollection AddMemCache(this IServiceCollection me)
    {
        return me.AddMemoryCache(options => options.TrackStatistics = true);
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
    ///     添加 Redis缓存
    /// </summary>
    public static IServiceCollection AddRedisCache(this IServiceCollection me)
    {
        var redisOptions = App.GetOptions<RedisOptions>().Instances.First(x => x.Name == Chars.FLG_REDIS_INSTANCE_DATA_CACHE);

        // IDistributedCache 分布式缓存通用接口
        _ = me.AddStackExchangeRedisCache(options => {
            // 连接字符串
            options.Configuration = redisOptions.ConnStr;
        });

        // Redis原生接口
        return me.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions.ConnStr));
    }

    /// <summary>
    ///     添加雪花编号生成器
    /// </summary>
    public static IServiceCollection AddSnowflake(this IServiceCollection me)
    {
        // 雪花漂移算法
        var workerId           = Environment.GetEnvironmentVariable(Chars.FLG_SNOWFLAKE_WORK_ID).Int32Try(0);
        var idGeneratorOptions = new IdGeneratorOptions((ushort)workerId) { WorkerIdBitLength = 6 };
        YitIdHelper.SetIdGenerator(idGeneratorOptions);
        return me;
    }

    #if DEBUG
    private static void MarkupLine(                     //
        string                                  msg     //
      , LogMessage                              message //
      , Dictionary<LogLevels, DisplayAttribute> logLevels)
    {
        msg = _consoleColors.Aggregate( //
            msg, (current, regex) => regex.Key.Replace(current, regex.Value));
        msg = msg.ReplaceLineEndings(string.Empty);
        var colorName = logLevels[(LogLevels)message.LogLevel].Name!;
        var (date, logName, logFormat) = ParseMessage(message, true);
        AnsiConsole.MarkupLine( //
            CultureInfo.InvariantCulture, logFormat, date, colorName, logName, message.ThreadId, msg);
    }

    #endif
    private static (string Date, string LogName, string LogFormat) ParseMessage(LogMessage message, bool showColor)
    {
        var date    = $"{message.LogDateTime:HH:mm:ss.ffffff}";
        var logName = message.LogName.PadRight(64, ' ')[^64..];
        var format = showColor
            ? $"[{nameof(ConsoleColor.Gray)}][[{{0}} {{1}} {{2,-{64}}} #{{3,4}}]][/] {{4}}"
            : $"[{{0}} {{1}} {{2,-{64}}} #{{3,4}}] {{4}}";

        return (date, logName, format);
    }
}