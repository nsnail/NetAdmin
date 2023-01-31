using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     短信服务
/// </summary>
public class SmsController : ControllerBase<ISmsService>, ISmsModule
{
    private readonly ICaptchaCache _captchaCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsController" /> class.
    /// </summary>
    public SmsController(ISmsService service, ICaptchaCache captchaCache) //
        : base(service)
    {
        _captchaCache = captchaCache;
    }

    /// <summary>
    ///     批量删除短信
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建短信
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<QuerySmsRsp> Create(CreateSmsReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除短信
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     分页查询短信
    /// </summary>
    public async Task<PagedQueryRsp<QuerySmsRsp>> PagedQuery(PagedQueryReq<QuerySmsReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询短信
    /// </summary>
    public async Task<IEnumerable<QuerySmsRsp>> Query(QueryReq<QuerySmsReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task<SendSmsCodeRsp> SendSmsCode(SendSmsCodeReq req)
    {
        #if DEBUG
        try {
            #endif
            await _captchaCache.VerifyCaptchaAndRemove(req.VerifyCaptchaReq);
            #if DEBUG
        }
        catch {
            // ignored
        }
        #endif
        var ret = await Service.SendSmsCode(req);

        return ret;
    }

    /// <summary>
    ///     更新短信
    /// </summary>
    [NonAction]
    [Transaction]
    public async Task<QuerySmsRsp> Update(UpdateSmsReq req)
    {
        return await Service.Update(req);
    }

    /// <summary>
    ///     完成短信验证
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task<bool> VerifySmsCode(VerifySmsCodeReq req)
    {
        return await Service.VerifySmsCode(req);
    }
}