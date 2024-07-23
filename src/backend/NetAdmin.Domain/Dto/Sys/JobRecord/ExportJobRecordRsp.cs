using NetAdmin.Domain.Dto.Sys.Job;

namespace NetAdmin.Domain.Dto.Sys.JobRecord;

/// <summary>
///     响应：导出计划作业执行记录
/// </summary>
public record ExportJobRecordRsp : QueryJobRecordRsp, IRegister
{
    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.响应状态码))]
    public override string HttpStatusCode => base.HttpStatusCode;

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(6)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.执行耗时))]
    public override long Duration { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryJobRsp Job { get; init; }

    /// <summary>
    ///     作业名称
    /// </summary>
    [CsvIgnore(false)]
    [CsvIndex(4)]
    [CsvName(nameof(Ln.作业名称))]
    public string JobName { get; set; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(5)]
    [CsvName(nameof(Ln.响应体))]
    public override string ResponseBody { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_JobRecord, ExportJobRecordRsp>().Map(d => d.JobName, s => s.Job.JobName);
    }
}