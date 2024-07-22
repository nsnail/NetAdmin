namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：查询用户精简版
/// </summary>
public record QueryUserLiteRsp : Sys_User
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string UserName { get; init; }
}