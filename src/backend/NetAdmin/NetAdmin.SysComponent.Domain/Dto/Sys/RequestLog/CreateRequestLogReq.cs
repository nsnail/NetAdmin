using NetAdmin.SysComponent.Domain.Dto.Sys.RequestLogDetail;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.RequestLog;

/// <summary>
///     请求：创建请求日志
/// </summary>
public sealed record CreateRequestLogReq : Sys_RequestLog
{
    /// <inheritdoc cref="Sys_RequestLog.Detail" />
    public new CreateRequestLogDetailReq Detail { get; init; }
}