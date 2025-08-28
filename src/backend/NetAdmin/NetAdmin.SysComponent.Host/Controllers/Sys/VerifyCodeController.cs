using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     验证码服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class VerifyCodeController(IVerifyCodeCache cache, ICaptchaCache captchaCache)
    : ControllerBase<IVerifyCodeCache, IVerifyCodeService>(cache), IVerifyCodeModule
{
    /// <summary>
    ///     批量删除验证码
    /// </summary>
    [NonAction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     验证码计数
    /// </summary>
    [NonAction]
    public Task<long> CountAsync(QueryReq<QueryVerifyCodeReq> req) {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     验证码分组计数
    /// </summary>
    [NonAction]
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryVerifyCodeReq> req) {
        return Cache.CountByAsync(req);
    }

    /// <summary>
    ///     创建验证码
    /// </summary>
    [NonAction]
    public Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req) {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除验证码
    /// </summary>
    [NonAction]
    public Task<int> DeleteAsync(DelReq req) {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑验证码
    /// </summary>
    [NonAction]
    [Transaction]
    public Task<QueryVerifyCodeRsp> EditAsync(EditVerifyCodeReq req) {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     导出验证码
    /// </summary>
    [NonAction]
    public Task<IActionResult> ExportAsync(QueryReq<QueryVerifyCodeReq> req) {
        return Cache.ExportAsync(req);
    }

    /// <summary>
    ///     获取单个验证码
    /// </summary>
    [NonAction]
    public Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req) {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     分页查询验证码
    /// </summary>
    [NonAction]
    public Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req) {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询验证码
    /// </summary>
    [NonAction]
    public Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req) {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     发送验证码
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public async Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req) {
        await captchaCache.VerifyCaptchaAndRemoveAsync(req.VerifyCaptchaReq).ConfigureAwait(false);
        return await Cache.SendVerifyCodeAsync(req).ConfigureAwait(false);
    }

    /// <summary>
    ///     验证码求和
    /// </summary>
    [NonAction]
    public Task<decimal> SumAsync(QueryReq<QueryVerifyCodeReq> req) {
        return Cache.SumAsync(req);
    }

    /// <summary>
    ///     完成验证
    /// </summary>
    [AllowAnonymous]
    [Transaction]
    public Task<bool> VerifyAsync(VerifyCodeReq req) {
        return Cache.VerifyAsync(req);
    }
}