using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     短信模块
/// </summary>
public interface ISmsModule : ICrudModule<CreateSmsReq, QuerySmsRsp // 创建类型
  , QuerySmsReq, QuerySmsRsp                                        // 查询类型
  , UpdateSmsReq, QuerySmsRsp                                       // 修改类型
  , DelReq                                                          // 删除类型
>
{
    /// <summary>
    ///     发送短信验证码
    /// </summary>
    Task<SendSmsCodeRsp> SendSmsCodeAsync(SendSmsCodeReq req);

    /// <summary>
    ///     完成短信验证
    /// </summary>
    /// <remarks>
    ///     对于验证失败的，不主动删除缓存，通过防火墙来应对暴力破解
    /// </remarks>
    Task<bool> VerifySmsCodeAsync(VerifySmsCodeReq req);
}