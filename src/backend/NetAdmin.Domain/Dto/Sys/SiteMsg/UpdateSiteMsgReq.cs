using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.SiteMsg;

/// <summary>
///     请求：更新站内信
/// </summary>
public sealed record UpdateSiteMsgReq : CreateSiteMsgReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}