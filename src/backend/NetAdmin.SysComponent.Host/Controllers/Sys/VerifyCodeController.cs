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
public sealed class VerifyCodeController(IVerifyCodeCache cache, ICaptchaCache captchaCache)
    : ControllerBase<IVerifyCodeCache, IVerifyCodeService>(cache), IVerifyCodeModule
{
    /// <summary>
    ///     批量删除验证码
    /// </summary>
    [NonAction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     验证码计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建验证码
    /// </summary>
    [NonAction]
    public Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除验证码
    /// </summary>
    [NonAction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     判断验证码是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个验证码
    /// </summary>
    [NonAction]
    public Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询验证码
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询验证码
    /// </summary>
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

    /// <summary>
    ///     更新验证码
    /// </summary>
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