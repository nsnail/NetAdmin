namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     请求：设置当前用户应用配置
/// </summary>
public record SetSessionUserAppConfigReq : Sys_UserProfile
{
    /// <inheritdoc cref="Sys_UserProfile.AppConfig" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonString]
    public override string AppConfig { get; set; }
}