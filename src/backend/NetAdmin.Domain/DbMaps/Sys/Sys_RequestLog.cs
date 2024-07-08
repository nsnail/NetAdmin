namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     请求日志表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(ApiId),          nameof(ApiId),          false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(CreatedTime),    nameof(CreatedTime),    false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(UserId),         nameof(UserId),         false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(HttpStatusCode), nameof(HttpStatusCode), false)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_RequestLog))]
public record Sys_RequestLog : SimpleEntity, IFieldCreatedTime, IFieldCreatedClient
{
    /// <summary>
    ///     接口
    /// </summary>
    [Ignore]
    [JsonIgnore]
    [Navigate(nameof(ApiId))]
    public Sys_Api Api { get; init; }

    /// <summary>
    ///     接口编号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [Ignore]
    [JsonIgnore]
    public virtual string ApiId { get; init; }

    /// <inheritdoc />
    [Column(Position = -1)]
    [Ignore]
    [JsonIgnore]
    public int? CreatedClientIp { get; init; }

    /// <inheritdoc />
    [Column(ServerTime = DateTimeKind.Local, CanUpdate = false, Position = -1)]
    [Ignore]
    [JsonIgnore]
    public virtual DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    #if DBTYPE_SQLSERVER
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_1022)]
    #else
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [Ignore]
    [JsonIgnore]
    public virtual string CreatedUserAgent { get; init; }

    /// <summary>
    ///     执行耗时（微秒）
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long Duration { get; init; }

    /// <summary>
    ///     程序响应码
    /// </summary>
    [Column]
    [Ignore]
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
    [Ignore]
    [JsonIgnore]
    public virtual string Exception { get; init; }

    /// <summary>
    ///     HTTP状态码
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual int HttpStatusCode { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    [Ignore]
    [JsonIgnore]
    public virtual string Method { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [Ignore]
    [JsonIgnore]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求content-type
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [Ignore]
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
    [Ignore]
    [JsonIgnore]
    public virtual string RequestHeaders { get; init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [Ignore]
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
    [Ignore]
    [JsonIgnore]
    public virtual string ResponseBody { get; init; }

    /// <summary>
    ///     响应content-type
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [Ignore]
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
    [Ignore]
    [JsonIgnore]
    public virtual string ResponseHeaders { get; init; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual int? ServerIp { get; init; }

    /// <summary>
    ///     请求跟踪标识
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [Ignore]
    [JsonIgnore]
    public virtual string TraceId { get; init; }

    /// <summary>
    ///     用户
    /// </summary>
    [Ignore]
    [JsonIgnore]
    [Navigate(nameof(UserId))]
    public Sys_User User { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long? UserId { get; init; }
}