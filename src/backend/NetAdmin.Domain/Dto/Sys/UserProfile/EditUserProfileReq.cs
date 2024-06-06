using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     请求：编辑用户档案
/// </summary>
public sealed record EditUserProfileReq : CreateUserProfileReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}