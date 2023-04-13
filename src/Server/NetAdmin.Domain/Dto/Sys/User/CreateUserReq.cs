using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public record CreateUserReq : Sys_User, IRegister
{
    /// <inheritdoc cref="Sys_User.Avatar" />
    [Url]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Avatar { get; init; }

    /// <inheritdoc cref="Sys_User.DeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DeptId { get; init; }

    /// <inheritdoc cref="Sys_User.Email" />
    [EmailAddress]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Email { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="Sys_User.Mobile" />
    [Mobile]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [Required]
    [Password]
    public virtual string PasswordText { get; init; }

    /// <summary>
    ///     岗位id列表
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> PositionIds { get; init; }

    /// <summary>
    ///     用户档案
    /// </summary>
    [Required]
    public new CreateUserProfileReq Profile { get; init; }

    /// <summary>
    ///     角色id列表
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [Required]
    [UserName]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserName { get; init; }

    /// <inheritdoc cref="IRegister.Register" />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateUserReq, Sys_User>()
                  .Map(dest => dest.Password, src => src.PasswordText.Pwd().Guid())
                  .Map(dest => dest.Token,    _ => Guid.NewGuid())
                  .Map( //
                      dest => dest.Roles
                    , src => src.RoleIds.NullOrEmpty()
                          ? Array.Empty<Sys_Role>()
                          : src.RoleIds.Select(x => new Sys_Role { Id = x }))
                  .Map( //
                      dest => dest.Positions
                    , src => src.PositionIds.NullOrEmpty()
                          ? Array.Empty<Sys_Position>()
                          : src.PositionIds.Select(x => new Sys_Position { Id = x }));
    }
}