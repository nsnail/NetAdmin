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
    [CsvIndex(5)]
    [Ignore(false)]
    [Name(nameof(Ln.上次执行状态))]
    public override string LastStatusCode => base.LastStatusCode;

    /// <inheritdoc />
    [CsvIndex(10)]
    [Ignore(false)]
    [Name(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(9)]
    [Ignore(false)]
    [Name(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [Ignore(false)]
    [Name(nameof(Ln.执行计划))]
    public override string ExecutionCron { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [Ignore(false)]
    [Name(nameof(Ln.请求方式))]
    public override HttpMethods HttpMethod { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [Ignore(false)]
    [Name(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [Ignore(false)]
    [Name(nameof(Ln.作业名称))]
    public override string JobName { get; init; }

    /// <inheritdoc />
    [CsvIndex(7)]
    [Ignore(false)]
    [Name(nameof(Ln.上次执行耗时))]
    public override long? LastDuration { get; init; }

    /// <inheritdoc />
    [CsvIndex(6)]
    [Ignore(false)]
    [Name(nameof(Ln.上次执行时间))]
    public override DateTime? LastExecTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(8)]
    [Ignore(false)]
    [Name(nameof(Ln.下次执行时间))]
    public override DateTime? NextExecTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [Ignore(false)]
    [Name(nameof(Ln.作业状态))]
    public override JobStatues Status { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override QueryUserRsp User { get; init; }
}