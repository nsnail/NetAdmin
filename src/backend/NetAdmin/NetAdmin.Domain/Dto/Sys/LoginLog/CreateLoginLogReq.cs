using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.LoginLog;

/// <summary>
///     请求：创建登录日志
/// </summary>
public sealed record CreateLoginLogReq : Sys_LoginLog, IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateRequestLogReq, CreateLoginLogReq>().MapWith(x => Convert(x));
    }

    private static CreateLoginLogReq Convert(CreateRequestLogReq s)
    {
        var              body      = s.Detail.ResponseBody.ToObject<RestfulInfo<LoginRsp>>();
        ContextUserToken userToken = null;

        // ReSharper disable once InvertIf
        if (body.Data?.AccessToken != null) {
            try {
                userToken = ContextUserToken.Create(body.Data.AccessToken);
            }
            catch {
                // ignored
            }
        }

        return new CreateLoginLogReq {
                                         Id               = s.Id
                                       , CreatedClientIp  = s.CreatedClientIp
                                       , CreatedTime      = s.CreatedTime
                                       , Duration         = s.Duration
                                       , HttpStatusCode   = s.HttpStatusCode
                                       , ErrorCode        = s.Detail.ErrorCode
                                       , RequestBody      = s.Detail.RequestBody
                                       , RequestHeaders   = s.Detail.RequestHeaders
                                       , RequestUrl       = s.Detail.RequestUrl
                                       , ResponseBody     = s.Detail.ResponseBody
                                       , ResponseHeaders  = s.Detail.ResponseHeaders
                                       , ServerIp         = s.Detail.ServerIp
                                       , CreatedUserAgent = s.Detail.CreatedUserAgent
                                       , OwnerId          = userToken?.Id
                                       , OwnerDeptId      = userToken?.DeptId
                                       , LoginUserName    = s.Detail.RequestBody?.ToObject<LoginByPwdReq>()?.Account
                                     };
    }
}