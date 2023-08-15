using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：当前用户信息
/// </summary>
public record UserInfoRsp : QueryUserRsp, IRegister
{
    /// <inheritdoc />
    public override QueryDeptRsp Dept { get; init; }

    /// <inheritdoc />
    public override IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc />
    public new void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_User, UserInfoRsp>() //
                  .IgnoreIf((s, _) => s.Mobile == null, d => d.Mobile)
                  .Map(d => d.Mobile, s => s.Mobile.MaskMobile())

            //
            ;
    }
}