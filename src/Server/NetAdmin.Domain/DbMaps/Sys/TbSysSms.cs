using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     短信表
/// </summary>
[Table]
public record TbSysSms : MutableEntity
{
    /// <summary>
    ///     短信状态
    /// </summary>
    public enum Statues
    {
        /// <summary>
        ///     等待发送
        /// </summary>
        Wating = 1

       ,

        /// <summary>
        ///     已发送
        /// </summary>
        Sent = 2

       ,

        /// <summary>
        ///     发送失败
        /// </summary>
        Failed = 3

       ,

        /// <summary>
        ///     已校验
        /// </summary>
        Verified = 4
    }

    /// <summary>
    ///     短信类型
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

       ,

        /// <summary>
        ///     注册
        /// </summary>
        Register = 4

       ,

        /// <summary>
        ///     重设密码
        /// </summary>
        ResetPassword = 5
    }

    /// <summary>
    ///     验证码
    /// </summary>
    [JsonIgnore]
    [MaxLength(7)]
    public virtual string Code { get; init; }

    /// <summary>
    ///     短信内容
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Content { get; init; }

    /// <summary>
    ///     目的手机号
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    public virtual string DestMobile { get; init; }

    /// <summary>
    ///     发送报告
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Report { get; init; }

    /// <summary>
    ///     短信状态
    /// </summary>
    [JsonIgnore]
    public virtual Statues Status { get; init; }

    /// <summary>
    ///     短信类型
    /// </summary>
    [JsonIgnore]
    public virtual Types Type { get; init; }
}