using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     短信缓存
/// </summary>
public sealed class SmsCache : DistributedCache<ISmsService>, IScoped, ISmsCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsCache" /> class.
    /// </summary>
    public SmsCache(IDistributedCache cache, ISmsService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySmsRsp> CreateAsync(CreateSmsReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QuerySmsReq> req)
    {
        return Service.ExistAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySmsRsp> GetAsync(QuerySmsReq req)
    {
        return Service.GetAsync(req);
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QuerySmsRsp>> PagedQueryAsync(PagedQueryReq<QuerySmsReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <inheritdoc />
    public Task<IEnumerable<QuerySmsRsp>> QueryAsync(QueryReq<QuerySmsReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <inheritdoc />
    public Task<SendSmsCodeRsp> SendSmsCodeAsync(SendSmsCodeReq req)
    {
        return Service.SendSmsCodeAsync(req);
    }

    /// <inheritdoc />
    public Task<QuerySmsRsp> UpdateAsync(UpdateSmsReq req)
    {
        return Service.UpdateAsync(req);
    }

    /// <inheritdoc />
    public Task<bool> VerifySmsCodeAsync(VerifySmsCodeReq req)
    {
        return Service.VerifySmsCodeAsync(req);
    }
}