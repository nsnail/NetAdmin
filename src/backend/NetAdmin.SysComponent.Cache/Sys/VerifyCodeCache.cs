using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IVerifyCodeCache" />
public sealed class VerifyCodeCache(IDistributedCache cache, IVerifyCodeService service) //
    : DistributedCache<IVerifyCodeService>(cache, service), IScoped, IVerifyCodeCache
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Service.CountAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Service.ExportAsync(req);
    }

    /// <inheritdoc />
    public Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc cref="IVerifyCodeModule.SendVerifyCodeAsync" />
    public Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req)
    {
        return Service.SendVerifyCodeAsync(req);
    }

    /// <inheritdoc cref="IVerifyCodeModule.VerifyAsync" />
    public Task<bool> VerifyAsync(VerifyCodeReq req)
    {
        return Service.VerifyAsync(req);
    }
}