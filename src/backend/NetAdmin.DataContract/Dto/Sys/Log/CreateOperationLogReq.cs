using Mapster;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.User;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.Log;

/// <summary>
///     请求：创建操作日志
/// </summary>
public record CreateOperationLogReq : TbSysOperationLog, IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateOperationLogReq, CreateLoginLogReq>()
              .Map( //
                  dest => dest.Succeed
                , src => src.ResponseBody != null && src.ResponseBody.Object<RestfulInfo<object>>(null).Code == 0)
              .Map( //
                  dest => dest.UserName, src => src.RequestBody.Object<ReqParameter<LoginReq>>(null).Req.UserName);
    }
}