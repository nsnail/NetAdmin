namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     请求：更新用户档案
/// </summary>
public record UpdateUserProfileReq : CreateUserProfileReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}