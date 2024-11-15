using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.RequestLog;

/// <summary>
///     请求：查询请求日志
/// </summary>
public sealed record QueryRequestLogReq : Sys_RequestLog
{
    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }
}