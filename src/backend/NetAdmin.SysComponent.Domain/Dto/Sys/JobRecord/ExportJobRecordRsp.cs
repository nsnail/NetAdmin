using NetAdmin.SysComponent.Domain.Dto.Sys.Job;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.JobRecord;

/// <summary>
///     响应：导出计划作业执行记录
/// </summary>
public sealed record ExportJobRecordRsp : QueryJobRecordRsp, IRegister
{
    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.响应状态码))]
    public override string HttpStatusCode => base.HttpStatusCode;

    /// <inheritdoc />
    [CsvIndex(6)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.执行耗时))]
    public override long Duration { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryJobRsp Job { get; init; }

    /// <summary>
    ///     作业名称
    /// </summary>
    [CsvIndex(4)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.作业名称))]
    public string JobName { get; set; }

    /// <inheritdoc />
    [CsvIndex(5)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.响应体))]
    public override string ResponseBody { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_JobRecord, ExportJobRecordRsp>().Map(d => d.JobName, s => s.Job.JobName);
    }
}