using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     验证码服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class VerifyCodeController
    (IVerifyCodeCache cache, ICaptchaCache captchaCache) : ControllerBase<IVerifyCodeCache, IVerifyCodeService>(cache)
                                                         , IVerifyCodeModule
{
    /// <inheritdoc />
    [NonAction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     发送验证码
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req)
    {
        await captchaCache.VerifyCaptchaAndRemoveAsync(req.VerifyCaptchaReq).ConfigureAwait(false);
        return await Cache.SendVerifyCodeAsync(req).ConfigureAwait(false);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryVerifyCodeRsp> UpdateAsync(UpdateVerifyCodeReq req)
    {
        return Cache.UpdateAsync(req);
    }

    /// <summary>
    ///     完成验证
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public Task<bool> VerifyAsync(VerifyCodeReq req)
    {
        return Cache.VerifyAsync(req);
    }
}