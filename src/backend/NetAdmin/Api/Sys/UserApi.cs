using Furion;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Sys.User;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Repositories;
using NSExt.Extensions;

namespace NetAdmin.Api.Sys;

/// <inheritdoc cref="NetAdmin.Api.Sys.IUserApi" />
public class UserApi : ApiCrud<TbSysUser, IUserApi>, IUserApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserApi" /> class.
    /// </summary>
    public UserApi(Repository<TbSysUser> repository) //
        : base(repository) { }

    /// <inheritdoc />
    public Task CreateUser(CreateUserReq req)
    {
        Console.WriteLine(req);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public async Task<LoginRsp> Login(LoginReq req)
    {
        var dbUser = await Repository.GetAsync(a => a.UserName == req.UserName &&
                                                    a.Password == req.Password.Pwd().Guid());
        if (dbUser is null) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidInput, Strings.MSG_UNAME_PASSWORD_WRONG);
        }

        if (!dbUser.BitSet.HasFlag(Enums.SysUserBits.Enabled)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Strings.MSG_USER_DISABLED);
        }

        var tokenPayload = new Dictionary<string, object> { { nameof(ContextUser), dbUser.Adapt<ContextUser>() } };

        var ret = new LoginRsp { AccessToken = JWTEncryption.Encrypt(tokenPayload) };
        ret.RefreshToken = JWTEncryption.GenerateRefreshToken(ret.AccessToken);

        // 设置响应报文头
        App.HttpContext.Response.Headers[Strings.FLAG_ACCESS_TOKEN]   = ret.AccessToken;
        App.HttpContext.Response.Headers[Strings.FLAG_X_ACCESS_TOKEN] = ret.RefreshToken;

        return ret;
    }
}