using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     短信服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class SmsController : ControllerBase<ISmsCache, ISmsService>, ISmsModule
{
    private readonly ICaptchaCache _captchaCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsController" /> class.
    /// </summary>
    public SmsController(ISmsCache cache, ICaptchaCache captchaCache) //
        : base(cache)
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
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建短信
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QuerySmsRsp> CreateAsync(CreateSmsReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除短信
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     短信是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QuerySmsReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个短信
    /// </summary>
    [NonAction]
    public Task<QuerySmsRsp> GetAsync(QuerySmsReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询短信
    /// </summary>
    public Task<PagedQueryRsp<QuerySmsRsp>> PagedQueryAsync(PagedQueryReq<QuerySmsReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询短信
    /// </summary>
    public Task<IEnumerable<QuerySmsRsp>> QueryAsync(QueryReq<QuerySmsReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task<SendSmsCodeRsp> SendSmsCodeAsync(SendSmsCodeReq req)
    {
        await _captchaCache.VerifyCaptchaAndRemoveAsync(req.VerifyCaptchaReq);
        return await Cache.SendSmsCodeAsync(req);
    }

    /// <summary>
    ///     更新短信
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QuerySmsRsp> UpdateAsync(UpdateSmsReq req)
    {
        return Cache.UpdateAsync(req);
    }

    /// <summary>
    ///     完成短信验证
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public Task<bool> VerifySmsCodeAsync(VerifySmsCodeReq req)
    {
        return Cache.VerifySmsCodeAsync(req);
    }
}