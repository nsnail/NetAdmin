using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：更新菜单
/// </summary>
public record UpdateMenuReq : CreateMenuReq
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}