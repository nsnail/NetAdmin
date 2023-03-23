using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Caches.Sys;

namespace NetAdmin.Host.Controllers.Sys;

/// <summary>
///     配置服务
/// </summary>
public class ConfigController : ControllerBase<IConfigService>, IConfigModule
{
    private readonly IConfigCache _configCache;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConfigController" /> class.
    /// </summary>
    public ConfigController(IConfigService service, IConfigCache configCache) //
        : base(service)
    {
        _configCache = configCache;
    }

    /// <summary>
    ///     批量删除配置
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Service.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     创建配置
    /// </summary>
    [Transaction]
    public Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        return Service.CreateAsync(req);
    }

    /// <summary>
    ///     删除配置
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Service.DeleteAsync(req);
    }

    /// <summary>
    ///     获取最新有效配置
    /// </summary>
    public Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        return _configCache.GetLatestConfigAsync();
    }

    /// <summary>
    ///     分页查询配置
    /// </summary>
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        return Service.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询配置
    /// </summary>
    public Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        return Service.QueryAsync(req);
    }

    /// <summary>
    ///     更新配置
    /// </summary>
    [Transaction]
    public Task<QueryConfigRsp> UpdateAsync(UpdateConfigReq req)
    {
        return Service.UpdateAsync(req);
    }
}