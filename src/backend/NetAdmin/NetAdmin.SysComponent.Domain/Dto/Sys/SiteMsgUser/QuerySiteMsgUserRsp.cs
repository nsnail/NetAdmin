namespace NetAdmin.SysComponent.Domain.Dto.Sys.SiteMsgUser;

/// <summary>
///     响应：查询站内信-用户映射
/// </summary>
public sealed record QuerySiteMsgUserRsp : Sys_SiteMsgUser
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}