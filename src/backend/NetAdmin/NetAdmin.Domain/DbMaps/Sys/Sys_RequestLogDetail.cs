namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     请求日志明细表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_RequestLogDetail))]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(CreatedTime), $"{nameof(CreatedTime)} DESC", false)]
public record Sys_RequestLogDetail : SimpleEntity, IFieldCreatedTime, IFieldCreatedClientUserAgent
{
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
    ///     程序响应码
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual ErrorCodes ErrorCode { get; init; }

    /// <summary>
    ///     异常信息
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Exception { get; init; }

    /// <summary>
    ///     请求内容
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求content-type
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestContentType { get; init; }

    /// <summary>
    ///     请求头信息
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestHeaders { get; init; }

    /// <summary>
    ///     请求地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应内容
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ResponseBody { get; init; }

    /// <summary>
    ///     响应content-type
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ResponseContentType { get; init; }

    /// <summary>
    ///     响应头
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ResponseHeaders { get; init; }

    /// <summary>
    ///     服务器IP
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int? ServerIp { get; init; }
}