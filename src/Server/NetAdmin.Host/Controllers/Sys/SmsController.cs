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
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建短信
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QuerySmsRsp> CreateAsync(CreateSmsReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除短信
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     分页查询短信
    /// </summary>
    public Task<PagedQueryRsp<QuerySmsRsp>> PagedQueryAsync(PagedQueryReq<QuerySmsReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询短信
    /// </summary>
    public Task<IEnumerable<QuerySmsRsp>> QueryAsync(QueryReq<QuerySmsReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task<SendSmsCodeRsp> SendSmsCodeAsync(SendSmsCodeReq req)
    {
        #if DEBUG
        try {
            #endif
            await _captchaCache.VerifyCaptchaAndRemoveAsync(req.VerifyCaptchaReq);
            #if DEBUG
        }
        catch {
            // ignored
        }
        #endif
        return await Service.SendSmsCodeAsync(req);
    }

    /// <summary>
    ///     更新短信
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QuerySmsRsp> UpdateAsync(UpdateSmsReq req)
    {
        return Service.UpdateAsync(req);
    }

    /// <summary>
    ///     完成短信验证
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public Task<bool> VerifySmsCodeAsync(VerifySmsCodeReq req)
    {
        return Service.VerifySmsCodeAsync(req);
    }
}