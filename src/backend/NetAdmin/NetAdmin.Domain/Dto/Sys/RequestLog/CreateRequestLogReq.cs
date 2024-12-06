using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLogDetail;

namespace NetAdmin.Domain.Dto.Sys.RequestLog;

/// <summary>
///     请求：创建请求日志
/// </summary>
public record CreateRequestLogReq : Sys_RequestLog
{
    /// <inheritdoc cref="Sys_RequestLog.Detail" />
    public new CreateRequestLogDetailReq Detail { get; init; }
}