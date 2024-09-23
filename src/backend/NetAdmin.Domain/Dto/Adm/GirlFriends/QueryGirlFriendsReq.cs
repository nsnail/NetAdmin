using NetAdmin.Domain.DbMaps.Adm;

namespace NetAdmin.Domain.Dto.Adm.GirlFriends;

/// <summary>
///     请求：查询女朋友
/// </summary>
public sealed record QueryGirlFriendsReq : Adm_GirlFriends
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}