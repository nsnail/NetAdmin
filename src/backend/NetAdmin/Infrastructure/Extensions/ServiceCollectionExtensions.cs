using System.Globalization;
using System.Reflection;
using FreeSql;
using Furion;
using Furion.ConfigurableOptions;
using Furion.DependencyInjection;
using NetAdmin.Aop.Pipelines;
using NetAdmin.Infrastructure.Configuration;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Utils;
using NSExt.Extensions;
using Spectre.Console;
using Yitter.IdGenerator;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
[SuppressSniffer]
public static class ServiceCollectionExtensions
{
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
                const int loggerWidth = 64;
                AnsiConsole.MarkupLine( //
                    CultureInfo.InvariantCulture
                  , $"[gray][[{{0}} {{1}} {{2,-{loggerWidth}}} #{{3,4}}]][/] {{4}}"
                  , message.LogDateTime.ToString(
                        "HH:mm:ss.ffffff", CultureInfo.InvariantCulture)
                  , ((Enums.LogLevels)message.LogLevel).Desc()
                  , message.LogName.PadRight(loggerWidth, ' ')[^loggerWidth..]
                  , message.ThreadId, Regexes.RegexDigitDot3.Replace( //
                        message.Message.EscapeMarkup(), "[bold yellow]$1[/]"));
            };
        });
    }

    /// <summary>
    ///     注册freeSql orm工具
    /// </summary>
    public static IServiceCollection AddFreeSql(this IServiceCollection me)
    {
        var options = App.GetConfig<DatabaseOptions>(nameof(DatabaseOptions).TrimEndOptions());
        var freeSql = FreeSqlHelper.Create(options);
        me.AddSingleton(freeSql);
        me.AddScoped<UnitOfWorkManager>();
        me.AddFreeRepository(null, App.Assemblies.ToArray());

        // 事务拦截器
        me.AddMvcFilter<TransactionInterceptor>();
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
        //雪花漂移算法
        var idGeneratorOptions = new IdGeneratorOptions(1) { WorkerIdBitLength = 6 };
        YitIdHelper.SetIdGenerator(idGeneratorOptions);
        return me;
    }
}