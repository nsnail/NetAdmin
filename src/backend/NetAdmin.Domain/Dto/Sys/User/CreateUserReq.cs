using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public sealed record CreateUserReq : CreateEditUserReq, IRegister
{
    /// <inheritdoc cref="CreateEditUserReq.PasswordText" />
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码不能为空))]
    public override string PasswordText { get; init; }

    /// <inheritdoc cref="Sys_User.Profile" />
    public new CreateUserProfileReq Profile { get; init; }

    /// <inheritdoc />
    public new void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<RegisterUserReq, CreateUserReq>() //
                  .Map(d => d.Mobile, s => s.VerifySmsCodeReq.DestDevice)

            //
            ;
    }
}