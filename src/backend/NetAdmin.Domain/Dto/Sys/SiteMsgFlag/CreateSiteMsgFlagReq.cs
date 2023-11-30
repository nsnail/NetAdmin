using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

/// <summary>
///     请求：创建站内信标记
/// </summary>
public record CreateSiteMsgFlagReq : Sys_SiteMsgFlag
{
    /// <inheritdoc cref="Sys_SiteMsgFlag.SiteMsgId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long SiteMsgId { get; set; }

    /// <inheritdoc cref="Sys_SiteMsgFlag.UserSiteMsgStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(UserSiteMsgStatues))]
    public override UserSiteMsgStatues UserSiteMsgStatus { get; set; }
}