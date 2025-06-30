using NetAdmin.Domain.Dto.Sys.Config;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     配置服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class ConfigController(IConfigCache cache) : ControllerBase<IConfigCache, IConfigService>(cache), IConfigModule
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
    ///     配置分组计数
    /// </summary>
    public Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryConfigReq> req)
    {
        return Cache.CountByAsync(req);
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
    ///     导出配置
    /// </summary>
    public Task<IActionResult> ExportAsync(QueryReq<QueryConfigReq> req)
    {
        return Cache.ExportAsync(req);
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
    ///     获取注册配置
    /// </summary>
    [AllowAnonymous]
    public Task<QueryConfigRsp> GetRegisterConfigAsync()
    {
        return Cache.GetRegisterConfigAsync();
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