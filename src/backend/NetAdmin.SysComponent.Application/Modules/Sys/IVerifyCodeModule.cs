using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     验证码模块
/// </summary>
public interface IVerifyCodeModule : ICrudModule<CreateVerifyCodeReq, QueryVerifyCodeRsp // 创建类型
  , QueryVerifyCodeReq, QueryVerifyCodeRsp                                               // 查询类型
  , UpdateVerifyCodeReq, QueryVerifyCodeRsp                                              // 修改类型
  , DelReq                                                                               // 删除类型
>
{
    /// <summary>
    ///     发送验证码
    /// </summary>
    Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req);

    /// <summary>
    ///     完成验证
    /// </summary>
    /// <remarks>
    ///     对于验证失败的，不主动删除缓存，通过防火墙来应对暴力破解
    /// </remarks>
    Task<bool> VerifyAsync(VerifyCodeReq req);
}