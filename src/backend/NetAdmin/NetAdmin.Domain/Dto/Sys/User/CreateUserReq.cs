using NetAdmin.Domain.Dto.Sys.UserInvite;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public   record CreateUserReq : CreateEditUserReq
{
    /// <inheritdoc />
    public override bool Enabled { get; init; } = true;

    /// <inheritdoc cref="Sys_User.Invite" />
    public new CreateUserInviteReq Invite { get; init; }

    /// <inheritdoc />
    public override string InviteCode { get; init; } = ((long)new[] { 60466176, int.MaxValue }.Rand()).Sys36().ToUpperInvariant();

    /// <inheritdoc cref="CreateEditUserReq.PasswordText" />
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码不能为空))]
    public override string PasswordText { get; init; }

    /// <inheritdoc cref="Sys_User.Profile" />
    public new CreateUserProfileReq Profile { get; init; }

    /// <inheritdoc />
    public override void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<RegisterUserReq, CreateUserReq>() //
                  .Ignore(a => a.InviteCode)
                  .Map(d => d.Mobile, s => s.VerifySmsCodeReq.DestDevice)

            //
            ;
    }
}