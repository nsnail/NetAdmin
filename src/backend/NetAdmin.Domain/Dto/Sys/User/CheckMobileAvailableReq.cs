using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：检查手机号码是否可用
/// </summary>
public sealed record CheckMobileAvailableReq : Sys_User
{
    /// <inheritdoc cref="Sys_User.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Mobile]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.手机号码不能为空))]
    public override string Mobile { get; init; }
}