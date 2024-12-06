using Microsoft.OpenApi.Extensions;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     CountryCodes 扩展方法
/// </summary>
public static class CountryCodesExtensions
{
    /// <summary>
    ///     获取国际电话呼号
    /// </summary>
    public static int GetCallingCode(this CountryCodes me)
    {
        return me.GetAttributeOfType<CountryInfoAttribute>().CallingCode;
    }
}