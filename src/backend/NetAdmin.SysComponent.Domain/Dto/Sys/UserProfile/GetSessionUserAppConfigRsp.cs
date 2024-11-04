namespace NetAdmin.SysComponent.Domain.Dto.Sys.UserProfile;

/// <summary>
///     响应：获取当前用户应用配置
/// </summary>
public sealed record GetSessionUserAppConfigRsp : Sys_UserProfile
{
    /// <inheritdoc cref="Sys_UserProfile.AppConfig" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string AppConfig { get; set; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}