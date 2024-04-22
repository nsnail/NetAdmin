using NetAdmin.Domain.DbMaps.Dependency;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     计划作业执行记录表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_JobRecord))]
[Index($"idx_{{tablename}}_{nameof(JobId)}_{nameof(TimeId)}", $"{nameof(JobId)},{nameof(TimeId)}", true)]
public record Sys_JobRecord : LiteImmutableEntity
{
    /// <summary>
    ///     执行耗时（毫秒）
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long Duration { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual HttpMethods HttpMethod { get; init; }

    /// <summary>
    ///     HTTP 状态码
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual int HttpStatusCode { get; init; }

    /// <summary>
    ///     作业编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long JobId { get; init; }

    /// <summary>
    ///     请求体
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求头
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string RequestHeader { get; init; }

    /// <summary>
    ///     请求的网络地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [JsonIgnore]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     响应体
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string ResponseBody { get; init; }

    /// <summary>
    ///     响应头
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string ResponseHeader { get; init; }

    /// <summary>
    ///     执行时间编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long TimeId { get; init; }
}