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
    public const int  BULK_REQ_LIMIT           = 100;             // 批量请求允许的最大数量
    public const long DEF_SORT_VAL             = 100;             // 排序默认值
    public const long DIC_CATALOG_ID_GEO_AREA  = 379794295185413; // 字典目录id-行政区划字典
    public const int  HEART_TIMEOUT_SECS       = 600;             // 心跳超时时间
    public const int  HTTP_STATUS_BIZ_FAIL     = 900;             // Http状态码-业务异常
    public const int  QUERY_DEF_PAGESIZE       = 20;              // 分页查询默认的页容量
    public const int  QUERY_LIMIT              = 100;             // 非分页查询允许返回的最大条数
    public const int  QUERY_MAX_PAGENO         = 1000;            // 分页查询允许最大的页码
    public const int  QUERY_MAX_PAGESIZE       = 100;             // 分页查询允许最大的页容量
    public const int  REDLOCK_EXPIRY_TIME_SECS = 30;              // redlock 锁锁定过期时间，锁区域内的逻辑执行如果超过过期时间，锁将被释放
    public const int  REDLOCK_RETRY_TIME_SECS  = 1;               // redlock 锁等待时间内，多久尝试获取一次
    public const int  REDLOCK_WAIT_TIME_SECS   = 10;              // redlock 锁等待时间,相同的 resource 如果当前的锁被其他线程占用,最多等待时间
}