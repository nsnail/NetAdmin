using NetAdmin.Domain.Attributes.DataValidation;

namespace NetAdmin.Domain.Dto.Sys.Email;

/// <summary>
///     信息：邮件
/// </summary>
public abstract record EmailCodeInfo : DataAbstraction
{
    /// <summary>
    ///     验证码
    /// </summary>
    [JsonIgnore]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码))]
    public virtual string Code { get; init; }

    /// <summary>
    ///     邮箱地址
    /// </summary>
    [JsonIgnore]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.电子邮箱))]
    [Email]
    public virtual string EmailAddress { get; init; }
}