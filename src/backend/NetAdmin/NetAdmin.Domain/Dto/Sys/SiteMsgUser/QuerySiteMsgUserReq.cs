using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsgUser;

/// <summary>
///     请求：查询站内信-用户映射
/// </summary>
public sealed record QuerySiteMsgUserReq : Sys_SiteMsgUser
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}