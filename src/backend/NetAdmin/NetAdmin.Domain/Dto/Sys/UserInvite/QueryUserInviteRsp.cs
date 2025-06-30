using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     响应：查询用户邀请
/// </summary>
public record QueryUserInviteRsp : Sys_UserInvite
{
    /// <inheritdoc cref="Sys_UserInvite.Children" />
    public new virtual IEnumerable<QueryUserInviteRsp> Children { get; init; }

    /// <inheritdoc cref="Sys_UserInvite.CommissionRatio" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int CommissionRatio { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? CreatedUserId { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserName { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? ModifiedTime { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? ModifiedUserId { get; init; }

    /// <inheritdoc cref="IFieldModifiedUser.ModifiedUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ModifiedUserName { get; init; }

    /// <inheritdoc cref="Sys_UserInvite.Owner" />
    [CsvIgnore]
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="Sys_UserInvite.User" />
    [CsvIgnore]
    public new virtual QueryUserRsp User { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}