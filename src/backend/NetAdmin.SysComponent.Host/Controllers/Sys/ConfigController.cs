using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.Host.Attributes;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     配置服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class ConfigController(IConfigCache cache) : ControllerBase<IConfigCache, IConfigService>(cache)
                                                         , IConfigModule
{
    /// <summary>
    ///     批量删除配置
    /// </summary>
    [Transaction]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return Cache.BulkDeleteAsync(req);
    }

    /// <summary>
    ///     配置计数
    /// </summary>
    public Task<long> CountAsync(QueryReq<QueryConfigReq> req)
    {
        return Cache.CountAsync(req);
    }

    /// <summary>
    ///     创建配置
    /// </summary>
    [Transaction]
    public Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        return Cache.CreateAsync(req);
    }

    /// <summary>
    ///     删除配置
    /// </summary>
    [Transaction]
    public Task<int> DeleteAsync(DelReq req)
    {
        return Cache.DeleteAsync(req);
    }

    /// <summary>
    ///     编辑配置
    /// </summary>
    [Transaction]
    public Task<QueryConfigRsp> EditAsync(EditConfigReq req)
    {
        return Cache.EditAsync(req);
    }

    /// <summary>
    ///     配置是否存在
    /// </summary>
    [NonAction]
    public Task<bool> ExistAsync(QueryReq<QueryConfigReq> req)
    {
        return Cache.ExistAsync(req);
    }

    /// <summary>
    ///     获取单个配置
    /// </summary>
    public Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        return Cache.GetAsync(req);
    }

    /// <summary>
    ///     获取最新有效配置
    /// </summary>
    public Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        return Cache.GetLatestConfigAsync();
    }

    /// <summary>
    ///     分页查询配置
    /// </summary>
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        return Cache.PagedQueryAsync(req);
    }

    /// <summary>
    ///     查询配置
    /// </summary>
    public Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        return Cache.QueryAsync(req);
    }

    /// <summary>
    ///     设置配置启用状态
    /// </summary>
    public Task<int> SetEnabledAsync(SetConfigEnabledReq req)
    {
        return Cache.SetEnabledAsync(req);
    }
}