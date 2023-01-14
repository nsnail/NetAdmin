using System.Globalization;
using System.Reflection;
using System.Text;
using Furion;
using Furion.ConfigurableOptions;
using Furion.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NetAdmin.Infrastructure.Lang;
using NetAdmin.Infrastructure.Utils;
using NSExt.Extensions;
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
                 ?.Info($"{nameof(IConfigurableOptions)} {Str.Initialization_completed} {sbLog}");
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