namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     请求：更新配置
/// </summary>
public record UpdateConfigReq : CreateConfigReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}