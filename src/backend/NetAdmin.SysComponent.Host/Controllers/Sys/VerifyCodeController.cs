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
public sealed class VerifyCodeController : ControllerBase<IVerifyCodeCache, IVerifyCodeService>, IVerifyCodeModule
{
    private readonly ICaptchaCache _captchaCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="VerifyCodeController" /> class.
    /// </summary>
    public VerifyCodeController(IVerifyCodeCache cache, ICaptchaCache captchaCache) //
        : base(cache)
    {
        _captchaCache = captchaCache;
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     发送验证码
    /// </summary>
    [Transaction]
    [AllowAnonymous]
    public async Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req)
    {
        await _captchaCache.VerifyCaptchaAndRemoveAsync(req.VerifyCaptchaReq);
        return await Cache.SendVerifyCodeAsync(req);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<QueryVerifyCodeRsp> UpdateAsync(UpdateVerifyCodeReq req)
    {
        throw new NotImplementedException();
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