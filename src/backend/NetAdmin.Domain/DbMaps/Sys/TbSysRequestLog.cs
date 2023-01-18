using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     请求日志表
/// </summary>
[Table]
public record TbSysRequestLog : ImmutableEntity
{
    /// <summary>
    ///     接口
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ApiId))]
    public virtual TbSysApi Api { get; init; }

    /// <summary>
    ///     接口Id
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string ApiId { get; init; }

    /// <summary>
    ///     客户端IP
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    public virtual string ClientIp { get; init; }

    /// <summary>
    ///     执行耗时（ms）
    /// </summary>
    [JsonIgnore]
    public virtual int Duration { get; init; }

    /// <summary>
    ///     异常信息
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Exception { get; init; }

    /// <summary>
    ///     附加数据
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string ExtraData { get; set; }

    /// <summary>
    ///     HTTP状态码
    /// </summary>
    public virtual int HttpStatusCode { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    public virtual string Method { get; init; }

    /// <summary>
    ///     来源地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string ReferUrl { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求content-type
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string RequestContentType { get; init; }

    /// <summary>
    ///     请求头信息
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string RequestHeaders { get; init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应内容
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string ResponseBody { get; set; }

    /// <summary>
    ///     响应content-type
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string ResponseContentType { get; init; }

    /// <summary>
    ///     响应头
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string ResponseHeaders { get; set; }

    /// <summary>
    ///     程序响应码
    /// </summary>
    public virtual Enums.RspCodes RspCode { get; set; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [JsonIgnore]
    [MaxLength(7)]
    public virtual string ServerIp { get; init; }

    /// <summary>
    ///     浏览器标识
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string UserAgent { get; init; }
}