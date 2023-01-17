using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.UserPosition;

/// <summary>
///     响应：查询用户-岗位映射
/// </summary>
public record QueryUserPositionRsp : TbSysUserPosition
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}