using System.Net;
using Mapster;
using NetAdmin.DataContract.DbMaps;
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
                  dest => dest.Succeed, src => src.StatusCode == (int)HttpStatusCode.OK)
              .Map( //
                  dest => dest.UserName, src => src.RequestBody.Object<LoginReq>(null).UserName)

            //
            ;
    }
}