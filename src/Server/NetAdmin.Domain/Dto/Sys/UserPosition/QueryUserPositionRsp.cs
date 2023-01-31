using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.UserPosition;

/// <summary>
///     响应：查询用户-岗位映射
/// </summary>
public record QueryUserPositionRsp : TbSysUserPosition
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}