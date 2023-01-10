using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.Dto.Sys.Menu;

/// <summary>
///     请求：更新菜单
/// </summary>
public record UpdateMenuReq : CreateMenuReq
{
    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; set; }
}