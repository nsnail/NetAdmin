using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     请求日志表
/// </summary>
[Index(Chars.FLG_DB_INDEX_PREFIX             + nameof(ApiId),       nameof(ApiId),       false)]
[Index(Chars.FLG_DB_INDEX_PREFIX             + nameof(CreatedTime), nameof(CreatedTime), false)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_RequestLog))]
public record Sys_RequestLog : ImmutableEntity, IFieldCreatedClient
{
    /// <summary>
    ///     接口
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ApiId))]
    public Sys_Api Api { get; init; }

    /// <summary>
    ///     接口编号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [JsonIgnore]
    public virtual string ApiId { get; init; }

    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    [Column(Position = -1)]
    [JsonIgnore]
    public int? CreatedClientIp { get; init; }

    /// <summary>
    ///     创建者来源地址
    /// </summary>
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public string CreatedReferer { get; init; }

    /// <summary>
    ///     创建者客户端用户代理
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_1022)]
    #else
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string CreatedUserAgent { get; init; }

    /// <summary>
    ///     执行耗时（微秒）
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long Duration { get; init; }

    /// <summary>
    ///     程序响应码
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual ErrorCodes ErrorCode { get; init; }

    /// <summary>
    ///     异常信息
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string Exception { get; init; }

    /// <summary>
    ///     附加数据
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string ExtraData { get; init; }

    /// <summary>
    ///     HTTP状态码
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual int HttpStatusCode { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    [JsonIgnore]
    public virtual string Method { get; init; }

    /// <summary>
    ///     来源地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string ReferUrl { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求content-type
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [JsonIgnore]
    public virtual string RequestContentType { get; init; }

    /// <summary>
    ///     请求头信息
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string RequestHeaders { get; init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [JsonIgnore]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应内容
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string ResponseBody { get; init; }

    /// <summary>
    ///     响应content-type
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [JsonIgnore]
    public virtual string ResponseContentType { get; init; }

    /// <summary>
    ///     响应头
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [JsonIgnore]
    public virtual string ResponseHeaders { get; init; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual int? ServerIp { get; init; }
}