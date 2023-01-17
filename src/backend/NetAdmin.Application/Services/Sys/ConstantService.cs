using System.Collections.Immutable;
using System.Reflection;
using Furion;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Infrastructure.Attributes;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IConstantService" />
public class ConstantService : ServiceBase<IConstantService>, IConstantService
{
    /// <inheritdoc />
    public IDictionary<string, string> GetCharsDic()
    {
        var ret = typeof(Chars).GetFields(BindingFlags.Public | BindingFlags.Static)
                               .Where(x => x.FieldType == typeof(string))
                               .ToImmutableSortedDictionary( //
                                   x => x.Name.ToString(), x => x.GetValue(null)?.ToString());
        return ret;
    }

    /// <inheritdoc />
    public IDictionary<string, Dictionary<string, string>> GetEnums()
    {
        var ret = App.EffectiveTypes.Where(x => x.IsEnum && x.GetCustomAttribute<ExportAttribute>(false) is not null)
                     .ToDictionary(
                         x => x.Name
                       , x => x.GetEnumValues().Cast<Enum>().ToDictionary(y => y.ToString(), y => y.Desc()));
        return ret;
    }

    /// <inheritdoc />
    public IDictionary<string, string> GetLocalizedStrings()
    {
        var ret = typeof(Ln).GetProperties(BindingFlags.Public | BindingFlags.Static)
                            .Where(x => x.PropertyType == typeof(string))
                            .ToImmutableSortedDictionary(x => x.Name.ToString(), x => x.GetValue(null)?.ToString());
        return ret;
    }
}