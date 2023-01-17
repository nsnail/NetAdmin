using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.UserPosition;

/// <summary>
///     请求：查询用户-岗位映射
/// </summary>
public record QueryUserPositionReq : TbSysUserPosition
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}