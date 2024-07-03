using NetAdmin.Domain.Enums.Sys;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     计划作业表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_Job))]
public record Sys_Job : VersionEntity, IFieldEnabled, IFieldSummary
{
    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     执行时间计划
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [Ignore]
    [JsonIgnore]
    public virtual string ExecutionCron { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual HttpMethods HttpMethod { get; init; }

    /// <summary>
    ///     作业名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [Ignore]
    [JsonIgnore]
    public virtual string JobName { get; init; }

    /// <summary>
    ///     上次执行耗时
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long? LastDuration { get; init; }

    /// <summary>
    ///     上次执行时间
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual DateTime? LastExecTime { get; init; }

    /// <summary>
    ///     上次执行状态
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual HttpStatusCode? LastStatusCode { get; init; }

    /// <summary>
    ///     下次执行时间
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual DateTime? NextExecTime { get; init; }

    /// <summary>
    ///     下次执行时间编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long? NextTimeId { get; init; }

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
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [Ignore]
    [JsonIgnore]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     作业状态
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual JobStatues Status { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [Ignore]
    [JsonIgnore]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     执行用户
    /// </summary>
    [Ignore]
    [JsonIgnore]
    [Navigate(nameof(UserId))]
    public Sys_User User { get; init; }

    /// <summary>
    ///     执行用户编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public virtual long UserId { get; init; }
}