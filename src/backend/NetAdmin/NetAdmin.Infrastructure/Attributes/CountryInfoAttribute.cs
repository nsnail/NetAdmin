namespace NetAdmin.Infrastructure.Attributes;

/// <summary>
///     国家信息特性
/// </summary>
/// <remarks>
///     https://github.com/countries/countries
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class CountryInfoAttribute : Attribute
{
    /// <summary>
    ///     三个字母的国家代码
    /// </summary>
    public string Alpha3 { get; set; }

    /// <summary>
    ///     国际电话呼号
    /// </summary>
    public int CallingCode { get; set; }

    /// <summary>
    ///     国际电话子呼号（区分同一呼号不同国家）
    /// </summary>
    public string[] CallingSubCode { get; set; }

    /// <summary>
    ///     货币代码
    /// </summary>
    public string CurrencyCode { get; set; }

    /// <summary>
    ///     当命中多个国家时的首选国家
    /// </summary>
    public bool IsPreferred { get; set; }

    /// <summary>
    ///     官方语言代码
    /// </summary>
    public string[] Languages { get; set; }

    /// <summary>
    ///     国家全称
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    ///     国家简称
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    ///     时区
    /// </summary>
    public string[] Timezones { get; set; }

    /// <summary>
    ///     非正式名称
    /// </summary>
    public string[] UnofficialNames { get; set; }
}