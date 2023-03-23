using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IConstantService" />
public class ConstantService : ServiceBase<IConstantService>, IConstantService
{
    /// <inheritdoc />
    public IDictionary<string, string> GetCharsDic()
    {
        return typeof(Chars).GetFields(BindingFlags.Public | BindingFlags.Static)
                            .Where(x => x.FieldType == typeof(string))
                            .ToImmutableSortedDictionary( //
                                x => x.Name, x => x.GetValue(null)?.ToString());
    }

    /// <inheritdoc />
    public IDictionary<string, Dictionary<string, string>> GetEnums()
    {
        return App.EffectiveTypes.Where(x => x.IsEnum && x.GetCustomAttribute<ExportAttribute>(false) is not null)
                  .ToDictionary(
                      x => x.Name, x => x.GetEnumValues().Cast<Enum>().ToDictionary(y => y.ToString(), y => y.Desc()));
    }

    /// <inheritdoc />
    public IDictionary<string, string> GetLocalizedStrings()
    {
        return typeof(Ln).GetProperties(BindingFlags.Public | BindingFlags.Static)
                         .Where(x => x.PropertyType == typeof(string))
                         .ToImmutableSortedDictionary(x => x.Name, x => x.GetValue(null)?.ToString());
    }

    /// <inheritdoc />
    public IDictionary<string, long> GetNumbersDic()
    {
        return typeof(Numbers).GetFields(BindingFlags.Public | BindingFlags.Static)
                              .Where(x => x.FieldType == typeof(int) || x.FieldType == typeof(long))
                              .ToImmutableSortedDictionary( //
                                  x => x.Name, x => Convert.ToInt64(x.GetValue(null), CultureInfo.InvariantCulture));
    }
}