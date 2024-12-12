namespace NetAdmin.Domain.Dto.Sys.RequestLogDetail;

/// <summary>
///     请求：查询请求日志明细
/// </summary>
public sealed record QueryRequestLogDetailReq : Sys_RequestLogDetail
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}