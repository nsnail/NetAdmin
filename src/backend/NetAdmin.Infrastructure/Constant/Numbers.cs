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
    public const int  DEF_PAGE_SIZE_QUERY = 20;  // 分页查询默认的页容量
    public const long DEF_SORT_VAL        = 100; // 排序默认值

    public const int  HTTP_STATUS_BIZ_FAIL    = 900;             // Http状态码-业务异常
    public const long ID_DIC_CATALOG_GEO_AREA = 379794295185413; // 唯一编号：字典目录-行政区划字典

    public const int MAX_LIMIT_BULK_REQ          = 100;   // 最大限制：批量请求数
    public const int MAX_LIMIT_EXPORT            = 10000; // 最大限制：导出为CSV文件的条数
    public const int MAX_LIMIT_PRINT_LEN_CONTENT = 4096;  // 最大限制：打印长度（HTTP 内容）
    public const int MAX_LIMIT_PRINT_LEN_SQL     = 4096;  // 最大限制：打印长度（SQL 语句）
    public const int MAX_LIMIT_QUERY             = 1000;  // 最大限制：非分页查询条数
    public const int MAX_LIMIT_QUERY_PAGE_NO     = 10000; // 最大限制：分页查询页码
    public const int MAX_LIMIT_QUERY_PAGE_SIZE   = 100;   // 最大限制：分页查询页容量

    public const int SECS_CACHE_CHART     = 300; // 秒：缓存时间-仪表
    public const int SECS_CACHE_DEFAULT   = 60;  // 秒：缓存时间-默认
    public const int SECS_RED_LOCK_EXPIRY = 30;  // 秒：RedLock-锁过期时间，假如持有锁的进程挂掉，最多在此时间内锁将被释放（如持有锁的进程正常，此值不会生效）
    public const int SECS_RED_LOCK_RETRY  = 1;   // 秒：RedLock-锁等待时间内，多久尝试获取一次
    public const int SECS_RED_LOCK_WAIT   = 10;  // 秒：RedLock-锁等待时间，相同的 resource 如果当前的锁被其他线程占用，最多等待时间
    public const int SECS_TIMEOUT_JOB     = 600; // 秒：超时时间-作业
}