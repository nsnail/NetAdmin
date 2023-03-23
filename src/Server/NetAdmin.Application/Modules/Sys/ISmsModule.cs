using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Application.Modules.Sys;

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
    Task<bool> VerifySmsCodeAsync(VerifySmsCodeReq req);
}