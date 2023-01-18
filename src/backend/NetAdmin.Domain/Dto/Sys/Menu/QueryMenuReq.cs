using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：查询菜单
/// </summary>
public record QueryMenuReq : TbSysMenu
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}