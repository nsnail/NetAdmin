using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：检查手机号是否可用
/// </summary>
public record CheckMobileAvaliableReq : TbSysUser
{
    /// <inheritdoc cref="TbSysUser.Mobile" />
    [Mobile]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Mobile { get; init; }
}