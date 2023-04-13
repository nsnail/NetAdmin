using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     请求日志表
/// </summary>
[Table(Name = "Sys_RequestLog")]
public record Sys_RequestLog : ImmutableEntity
{
    /// <summary>
    ///     接口
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ApiId))]
    public Sys_Api Api { get; init; }

    /// <summary>
    ///     接口Id
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR127)]
    public virtual string ApiId { get; init; }

    /// <summary>
    ///     客户端IP
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR15)]
    public virtual string ClientIp { get; init; }

    /// <summary>
    ///     执行耗时（微秒）
    /// </summary>
    [JsonIgnore]
    public virtual long Duration { get; init; }

    /// <summary>
    ///     程序响应码
    /// </summary>
    [JsonIgnore]
    public virtual ErrorCodes ErrorCode { get; init; }

    /// <summary>
    ///     异常信息
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR255)]
    public virtual string Exception { get; init; }

    /// <summary>
    ///     附加数据
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR255)]
    public virtual string ExtraData { get; init; }

    /// <summary>
    ///     HTTP状态码
    /// </summary>
    [JsonIgnore]
    public virtual int HttpStatusCode { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR15)]
    public virtual string Method { get; init; }

    /// <summary>
    ///     来源地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR255)]
    public virtual string ReferUrl { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR255)]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求content-type
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR63)]
    public virtual string RequestContentType { get; init; }

    /// <summary>
    ///     请求头信息
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR255)]
    public virtual string RequestHeaders { get; init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR127)]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应内容
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR255)]
    public virtual string ResponseBody { get; init; }

    /// <summary>
    ///     响应content-type
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR63)]
    public virtual string ResponseContentType { get; init; }

    /// <summary>
    ///     响应头
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_NVARCHAR255)]
    public virtual string ResponseHeaders { get; init; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR15)]
    public virtual string ServerIp { get; init; }

    /// <summary>
    ///     浏览器标识
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR255)]
    public virtual string UserAgent { get; init; }
}