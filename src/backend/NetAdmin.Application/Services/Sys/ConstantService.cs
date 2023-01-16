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
    public dynamic GetEnums()
    {
        return App.EffectiveTypes.Where(x => x.IsEnum && x.GetCustomAttribute<ExportAttribute>(false) is not null)
                  .ToDictionary(x => x.Name, x => x.GetEnumValues()
                                                   .Cast<object>()
                                                   .ToImmutableSortedDictionary( //
                                                       x.GetEnumName, y => new { Value = y, Desc = ((Enum)y).Desc() }));
    }

    /// <inheritdoc />
    public dynamic GetLocalizedStrings()
    {
        return typeof(Str).GetProperties(BindingFlags.Public | BindingFlags.Static)
                          .Where(x => x.PropertyType == typeof(string))
                          .ToImmutableSortedDictionary(x => x.Name.ToString(), x => x.GetValue(null)?.ToString());
    }

    /// <inheritdoc />
    public dynamic GetStrings()
    {
        return typeof(Strings).GetFields(BindingFlags.Public | BindingFlags.Static)
                              .Where(x => x.FieldType == typeof(string))
                              .ToImmutableSortedDictionary( //
                                  x => x.Name.ToString(), x => x.GetValue(null)?.ToString());
    }
}