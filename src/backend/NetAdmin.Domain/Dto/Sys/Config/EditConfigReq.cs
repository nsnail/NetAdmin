namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     请求：编辑配置
/// </summary>
public sealed record EditConfigReq : CreateConfigReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}