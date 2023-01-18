using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FreeSql;
using Furion;
using Furion.ConfigurableOptions;
using Furion.DependencyInjection;
using Furion.EventBus;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Events;
using NetAdmin.Host.Filters;
using NetAdmin.Host.Utils;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Utils;
using NSExt.Extensions;
using Spectre.Console;
using Yitter.IdGenerator;
using FreeSqlBuilder = NetAdmin.Infrastructure.Utils.FreeSqlBuilder;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
    private static readonly Dictionary<Regex, string> _consoleColors;

    static ServiceCollectionExtensions()
    {
        _consoleColors = new Dictionary<Regex, string> {
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
                                                             , @$"[{nameof(ConsoleColor.Blue)}]$1[/] "
                                                           }
                                                         , {
                                                               new Regex( //
                                                                   @"(SELECT) ", RegexOptions.Compiled)
                                                             , @$"[{nameof(ConsoleColor.Green)}]$1[/] "
                                                           }
                                                         , {
                                                               new Regex( //
                                                                   @"(UPDATE) ", RegexOptions.Compiled)
                                                             , @$"[{nameof(ConsoleColor.Yellow)}]$1[/] "
                                                           }
                                                         , {
                                                               new Regex( //
                                                                   @"(DELETE) ", RegexOptions.Compiled)
                                                             , @$"[{nameof(ConsoleColor.Red)}]$1[/] "
                                                           }
                                                         , {
                                                               new Regex( //
                                                                   @"(<s:.+?>)", RegexOptions.Compiled)
                                                             , @$"[underline {nameof(ConsoleColor.Gray)}]$1[/] "
                                                           }
                                                       };
    }

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
            configureMethod!.MakeGenericMethod(type).Invoke(me, new object[] { me });
            sbLog.Append(CultureInfo.InvariantCulture, $", {type.Name}");
        }

        LogHelper.Get<IServiceCollection>()
                 ?.Info($"{nameof(IConfigurableOptions)} {Ln.Initialization_completed} {sbLog}");
        return me;
    }

    /// <summary>
    ///     注册控制台日志模板
    /// </summary>
    public static IServiceCollection AddConsoleFormatter(this IServiceCollection me)
    {
        return me.AddConsoleFormatter(options => {
            options.WriteHandler = (message, _, _, _, _) => {
                var msg = _consoleColors.Aggregate( //
                    message.Message.EscapeMarkup()
                  , (current, regex) => regex.Key.Replace(current, regex.Value));

                AnsiConsole.MarkupLine( //
                    CultureInfo.InvariantCulture
                  , $"[{nameof(ConsoleColor.Gray)}][[{{0}} {{1}} {{2,-{64}}} #{{3,4}}]][/] {{4}}"
                  , message.LogDateTime.ToString(
                        Chars.TPL_DATE_HH_MM_SS_FFFFFF, CultureInfo.InvariantCulture)
                  , ((Enums.LogLevels)message.LogLevel).Desc()
                  , message.LogName.PadRight(64, ' ')[^64..], message.ThreadId, msg);
            };
        });
    }

    /// <summary>
    ///     注册上下文用户
    /// </summary>
    public static IServiceCollection AddContextUser(this IServiceCollection me)
    {
        me.AddScoped(typeof(ContextUser), _ => ContextUser.Create());
        return me;
    }

    /// <summary>
    ///     注册事件总线
    /// </summary>
    public static IServiceCollection AddEventBus(this IServiceCollection me)
    {
        me.AddEventBus(builder => { builder.AddSubscribers(App.Assemblies.ToArray()); });
        return me;
    }

    /// <summary>
    ///     注册freeSql orm工具
    /// </summary>
    public static IServiceCollection AddFreeSql(this IServiceCollection me)
    {
        var freeSql = new FreeSqlBuilder(App.GetOptions<DatabaseOptions>()).Build();
        me.AddSingleton(freeSql);

        var sqlAuditor = App.GetRequiredService<SqlAuditor>();

        freeSql.Aop.AuditValue += sqlAuditor.DataAuditHandler; // Insert/Update自动值处理

        // AOP事件发布（异步）
        freeSql.Aop.CommandBefore += (_, e) => {
            App.GetRequiredService<IEventPublisher>().PublishAsync(new SqlCommandBeforeEvent(e));
        }; // 增删查改，执行命令之前触发
        freeSql.Aop.CommandAfter += (_, e) => {
            App.GetRequiredService<IEventPublisher>().PublishAsync(new SqlCommandAfterEvent(e));
        }; // 增删查改，执行命令完成后触发

        freeSql.Aop.SyncStructureBefore += (_, e) => {
            App.GetRequiredService<IEventPublisher>().PublishAsync(new SyncStructureBeforeEvent(e));
        }; // CodeFirst迁移，执行之前触发

        freeSql.Aop.SyncStructureAfter += (_, e) => {
            App.GetRequiredService<IEventPublisher>().PublishAsync(new SyncStructureAfterEvent(e));
        }; // CodeFirst迁移，执行完成触发

        // 全局过滤器设置
        // freeSql.GlobalFilter.ApplyOnly<IFieldBitSet>( // 启用/禁用
        //     Strings.FLG_FILTER_BITSET, a => a.BitSet.HasFlag(BitSets.Enabled));
        // freeSql.GlobalFilter.ApplyOnly
        me.AddScoped<UnitOfWorkManager>();                    // 注入工作单元管理器
        me.AddFreeRepository(null, App.Assemblies.ToArray()); // 批量注入 Repository
        me.AddMvcFilter<TransactionInterceptor>();            // 注入事务拦截器

        return me;
    }

    /// <summary>
    ///     jwt授权处理器
    /// </summary>
    public static IServiceCollection AddJwt(this IServiceCollection me)
    {
        me.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
        return me;
    }

    /// <summary>
    ///     注册内存缓存
    /// </summary>
    public static IServiceCollection AddMemCache(this IServiceCollection me)
    {
        me.AddMemoryCache(options => { options.TrackStatistics = true; });
        return me;
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