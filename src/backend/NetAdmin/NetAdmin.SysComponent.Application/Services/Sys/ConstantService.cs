using NetAdmin.Domain.Attributes;

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

        var httpStatusCodes = Enum.GetNames<HttpStatusCode>().ToDictionary(x => x, GetHttpStatusCodeDicValue);
        httpStatusCodes.Add( //
            nameof(ErrorCodes.Unhandled)
          , [Numbers.HTTP_STATUS_BIZ_FAIL.ToInvString(), nameof(ErrorCodes.Unhandled), nameof(Indicates.Danger).ToLowerInvariant()]);
        ret.Add($"{nameof(HttpStatusCode)}s", httpStatusCodes);
        return ret;

        static string[] GetDicValue(Enum y)
        {
            var ret = new[] { Convert.ToInt64(y, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture), y.ResDesc<Ln>() };
            if (y is CountryCodes z) {
                return [..ret, z.GetCallingCode().ToInvString()];
            }

            var indicate = y.Attr<IndicatorAttribute>()?.Indicate.ToLowerInvariant();
            return indicate.NullOrEmpty() ? ret : [..ret, indicate];
        }

        static string[] GetHttpStatusCodeDicValue(string name)
        {
            var codeInt = Convert.ToInt64(Enum.Parse<HttpStatusCode>(name), CultureInfo.InvariantCulture);
            return [
                codeInt.ToString(CultureInfo.InvariantCulture), name
              , (codeInt switch { >= 200 and < 300 => nameof(Indicates.Success), < 400 => nameof(Indicates.Warning), _ => nameof(Indicates.Danger) })
                .ToLowerInvariant()
            ];
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