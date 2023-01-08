#pragma warning disable CS1591

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     数字常量表（public类型会通过接口暴露给前端）
/// </summary>
public static class Numbers
{
    public const int QUERY_LIMIT        = 100;  //非分页查询允许返回的最大条数
    public const int QUERY_MAX_PAGENO   = 1000; //分页查询允许最大的页码
    public const int QUERY_MAX_PAGESIZE = 100;  //分页查询允许最大的页容量
}