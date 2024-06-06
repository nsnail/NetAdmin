using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Dto.Sys.SiteMsg;

namespace NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

/// <summary>
///     请求：编辑站内信
/// </summary>
public sealed record EditSiteMsgReq : CreateSiteMsgReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}