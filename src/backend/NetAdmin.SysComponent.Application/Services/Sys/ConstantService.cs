using Microsoft.OpenApi.Extensions;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Attributes;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IConstantService" />
public sealed class ConstantService : ServiceBase<IConstantService>, IConstantService
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
    public IDictionary<string, Dictionary<string, string[]>> GetEnums()
    {
        var ret = App.EffectiveTypes.Where(x => x.IsEnum && x.GetCustomAttribute<ExportAttribute>(false) != null)
                     .ToDictionary(x => x.Name, x => //
                                       x.GetEnumValues().Cast<Enum>().ToDictionary(y => y.ToString(), GetDicValue));

        ret.Add(                         //
            $"{nameof(HttpStatusCode)}s" //
          , Enum.GetNames<HttpStatusCode>()
                .ToDictionary(
                    x => x
                  , x => new[] {
                                   Convert.ToInt64(Enum.Parse<HttpStatusCode>(x), CultureInfo.InvariantCulture)
                                          .ToString(CultureInfo.InvariantCulture)
                                 , x
                               }));
        return ret;

        static string[] GetDicValue(Enum y)
        {
            var ret = new[] {
                                Convert.ToInt64(y, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture)
                              , y.ResDesc<Ln>()
                            };
            var indicate = y.GetAttributeOfType<IndicatorAttribute>()?.Indicate.ToLowerInvariant();
            return indicate.NullOrEmpty() ? ret : [..ret, indicate];
        }
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