using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.Menu;

/// <summary>
///     请求：查询菜单
/// </summary>
public record QueryMenuReq : TbSysMenu
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}