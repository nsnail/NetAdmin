using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：用户登录
/// </summary>
public record LoginReq : TbSysUser
{
    /// <inheritdoc cref="TbSysUser.Password" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    [RegularExpression(//
        Strings.RGX_PASSWORD
                     , ErrorMessageResourceName = nameof(Str.NUMBER_LETTER_COMBINATION_OF_MORE_THAN_8_DIGITS)
                     , ErrorMessageResourceType = typeof(Str))]
    public new string Password { get; set; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    [RegularExpression(//
        Strings.RGX_USERNAME
                     , ErrorMessageResourceName = nameof(Str.MORE_THAN_4_DIGITS_ALPHANUMERIC_UNDERLINE)
                     , ErrorMessageResourceType = typeof(Str))
    ]
    public override string UserName { get; set; }
}