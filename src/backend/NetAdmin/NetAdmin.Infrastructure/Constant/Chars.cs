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
    public const string FLG_CONTEXT_MEMBER_INFO = nameof(FLG_CONTEXT_MEMBER_INFO);
    public const string FLG_CONTEXT_OWNER_DEPT_ID = nameof(FLG_CONTEXT_OWNER_DEPT_ID);
    public const string FLG_CONTEXT_USER_ID = nameof(FLG_CONTEXT_USER_ID);
    public const string FLG_CONTEXT_USER_INFO = nameof(FLG_CONTEXT_USER_INFO);
    public const string FLG_CRON_PER_SECS = "* * * * * *";
    public const string FLG_DB_EXCEPTION_IDX = "idx_";
    public const string FLG_DB_EXCEPTION_PRIMARY_KEY_CONFLICT = "PRIMARY KEY";
    public const string FLG_DB_EXCEPTION_UNIQUE_CONSTRAINT_CONFLICT = "UNIQUE constraint";
    public const string FLG_DB_FIELD_TYPE_CHAR_34 = "char(34)";
    public const string FLG_DB_FIELD_TYPE_CHAR_6 = "char(6)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR = "nvarchar";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_1022 = "nvarchar(1022)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_127 = "nvarchar(127)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_15 = "nvarchar(15)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_255 = "nvarchar(255)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_31 = "nvarchar(31)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_4094 = "nvarchar(4094)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_510 = "nvarchar(510)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_63 = "nvarchar(63)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_7 = "nvarchar(7)";
    public const string FLG_DB_FIELD_TYPE_NVARCHAR_MAX = "nvarchar(max)";
    public const string FLG_DB_FIELD_TYPE_SMALL_INT = "smallint";
    public const string FLG_DB_FIELD_TYPE_TEXT = "text";
    public const string FLG_DB_FIELD_TYPE_TINY_INT = "tinyint";
    public const string FLG_DB_FIELD_TYPE_VARCHAR = "varchar";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_1022 = "varchar(1022)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_127 = "varchar(127)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_15 = "varchar(15)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_255 = "varchar(255)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_31 = "varchar(31)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_4094 = "varchar(4094)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_510 = "varchar(510)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_63 = "varchar(63)";
    public const string FLG_DB_FIELD_TYPE_VARCHAR_7 = "varchar(7)";
    public const string FLG_DB_INDEX_PREFIX = "idx_{tablename}_";
    public const string FLG_DB_TABLE_NAME_PREFIX = "";
    public const string FLG_DIC_CATALOG_NEW_USER_ROLE_CONFIG = "new-user-role-config";
    public const string FLG_FREE_SQL_GLOBAL_FILTER_DELETE = nameof(FLG_FREE_SQL_GLOBAL_FILTER_DELETE);
    public const string FLG_FREE_SQL_GLOBAL_FILTER_DEPT = nameof(FLG_FREE_SQL_GLOBAL_FILTER_DEPT);
    public const string FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN = nameof(FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_CHILDREN);
    public const string FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON = nameof(FLG_FREE_SQL_GLOBAL_FILTER_DEPT_WITH_SON);
    public const string FLG_FREE_SQL_GLOBAL_FILTER_MEMBER = nameof(FLG_FREE_SQL_GLOBAL_FILTER_MEMBER);
    public const string FLG_FREE_SQL_GLOBAL_FILTER_SELF = nameof(FLG_FREE_SQL_GLOBAL_FILTER_SELF);
    public const string FLG_FREE_SQL_GLOBAL_FILTER_TENANT = nameof(FLG_FREE_SQL_GLOBAL_FILTER_TENANT);
    public const string FLG_FRONT_APP_SET_HOME_GRID = "APP_SET_HOME_GRID";
    public const string FLG_HTTP_HEADER_KEY_ACCESS_TOKEN = "ACCESS-TOKEN";
    public const string FLG_HTTP_HEADER_KEY_AUTHORIZATION = "Authorization";
    public const string FLG_HTTP_HEADER_KEY_REFERER = "Referer";
    public const string FLG_HTTP_HEADER_KEY_TRON_PRO_API_KEY = "TRON-PRO-API-KEY";
    public const string FLG_HTTP_HEADER_KEY_USER_AGENT = "User-Agent";
    public const string FLG_HTTP_HEADER_KEY_X_ACCESS_TOKEN = "X-ACCESS-TOKEN";
    public const string FLG_HTTP_HEADER_KEY_X_ACCESS_TOKEN_HEADER_KEY = "X-Authorization";
    public const string FLG_HTTP_HEADER_KEY_X_CACHE_CONTROL = "X-Cache-Control";
    public const string FLG_HTTP_HEADER_KEY_X_FORWARDED_FOR = "X-Forwarded-For";
    public const string FLG_HTTP_HEADER_KEY_X_REAL_IP = "X-Real-IP";
    public const string FLG_HTTP_HEADER_VALUE_APPLICATION_JSON = "application/json";
    public const string FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM = "application/octet-stream";
    public const string FLG_HTTP_HEADER_VALUE_APPLICATION_URLENCODED = "application/x-www-form-urlencoded";
    public const string FLG_HTTP_HEADER_VALUE_ATTACHMENT = "attachment";
    public const string FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA = "Bearer";
    public const string FLG_HTTP_HEADER_VALUE_NO_CACHE = "no-cache";
    public const string FLG_HTTP_METHOD_CONNECT = "CONNECT";
    public const string FLG_HTTP_METHOD_DELETE = "DELETE";
    public const string FLG_HTTP_METHOD_GET = "GET";
    public const string FLG_HTTP_METHOD_HEAD = "HEAD";
    public const string FLG_HTTP_METHOD_OPTIONS = "OPTIONS";
    public const string FLG_HTTP_METHOD_PATCH = "PATCH";
    public const string FLG_HTTP_METHOD_POST = "POST";
    public const string FLG_HTTP_METHOD_PUT = "PUT";
    public const string FLG_HTTP_METHOD_TRACE = "TRACE";
    public const string FLG_PATH_API_METRICS = "metrics";
    public const string FLG_PATH_API_RPOBE = "api/probe";
    public const string FLG_PATH_API_SYS_USER_LOGIN_BY_PWD = "api/sys/user/login.by.pwd";
    public const string FLG_PATH_WEBSOCKET_PREFIX = "ws";
    public const string FLG_REDIS_INSTANCE_DATA_CACHE = "DataCache";
    public const string FLG_SNOWFLAKE_WORK_ID = "SNOWFLAKE_WORK_ID";

    public const string FLGL_DB_FIELD_TYPE_VARCHAR_MAX =
            #if DBTYPE_SQLSERVER
            "varchar(max)"
        #else
            "text"
        #endif
        ;

    public const string FLGL_HTTP_HEADER_VALUE_UA_MOBILE
        = "Mozilla/5.0 (Linux; Android 9; Redmi Note 8 Pro Build/PPR1.180610.011; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/78.0.3904.96 Mobile Safari/537.36";

    public const string FLGL_HTTP_HEADER_VALUE_UA_PC
        = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";

    public const string FLGL_VISIBLE_ASCIIS = """!"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~""";

    public const string RGX_CERTIFICATE = "^[a-zA-Z0-9-_]+$";
    public const string RGX_INVITE_CODE = """^\d{8}$""";
    public const string RGX_MOBILE = """^1(3\d|4[5-9]|5[0-35-9]|6[6]|7[2-8]|8\d|9[0-35-9])\d{8}$""";
    public const string RGX_PASSWORD = "^(?![0-9]+$)(?![a-zA-Z]+$).{8,16}$";
    public const string RGX_PAY_PASSWORD = """^\d{6}$""";
    public const string RGX_TELEPHONE = """^((\d{3,4}\-)|)\d{7,8}(|([-\u8f6c]{1}\d{1,5}))$""";
    public const string RGX_UP_AND_LOWER_NUMBER = """^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$""";
    public const string RGX_URL = """^https?://[\x21-\x7E]+$""";
    public const string RGX_USERNAME = """^[\u4e00-\u9fa5a-zA-Z0-9_-]{2,16}$""";
    public const string RGX_VERIFY_CODE = """^\d{4}$""";

    public const string RGXL_CHINESE_NAME
        = """^(?:赵|钱|孙|李|周|吴|郑|王|冯|陈|褚|卫|蒋|沈|韩|杨|朱|秦|尤|许|何|吕|施|张|孔|曹|严|华|金|魏|陶|姜|戚|谢|邹|喻|柏|水|窦|章|云|苏|潘|葛|奚|范|彭|郎|鲁|韦|昌|马|苗|凤|花|方|俞|任|袁|柳|酆|鲍|史|唐|费|廉|岑|薛|雷|贺|倪|汤|滕|殷|罗|毕|郝|邬|安|常|乐|于|时|傅|皮|卞|齐|康|伍|余|元|卜|顾|孟|平|黄|和|穆|萧|尹|姚|邵|湛|汪|祁|毛|禹|狄|米|贝|明|臧|计|伏|成|戴|谈|宋|茅|庞|熊|纪|舒|屈|项|祝|董|梁|杜|阮|蓝|闵|席|季|麻|强|贾|路|娄|危|江|童|颜|郭|梅|盛|林|刁|钟|徐|邱|骆|高|夏|蔡|田|樊|胡|凌|霍|虞|万|支|柯|昝|管|卢|莫|经|房|裘|缪|干|解|应|宗|丁|宣|贲|邓|郁|单|杭|洪|包|诸|左|石|崔|吉|钮|龚|程|嵇|邢|滑|裴|陆|荣|翁|荀|羊|於|惠|甄|曲|家|封|芮|羿|储|靳|汲|邴|糜|松|井|段|富|巫|乌|焦|巴|弓|牧|隗|山|谷|车|侯|宓|蓬|全|郗|班|仰|秋|仲|伊|宫|宁|仇|栾|暴|甘|钭|厉|戎|祖|武|符|刘|景|詹|束|龙|叶|幸|司|韶|郜|黎|蓟|薄|印|宿|白|怀|蒲|邰|从|鄂|索|咸|籍|赖|卓|蔺|屠|蒙|池|乔|阴|胥|能|苍|双|闻|莘|党|翟|谭|贡|劳|逄|姬|申|扶|堵|冉|宰|郦|雍|郤|璩|桑|桂|濮|牛|寿|通|边|扈|燕|冀|郏|浦|尚|农|温|别|庄|晏|柴|瞿|阎|充|慕|连|茹|习|宦|艾|鱼|容|向|古|易|慎|戈|廖|庾|终|暨|居|衡|步|都|耿|满|弘|匡|国|文|寇|广|禄|阙|东|欧|殳|沃|利|蔚|越|夔|隆|师|巩|厍|聂|晁|勾|敖|融|冷|訾|辛|阚|那|简|饶|空|曾|毋|沙|乜|养|鞠|须|丰|巢|关|蒯|相|查|後|荆|红|游|竺|权|逯|盖|益|桓|公|万俟|司马|上官|欧阳|夏侯|诸葛|闻人|东方|赫连|皇甫|尉迟|公羊|澹台|公冶|宗政|濮阳|淳于|单于|太叔|申屠|公孙|仲孙|轩辕|令狐|钟离|宇文|长孙|慕容|鲜于|闾丘|司徒|司空|亓官|司寇|仉|督|子车|颛孙|端木|巫马|公西|漆雕|乐正|壤驷|公良|拓跋|夹谷|宰父|谷梁|晋|楚|闫|法|汝|鄢|涂|钦|段干|百里|东郭|南门|呼延|归|海|羊舌|微生|岳|帅|缑|亢|况|后|有|琴|梁丘|左丘|东门|西门|商|牟|佘|佴|伯|赏|南宫|墨|哈|谯|笪|年|爱|阳|佟|第五|言|福)[\u4e00-\u9fa5]{1,3}$""";

    public const string RGXL_CRON
        = "^\\s*($|#|\\w+\\s*=|(\\?|\\*|(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?(?:,(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?)*)\\s+(\\?|\\*|(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?(?:,(?:[0-5]?\\d)(?:(?:-|\\/|\\,)(?:[0-5]?\\d))?)*)\\s+(\\?|\\*|(?:[01]?\\d|2[0-3])(?:(?:-|\\/|\\,)(?:[01]?\\d|2[0-3]))?(?:,(?:[01]?\\d|2[0-3])(?:(?:-|\\/|\\,)(?:[01]?\\d|2[0-3]))?)*)\\s+(\\?|\\*|(?:0?[1-9]|[12]\\d|3[01])(?:(?:-|\\/|\\,)(?:0?[1-9]|[12]\\d|3[01]))?(?:,(?:0?[1-9]|[12]\\d|3[01])(?:(?:-|\\/|\\,)(?:0?[1-9]|[12]\\d|3[01]))?)*)\\s+(\\?|\\*|(?:[1-9]|1[012])(?:(?:-|\\/|\\,)(?:[1-9]|1[012]))?(?:L|W)?(?:,(?:[1-9]|1[012])(?:(?:-|\\/|\\,)(?:[1-9]|1[012]))?(?:L|W)?)*|\\?|\\*|(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?(?:,(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)(?:(?:-)(?:JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC))?)*)\\s+(\\?|\\*|(?:[0-6])(?:(?:-|\\/|\\,|#)(?:[0-6]))?(?:L)?(?:,(?:[0-6])(?:(?:-|\\/|\\,|#)(?:[0-6]))?(?:L)?)*|\\?|\\*|(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?(?:,(?:MON|TUE|WED|THU|FRI|SAT|SUN)(?:(?:-)(?:MON|TUE|WED|THU|FRI|SAT|SUN))?)*)(|\\s)+(\\?|\\*|(?:|\\d{4})(?:(?:-|\\/|\\,)(?:|\\d{4}))?(?:,(?:|\\d{4})(?:(?:-|\\/|\\,)(?:|\\d{4}))?)*))$";

    public const string RGXL_EMAIL
        = """^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$""";

    public const string RGXL_IP_V4 = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|[0-1]?[0-9]{1,2})){3}$";
}