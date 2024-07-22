using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     计划作业执行记录表
/// </summary>
[SqlIndex($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(JobId)}_{nameof(TimeId)}", $"{nameof(JobId)},{nameof(TimeId)}", true)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(CreatedTime),       $"{nameof(CreatedTime)} DESC",       false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(JobId),             nameof(JobId),                       false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(HttpStatusCode),    nameof(HttpStatusCode),              false)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_JobRecord))]
public record Sys_JobRecord : LiteImmutableEntity
{
    /// <summary>
    ///     执行耗时（毫秒）
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long Duration { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual HttpMethods HttpMethod { get; init; }

    /// <summary>
    ///     HTTP 状态码
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public int HttpStatusCode { get; init; }

    /// <summary>
    ///     拥有者信息
    /// </summary>
    [Ignore]
    [JsonIgnore]
    [Navigate(nameof(JobId))]
    public Sys_Job Job { get; init; }

    /// <summary>
    ///     作业编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long JobId { get; init; }

    /// <summary>
    ///     请求体
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
    ///     请求头
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [Ignore]
    [JsonIgnore]
    public virtual string RequestHeader { get; init; }

    /// <summary>
    ///     请求的网络地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [Ignore]
    [JsonIgnore]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应体
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
    ///     响应头
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [Ignore]
    [JsonIgnore]
    public virtual string ResponseHeader { get; init; }

    /// <summary>
    ///     执行时间编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long TimeId { get; init; }
}