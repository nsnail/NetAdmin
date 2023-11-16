using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsgUser;

/// <summary>
///     响应：查询站内信-用户映射
/// </summary>
public sealed record QuerySiteMsgUserRsp : Sys_SiteMsgUser
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}