using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.Domain.Attributes.DataValidation;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     信息：短信验证码
/// </summary>
public record SmsCodeInfo : DataAbstraction
{
    /// <summary>
    ///     短信验证码类型
    /// </summary>
    public enum Types
    {
        /// <summary>
        ///     绑定手机号
        /// </summary>
        LinkMobile = 1

       ,

        /// <summary>
        ///     登录
        /// </summary>
        Login = 2

       ,

        /// <summary>
        ///     解绑手机号
        /// </summary>
        UnlinkMobile = 3
    }

    /// <summary>
    ///     验证码
    /// </summary>
    [SmsCode]
    [JsonIgnore]
    public virtual string Code { get; init; }

    /// <summary>
    ///     创建时间
    /// </summary>
    public virtual DateTime CreateTime { get; init; }

    /// <summary>
    ///     手机号
    /// </summary>
    [Required]
    [Mobile]
    public virtual string Mobile { get; init; }
}