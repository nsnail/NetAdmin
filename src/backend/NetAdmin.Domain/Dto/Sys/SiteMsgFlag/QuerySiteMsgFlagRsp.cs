namespace NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

/// <summary>
///     响应：查询站内信标记
/// </summary>
public sealed record QuerySiteMsgFlagRsp : Sys_SiteMsgFlag
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override UserSiteMsgStatues UserSiteMsgStatus { get; init; }
}