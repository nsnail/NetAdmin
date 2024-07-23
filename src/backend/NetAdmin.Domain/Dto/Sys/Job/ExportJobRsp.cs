using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     响应：导出计划作业
/// </summary>
public record ExportJobRsp : QueryJobRsp
{
    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(5)]
    [CsvName(nameof(Ln.上次执行状态))]
    public override string LastStatusCode => base.LastStatusCode;

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(10)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(9)]
    [CsvName(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(2)]
    [CsvName(nameof(Ln.执行计划))]
    public override string ExecutionCron { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(4)]
    [CsvName(nameof(Ln.请求方式))]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.作业名称))]
    public override string JobName { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(7)]
    [CsvName(nameof(Ln.上次执行耗时))]
    public override long? LastDuration { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(6)]
    [CsvName(nameof(Ln.上次执行时间))]
    public override DateTime? LastExecTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(8)]
    [CsvName(nameof(Ln.下次执行时间))]
    public override DateTime? NextExecTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.作业状态))]
    public override JobStatues Status { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryUserRsp User { get; init; }
}