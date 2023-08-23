using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public record CreateUserReq : CreateUpdateUserReq, IRegister
{
    /// <inheritdoc cref="CreateUpdateUserReq.PasswordText" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码))]
    public override string PasswordText { get; init; }

    /// <inheritdoc cref="Sys_User.Profile" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户档案))]
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