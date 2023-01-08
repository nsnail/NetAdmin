#pragma warning disable CS1591

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     字符串常量表（public类型会通过接口暴露给前端）
/// </summary>
public static class Strings
{
    public const string DSC_CREATED_TIME       = "创建时间";
    public const string DSC_CREATED_USER_ID    = "创建者Id";
    public const string DSC_CREATED_USER_NAME  = "创建者";
    public const string DSC_ID                 = "主键Id";
    public const string DSC_IS_DELETED         = "是否删除";
    public const string DSC_MODIFIED_TIME      = "修改时间";
    public const string DSC_MODIFIED_USER_ID   = "修改者Id";
    public const string DSC_MODIFIED_USER_NAME = "修改者";
    public const string DSC_VERSION            = "版本";

    public const string FLG_ACCESS_TOKEN        = "ACCESS-TOKEN";
    public const string FLG_HTTP_METHOD_CONNECT = "CONNECT";
    public const string FLG_HTTP_METHOD_DELETE  = "DELETE";
    public const string FLG_HTTP_METHOD_GET     = "GET";
    public const string FLG_HTTP_METHOD_HEAD    = "HEAD";
    public const string FLG_HTTP_METHOD_OPTIONS = "OPTIONS";
    public const string FLG_HTTP_METHOD_PATCH   = "PATCH";
    public const string FLG_HTTP_METHOD_POST    = "POST";
    public const string FLG_HTTP_METHOD_PUT     = "PUT";
    public const string FLG_HTTP_METHOD_TRACE   = "TRACE";
    public const string FLG_X_ACCESS_TOKEN      = "X-ACCESS-TOKEN";

    public const string FMT_UA_MOBILE
        = "Mozilla/5.0 (Linux; Android 9; Redmi Note 8 Pro Build/PPR1.180610.011; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.96 Mobile Safari/537.36";

    public const string FMT_UA_PC
        = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";

    public const string FMT_YYYY_MM_DD            = "yyyy-MM-dd";
    public const string FMT_YYYY_MM_DD_HH_MM_SS   = "yyyy-MM-dd HH:mm:ss";
    public const string FMT_YYYYMMDD              = "yyyyMMdd";
    public const string FMT_YYYYMMDDHHMMSSFFFZZZZ = "yyyyMMddHHmmssfffzzz";

    public const string RGX_MOBILE   = """^1(3\d|4[5-9]|5[0-35-9]|6[6]|7[2-8]|8\d|9[0-35-9])\d{8}$""";
    public const string RGX_PASSWORD = """^(?![0-9]+$)(?![a-zA-Z]+$).{8,16}$""";
    public const string RGX_SMSCODE  = """^\d{4}$""";
    public const string RGX_USERNAME = """^[a-zA-Z0-9_]{4,16}$""";

    public const string TMP_LOG_OUPUT
        = "[{Timestamp:HH:mm:ss.fff} {Level:u3} {SourceContext,64}] {Message:lj}{NewLine}{Exception}";

    public const string TMP_SMSCODE            = "您正在进行 {0} 操作，验证码为：{1}，5分钟内有效，如非本人操作，请忽略。";
    public const string TMP_TRYSEND_SECS_AFTER = "{0} 秒后，可再次发送";
}