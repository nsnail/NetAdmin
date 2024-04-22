using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
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
    [JsonIgnore]
    [Column]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     执行时间计划
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string ExecutionCron { get; init; }

    /// <summary>
    ///     请求方法
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual HttpMethods HttpMethod { get; init; }

    /// <summary>
    ///     作业名称
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string JobName { get; init; }

    /// <summary>
    ///     上次执行时间
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual DateTime? LastExecTime { get; init; }

    /// <summary>
    ///     上次执行状态
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual HttpStatusCode? LastStatusCode { get; init; }

    /// <summary>
    ///     下次执行时间
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual DateTime? NextExecTime { get; init; }

    /// <summary>
    ///     下次执行时间编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long? NextTimeId { get; init; }

    /// <summary>
    ///     请求体
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string RequestBody { get; init; }

    /// <summary>
    ///     请求头
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string RequestHeader { get; init; }

    /// <summary>
    ///     请求的网络地址
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string RequestUrl { get; init; }

    /// <summary>
    ///     作业状态
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual JobStatues Status { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     执行用户
    /// </summary>
    [Navigate(nameof(UserId))]
    [JsonIgnore]
    public Sys_User User { get; init; }

    /// <summary>
    ///     执行用户编号
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual long UserId { get; init; }
}