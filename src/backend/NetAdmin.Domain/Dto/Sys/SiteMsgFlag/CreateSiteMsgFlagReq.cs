using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

/// <summary>
///     请求：创建站内信标记
/// </summary>
public record CreateSiteMsgFlagReq : Sys_SiteMsgFlag
{
    /// <inheritdoc cref="Sys_SiteMsgFlag.SiteMsgId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long SiteMsgId { get; init; }

    /// <inheritdoc cref="Sys_SiteMsgFlag.UserSiteMsgStatus" />
    [EnumDataType(typeof(UserSiteMsgStatues), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.站内信状态不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override UserSiteMsgStatues UserSiteMsgStatus { get; init; }
}