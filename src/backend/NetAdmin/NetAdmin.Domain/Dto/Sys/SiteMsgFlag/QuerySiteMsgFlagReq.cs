namespace NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

/// <summary>
///     请求：查询站内信标记
/// </summary>
public sealed record QuerySiteMsgFlagReq : Sys_SiteMsgFlag
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}