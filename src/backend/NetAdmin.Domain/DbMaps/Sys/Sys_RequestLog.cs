using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     请求日志表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX + nameof(ApiPathCrc32),   nameof(ApiPathCrc32),          false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX + nameof(CreatedTime),    $"{nameof(CreatedTime)} DESC", false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX + nameof(OwnerId),        nameof(OwnerId),               false)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX + nameof(HttpStatusCode), nameof(HttpStatusCode),        false)]
[Table( //
    Name = $"{Chars.FLG_DB_TABLE_NAME_PREFIX}{nameof(Sys_RequestLog)}_{{yyyyMMdd}}"
  , AsTable = $"{nameof(CreatedTime)}=2024-1-1(1 day)")]
public record Sys_RequestLog : SimpleEntity, IFieldCreatedTime, IFieldOwner, IFieldCreatedClientIp
{
    /// <summary>
    ///     接口
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(ApiPathCrc32), TempPrimary = nameof(Sys_Api.PathCrc32))]
    public Sys_Api Api { get; init; }

    /// <summary>
    ///     接口路径CRC32
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int ApiPathCrc32 { get; init; }

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

    /// <summary>
    ///     明细
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(Id))]
    public Sys_RequestLogDetail Detail { get; init; }

    /// <summary>
    ///     执行耗时（毫秒）
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int Duration { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_TINY_INT)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual HttpMethods HttpMethod { get; init; }

    /// <summary>
    ///     HTTP状态码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_SMALL_INT)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int HttpStatusCode { get; init; }

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
    ///     请求跟踪标识
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual Guid TraceId { get; init; }
}