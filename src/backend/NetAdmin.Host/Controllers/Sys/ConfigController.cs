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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        return await Service.BulkDelete(req);
    }

    /// <summary>
    ///     创建配置
    /// </summary>
    [Transaction]
    public async Task<QueryConfigRsp> Create(CreateConfigReq req)
    {
        return await Service.Create(req);
    }

    /// <summary>
    ///     删除配置
    /// </summary>
    [Transaction]
    public async Task<int> Delete(DelReq req)
    {
        return await Service.Delete(req);
    }

    /// <summary>
    ///     获取最新有效配置
    /// </summary>
    public async Task<QueryConfigRsp> GetLatestConfig()
    {
        return await _configCache.GetLatestConfig();
    }

    /// <summary>
    ///     分页查询配置
    /// </summary>
    public async Task<PagedQueryRsp<QueryConfigRsp>> PagedQuery(PagedQueryReq<QueryConfigReq> req)
    {
        return await Service.PagedQuery(req);
    }

    /// <summary>
    ///     查询配置
    /// </summary>
    public async Task<IEnumerable<QueryConfigRsp>> Query(QueryReq<QueryConfigReq> req)
    {
        return await Service.Query(req);
    }

    /// <summary>
    ///     更新配置
    /// </summary>
    [Transaction]
    public async Task<QueryConfigRsp> Update(UpdateConfigReq req)
    {
        return await Service.Update(req);
    }
}