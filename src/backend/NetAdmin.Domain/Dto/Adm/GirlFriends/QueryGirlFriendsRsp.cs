using NetAdmin.Domain.DbMaps.Adm;

namespace NetAdmin.Domain.Dto.Adm.GirlFriends;

/// <summary>
///     响应：查询女朋友
/// </summary>
public sealed record QueryGirlFriendsRsp : Adm_GirlFriends
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}