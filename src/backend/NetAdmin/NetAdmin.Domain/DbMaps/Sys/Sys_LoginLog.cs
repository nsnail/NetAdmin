namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     登录日志表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(CreatedTime),    $"{nameof(CreatedTime)} DESC", false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(HttpStatusCode), nameof(HttpStatusCode),        false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(OwnerId),        nameof(OwnerId),               false)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_LoginLog))]
public record Sys_LoginLog : SimpleEntity, IFieldCreatedTime, IFieldOwner, IFieldCreatedClientIp, IFieldCreatedClientUserAgent
{
    /// <summary>
    ///     创建者客户端IP
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int? CreatedClientIp { get; init; }

    /// <summary>
    ///     创建时间
    /// </summary>
    [Column(ServerTime = DateTimeKind.Local, CanUpdate = false, Position = -1)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual DateTime CreatedTime { get; init; }

    /// <summary>
    ///     创建者客户端用户代理
    /// </summary>
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

    /// <summary>
    ///     拥有者部门编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <summary>
    ///     拥有者用户编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestBody { get; protected init; }

    /// <summary>
    ///     请求头信息
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
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
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ResponseBody { get; protected init; }

    /// <summary>
    ///     响应头
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
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