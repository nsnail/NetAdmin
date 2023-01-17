using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.Position;

/// <summary>
///     请求：查询岗位
/// </summary>
public record QueryPositionReq : TbSysPosition
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}