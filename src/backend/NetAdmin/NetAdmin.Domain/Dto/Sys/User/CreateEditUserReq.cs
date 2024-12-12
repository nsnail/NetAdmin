namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：创建编辑用户
/// </summary>
public abstract record CreateEditUserReq : Sys_User
{
    /// <inheritdoc cref="Sys_User.Avatar" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Url(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.网络地址不正确))]
    public override string Avatar { get; init; }

    /// <inheritdoc cref="Sys_User.DeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DeptId { get; init; }

    /// <inheritdoc cref="Sys_User.Email" />
    [EmailAddress]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Email { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="Sys_User.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Mobile]
    public override string Mobile { get; init; }

    /// <summary>
    ///     登录密码
    /// </summary>
    [Password]
    public virtual string PasswordText { get; init; }

    /// <summary>
    ///     角色编号列表
    /// </summary>
    [MaxLength(Numbers.MAX_LIMIT_BULK_REQ)]
    [MinLength(1)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.角色编号列表不能为空))]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="Sys_User.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户名不能为空))]
    [UserName]
    public override string UserName { get; init; }
}