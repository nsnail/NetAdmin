#pragma warning disable CS1591

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     数字常量表
/// </summary>
/// <remarks>
///     public类型会通过接口暴露给前端
/// </remarks>
public static class Numbers
{
    public const int DEF_PAGE_SIZE_QUERY            = 20;    // 默认值：分页查询页容量
    public const int HTTP_STATUS_BIZ_FAIL           = 900;   // HTTP状态码：业务异常
    public const int MAX_LIMIT_BULK_REQ             = 100;   // 最大限制：批量请求数
    public const int MAX_LIMIT_EXPORT               = 50000; // 最大限制：导出为CSV文件的条数
    public const int MAX_LIMIT_QUERY                = 1000;  // 最大限制：非分页查询条数
    public const int MAX_LIMIT_QUERY_PAGE_NO        = 10000; // 最大限制：分页查询页码
    public const int MAX_LIMIT_QUERY_PAGE_SIZE      = 100;   // 最大限制：分页查询页容量
    public const int MAX_LIMIT_RETRY_CNT_REDIS_LOCK = 10;    // 最大限制：Redis锁重试次数
    public const int SECS_REDIS_LOCK_EXPIRY         = 60;    // 秒：Redis锁过期时间
    public const int SECS_REDIS_LOCK_RETRY_DELAY    = 1;     // 秒：Redis锁重试间隔
}