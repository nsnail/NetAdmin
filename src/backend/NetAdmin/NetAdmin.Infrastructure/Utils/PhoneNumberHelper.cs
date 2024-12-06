using Microsoft.OpenApi.Extensions;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     电话号相关工具类
/// </summary>
public static class PhoneNumberHelper
{
    private static readonly IEnumerable<(string CallingCode, CountryCodes CountryCode)> _countryList;

    #pragma warning disable S3963
    static PhoneNumberHelper()
        #pragma warning restore S3963
    {
        _countryList = Enum.GetValues<CountryCodes>()
                           .SelectMany(x => {
                               var attribute = x.GetAttributeOfType<CountryInfoAttribute>();

                               // ReSharper disable once UseCollectionExpression
                               return (attribute.CallingSubCode ?? new[] { string.Empty }).Select(y => (attribute.CallingCode + y, x));
                           })
                           .OrderBy(x => x.Item1)
                           .ThenByDescending(x => x.x.GetAttributeOfType<CountryInfoAttribute>().IsPreferred)
                           .DistinctBy(x => x.Item1)
                           .OrderByDescending(x => x.Item1.Length);
    }

    /// <summary>
    ///     电话号码转国家代码
    /// </summary>
    public static CountryCodes PhoneNumberToCountryCode(string phoneNumber)
    {
        return _countryList.First(x => phoneNumber.Replace("+", string.Empty).Trim().StartsWith(x.CallingCode, StringComparison.Ordinal)).CountryCode;
    }
}