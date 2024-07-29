namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     登录日志表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(CreatedTime),    $"{nameof(CreatedTime)} DESC", false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(HttpStatusCode), nameof(HttpStatusCode),        false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(OwnerId),        nameof(OwnerId),               false)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_LoginLog))]
public record Sys_LoginLog : SimpleEntity, IFieldCreatedTime, IFieldOwner, IFieldCreatedClientIp
                           , IFieldCreatedClientUserAgent
{
    /// <inheritdoc />
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int? CreatedClientIp { get; init; }

    /// <inheritdoc />
    [Column(ServerTime = DateTimeKind.Local, CanUpdate = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    #if DBTYPE_SQLSERVER
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_1022)]
    #else
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CreatedUserAgent { get; init; }

    /// <summary>
    ///     执行耗时（毫秒）
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int Duration { get; protected init; }

    /// <summary>
    ///     程序响应码
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual ErrorCodes ErrorCode { get; protected init; }

    /// <summary>
    ///     HTTP状态码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_SMALL_INT)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int HttpStatusCode { get; init; }

    /// <summary>
    ///     登录用户名
    /// </summary>
    [Column(Position = -1, DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string LoginUserName { get; protected init; }

    /// <summary>
    ///     拥有者
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public Sys_User Owner { get; init; }

    /// <inheritdoc />
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <inheritdoc />
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestBody { get; protected init; }

    /// <summary>
    ///     请求头信息
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestHeaders { get; protected init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestUrl { get; protected init; }

    /// <summary>
    ///     响应内容
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ResponseBody { get; protected init; }

    /// <summary>
    ///     响应头
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ResponseHeaders { get; protected init; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int? ServerIp { get; protected init; }
}