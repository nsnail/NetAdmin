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
    public const int BULK_REQ_LIMIT     = 100;  // 批量请求允许的最大数量
    public const int HTTP_STATUS_FAIL   = 900;  // Http状态码-业务异常
    public const int QUERY_DEF_PAGESIZE = 20;   // 分页查询默认的页容量
    public const int QUERY_LIMIT        = 100;  // 非分页查询允许返回的最大条数
    public const int QUERY_MAX_PAGENO   = 1000; // 分页查询允许最大的页码
    public const int QUERY_MAX_PAGESIZE = 100;  // 分页查询允许最大的页容量
}