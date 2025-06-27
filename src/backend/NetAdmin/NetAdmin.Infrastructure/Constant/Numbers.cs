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
    public const int  DEF_PAGE_SIZE_QUERY            = 20;              // 默认值：分页查询页容量
    public const long DEF_SORT_VAL                   = 100;             // 默认值：排序字段
    public const int  HTTP_STATUS_BIZ_FAIL           = 900;             // HTTP状态码：业务异常
    public const long ID_DIC_CATALOG_GEO_AREA        = 379794295185413; // 唯一编号：字典目录-行政区划字典
    public const int  MAX_LIMIT_BULK_REQ             = 100;             // 最大限制：批量请求数
    public const int  MAX_LIMIT_EXPORT               = 50000;           // 最大限制：导出为CSV文件的条数
    public const int  MAX_LIMIT_QUERY                = 1000;            // 最大限制：非分页查询条数
    public const int  MAX_LIMIT_QUERY_PAGE_NO        = 10000;           // 最大限制：分页查询页码
    public const int  MAX_LIMIT_QUERY_PAGE_SIZE      = 100;             // 最大限制：分页查询页容量
    public const int  MAX_LIMIT_RETRY_CNT_REDIS_LOCK = 10;              // 最大限制：Redis锁重试次数
    public const int  REQUEST_LOG_BUFF_SIZE          = 1000;            // 请求日志缓冲区大小
    public const int  SCHEDULED_JOB_PARALLEL_NUM     = 5;               // 计划作业并发数
    public const int  SECS_CACHE_CHART               = 300;             // 缓存时间（秒）：仪表
    public const int  SECS_CACHE_DEFAULT             = 60;              // 缓存时间（秒）：默认
    public const int  SECS_CACHE_DIC_CATALOG_CODE    = 300;             // 缓存时间（秒）：字典配置-目录代码
    public const int  SECS_CACHE_LOGIN_BY_USER_ID    = 3600 * 24 * 30;  // 缓存时间（秒）：通过用户编号登录的用户信息
    public const int  SECS_REDIS_LOCK_EXPIRY         = 60;              // 秒：Redis锁过期时间
    public const int  SECS_REDIS_LOCK_RETRY_DELAY    = 1;               // 秒：Redis锁重试间隔
    public const int  SECS_TIMEOUT_HTTP_CLIENT       = 15;              // 超时时间（秒）：HTTP 客户端
    public const int  SECS_TIMEOUT_JOB               = 180;             // 超时时间（秒）：作业
}