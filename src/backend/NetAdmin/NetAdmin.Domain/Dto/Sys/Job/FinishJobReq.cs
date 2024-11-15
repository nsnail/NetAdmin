using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Job;

/// <summary>
///     请求：完成计划作业
/// </summary>
public sealed record FinishJobReq : Sys_Job, IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<QueryJobRsp, FinishJobReq>().Map(d => d.LastStatusCode, s => ((Sys_Job)s).LastStatusCode);
    }
}