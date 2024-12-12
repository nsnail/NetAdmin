namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     请求：查询配置
/// </summary>
public sealed record QueryConfigReq : Sys_Config
{
    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public new bool? Enabled { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}