using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IConstantService" />
public sealed class ConstantService : ServiceBase<IConstantService>, IConstantService
{
    /// <summary>
    ///     获得常量字符串
    /// </summary>
    public IDictionary<string, string> GetCharsDic()
    {
        return typeof(Chars).GetFields(BindingFlags.Public | BindingFlags.Static)
                            .Where(x => x.FieldType == typeof(string))
                            .ToImmutableSortedDictionary( //
                                x => x.Name, x => x.GetValue(null)?.ToString());
    }

    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    public IDictionary<string, Dictionary<string, string[]>> GetEnums()
    {
        return App.EffectiveTypes.Where(x => x.IsEnum && x.GetCustomAttribute<ExportAttribute>(false) != null)
                  .ToDictionary(x => x.Name, x => //
                                    x.GetEnumValues()
                                     .Cast<Enum>()
                                     .ToDictionary( //
                                         y => y.ToString()
                                       , y => new[] {
                                                        Convert.ToInt64(y, CultureInfo.InvariantCulture)
                                                               .ToString(CultureInfo.InvariantCulture)
                                                      , y.ResDesc<Ln>()
                                                    }));
    }

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    public IDictionary<string, string> GetLocalizedStrings()
    {
        return typeof(Ln).GetProperties(BindingFlags.Public | BindingFlags.Static)
                         .Where(x => x.PropertyType == typeof(string))
                         .ToImmutableSortedDictionary(x => x.Name, x => x.GetValue(null)?.ToString());
    }

    /// <summary>
    ///     获得数字常量表
    /// </summary>
    public IDictionary<string, long> GetNumbersDic()
    {
        return typeof(Numbers).GetFields(BindingFlags.Public | BindingFlags.Static)
                              .Where(x => x.FieldType == typeof(int) || x.FieldType == typeof(long))
                              .ToImmutableSortedDictionary( //
                                  x => x.Name, x => Convert.ToInt64(x.GetValue(null), CultureInfo.InvariantCulture));
    }
}