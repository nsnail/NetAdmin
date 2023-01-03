using System.Reflection;
using FreeSql;
using Furion;
using Furion.ConfigurableOptions;
using NetAdmin.Aop.Filters;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Utils;
using Yitter.IdGenerator;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     ServiceCollection 扩展方法
/// </summary>
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
        me.AddScoped<TransactionHandler>();
        return me;
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