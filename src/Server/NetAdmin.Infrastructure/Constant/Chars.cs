#pragma warning disable CS1591

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     字符串常量表
/// </summary>
/// <remarks>
///     public类型会通过接口暴露给前端
/// </remarks>
public static class Chars
{
    public const string FLG_ACCESS_TOKEN               = "ACCESS-TOKEN";
    public const string FLG_APPLICATION_JSON           = "application/json";
    public const string FLG_CONSUL_REG_HOSTNAME        = "CONSUL_REG_HOSTNAME";
    public const string FLG_CONSUL_REG_PORT            = "CONSUL_REG_PORT";
    public const string FLG_CONTEXT_OWNER_DEPT_ID      = nameof(FLG_CONTEXT_OWNER_DEPT_ID);
    public const string FLG_CONTEXT_USER_ID            = nameof(FLG_CONTEXT_USER_ID);
    public const string FLG_CONTEXT_USER_INFO          = nameof(FLG_CONTEXT_USER_INFO);
    public const string FLG_DB_FIELD_TYPE_NVARCHAR     = "nvarchar";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_MAX = "nvarchar(max)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR255  = "nvarchar(255)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR      = "varchar";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_MAX  = "varchar(max)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR1022  = "varchar(1022)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR127   = "varchar(127)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR15    = "varchar(15)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR255   = "varchar(255)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR31    = "varchar(31)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR4094  = "varchar(4094)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR510   = "varchar(510)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR63    = "varchar(63)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR7     = "varchar(7)";
    public const string FLG_GLOBAL_FILTER_DATA         = nameof(FLG_GLOBAL_FILTER_DATA);
    public const string FLG_GLOBAL_FILTER_DELETE       = nameof(FLG_GLOBAL_FILTER_DELETE);
    public const string FLG_GLOBAL_FILTER_MEMBER       = nameof(FLG_GLOBAL_FILTER_MEMBER);
    public const string FLG_GLOBAL_FILTER_SELF         = nameof(FLG_GLOBAL_FILTER_SELF);
    public const string FLG_GLOBAL_FILTER_TENANT       = nameof(FLG_GLOBAL_FILTER_TENANT);
    public const string FLG_HEALTH_CHECK_PATH_PREFIX   = "health/check";
    public const string FLG_HTTP_METHOD_CONNECT        = "CONNECT";
    public const string FLG_HTTP_METHOD_DELETE         = "DELETE";
    public const string FLG_HTTP_METHOD_GET            = "GET";
    public const string FLG_HTTP_METHOD_HEAD           = "HEAD";
    public const string FLG_HTTP_METHOD_OPTIONS        = "OPTIONS";
    public const string FLG_HTTP_METHOD_PATCH          = "PATCH";
    public const string FLG_HTTP_METHOD_POST           = "POST";
    public const string FLG_HTTP_METHOD_PUT            = "PUT";
    public const string FLG_HTTP_METHOD_TRACE          = "TRACE";
    public const string FLG_REDIS_INSTANCE_DATACACHE   = "DataCache";
    public const string FLG_SNOWFLAKE_WORK_ID          = "SNOWFLAKE_WORK_ID";

    public const string FLG_UA_MOBILE
        = "Mozilla/5.0 (Linux; Android 9; Redmi Note 8 Pro Build/PPR1.180610.011; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.96 Mobile Safari/537.36";

    public const string FLG_UA_PC
        = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";

    public const string FLG_X_ACCESS_TOKEN = "X-ACCESS-TOKEN";
    public const string RGX_CERTIFICATE    = """^[a-zA-Z0-9-_]+$""";

    public const string RGX_EMAIL
        = """^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$""";

    public const string RGX_MOBILE    = """^1(3\d|4[5-9]|5[0-35-9]|6[6]|7[2-8]|8\d|9[0-35-9])\d{8}$""";
    public const string RGX_PASSWORD  = """^(?![0-9]+$)(?![a-zA-Z]+$).{8,16}$""";
    public const string RGX_SMSCODE   = """^\d{4}$""";
    public const string RGX_TELEPHONE = """^((\d{3,4}\-)|)\d{7,8}(|([-\u8f6c]{1}\d{1,5}))$""";
    public const string RGX_USERNAME  = """^[a-zA-Z0-9_]{4,16}$""";

    public const string TPL_DATE_HH_MM_SS_FFFFFF       = "HH:mm:ss.ffffff";
    public const string TPL_DATE_YYYY_MM_DD            = "yyyy-MM-dd";
    public const string TPL_DATE_YYYY_MM_DD_HH_MM_SS   = "yyyy-MM-dd HH:mm:ss";
    public const string TPL_DATE_YYYYMMDD              = "yyyyMMdd";
    public const string TPL_DATE_YYYYMMDDHHMMSSFFFZZZZ = "yyyyMMddHHmmssfffzzz";
}