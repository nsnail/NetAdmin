using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：更新菜单
/// </summary>
public record UpdateMenuReq : CreateMenuReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long Id { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long Version { get; init; }
}