#pragma warning disable CS1591

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     字符串常量表（public类型会通过接口暴露给前端）
/// </summary>
public static class Strings
{
    public const string FLG_ACCESS_TOKEN        = "ACCESS-TOKEN";
    public const string FLG_APPLICATION_JSON    = "application/json";
    public const string FLG_FILTER_BITSET       = nameof(FLG_FILTER_BITSET);
    public const string FLG_HTTP_METHOD_CONNECT = "CONNECT";
    public const string FLG_HTTP_METHOD_DELETE  = "DELETE";
    public const string FLG_HTTP_METHOD_GET     = "GET";
    public const string FLG_HTTP_METHOD_HEAD    = "HEAD";
    public const string FLG_HTTP_METHOD_OPTIONS = "OPTIONS";
    public const string FLG_HTTP_METHOD_PATCH   = "PATCH";
    public const string FLG_HTTP_METHOD_POST    = "POST";
    public const string FLG_HTTP_METHOD_PUT     = "PUT";
    public const string FLG_HTTP_METHOD_TRACE   = "TRACE";

    public const string FLG_UA_MOBILE
        = "Mozilla/5.0 (Linux; Android 9; Redmi Note 8 Pro Build/PPR1.180610.011; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.96 Mobile Safari/537.36";

    public const string FLG_UA_PC
        = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";

    public const string FLG_X_ACCESS_TOKEN = "X-ACCESS-TOKEN";

    public const string RGX_MOBILE   = """^1(3\d|4[5-9]|5[0-35-9]|6[6]|7[2-8]|8\d|9[0-35-9])\d{8}$""";
    public const string RGX_PASSWORD = """^(?![0-9]+$)(?![a-zA-Z]+$).{8,16}$""";
    public const string RGX_SMSCODE  = """^\d{4}$""";
    public const string RGX_USERNAME = """^[a-zA-Z0-9_]{4,16}$""";

    public const string TPL_DATE_HH_MM_SS_FFFFFF       = "HH:mm:ss.ffffff";
    public const string TPL_DATE_YYYY_MM_DD            = "yyyy-MM-dd";
    public const string TPL_DATE_YYYY_MM_DD_HH_MM_SS   = "yyyy-MM-dd HH:mm:ss";
    public const string TPL_DATE_YYYYMMDD              = "yyyyMMdd";
    public const string TPL_DATE_YYYYMMDDHHMMSSFFFZZZZ = "yyyyMMddHHmmssfffzzz";
}