using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     响应：导出计划作业
/// </summary>
public sealed record ExportJobRsp : QueryJobRsp
{
    /// <inheritdoc />
    [CsvIndex(5)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.上次执行状态))]
    public override string LastStatusCode => base.LastStatusCode;

    /// <inheritdoc />
    [CsvIndex(10)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(9)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.执行计划))]
    public override string ExecutionCron { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.请求方式))]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.作业名称))]
    public override string JobName { get; init; }

    /// <inheritdoc />
    [CsvIndex(7)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.上次执行耗时))]
    public override long? LastDuration { get; init; }

    /// <inheritdoc />
    [CsvIndex(6)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.上次执行时间))]
    public override DateTime? LastExecTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(8)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.下次执行时间))]
    public override DateTime? NextExecTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.作业状态))]
    public override JobStatues Status { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryUserRsp User { get; init; }
}