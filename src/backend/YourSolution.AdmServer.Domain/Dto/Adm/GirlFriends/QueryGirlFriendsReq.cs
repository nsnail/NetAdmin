using NetAdmin.Domain.DbMaps.Dependency;
using YourSolution.AdmServer.Domain.DbMaps.Adm;

namespace YourSolution.AdmServer.Domain.Dto.Adm.GirlFriends;

/// <summary>
///     请求：查询女朋友
/// </summary>
public sealed record QueryGirlFriendsReq : Adm_GirlFriends
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}