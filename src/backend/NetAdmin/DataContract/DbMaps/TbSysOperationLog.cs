using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     操作日志表
/// </summary>
[Table]
public record TbSysOperationLog : ImmutableEntity
{
    /// <summary>
    ///     操作
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string Action { get; init; }

    /// <summary>
    ///     客户端IP
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string ClientIp { get; init; }

    /// <summary>
    ///     控制器
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string Controller { get; init; }

    /// <summary>
    ///     执行耗时（ms）
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual uint Duration { get; init; }

    /// <summary>
    ///     服务端运行环境
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string Environment { get; init; }

    /// <summary>
    ///     异常信息
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string Exception { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string Method { get; init; }

    /// <summary>
    ///     来源地址
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string ReferUrl { get; init; }

    /// <summary>
    ///     请求content-type
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string RequestContentType { get; init; }

    /// <summary>
    ///     请求参数
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string RequestParameters { get; init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应原始类型
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string ResponseRawType { get; init; }

    /// <summary>
    ///     响应结果
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string ResponseResult { get; set; }

    /// <summary>
    ///     响应封装类型
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string ResponseWrapType { get; init; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string ServerIp { get; init; }

    /// <summary>
    ///     浏览器标识
    /// </summary>
    [Column(CanUpdate = false)]
    public virtual string UserAgent { get; init; }
}