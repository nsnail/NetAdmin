using NetAdmin.SysComponent.Domain.Dto.Sys.Dept;
using NetAdmin.SysComponent.Domain.Dto.Sys.Role;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.User;

/// <summary>
///     响应：当前用户信息
/// </summary>
public sealed record UserInfoRsp : QueryUserRsp
{
    /// <inheritdoc cref="Sys_User.Dept" />
    public override QueryDeptRsp Dept { get; init; }

    /// <inheritdoc cref="Sys_User.Roles" />
    public override IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc />
    public override void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_User, UserInfoRsp>() //
                  .IgnoreIf((s, _) => s.Mobile == null, d => d.Mobile)
                  .Map(d => d.Mobile, s => s.Mobile.MaskMobile())

            //
            ;
    }
}