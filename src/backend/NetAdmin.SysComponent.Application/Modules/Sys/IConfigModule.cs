using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Config;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     配置模块
/// </summary>
public interface IConfigModule : ICrudModule<CreateConfigReq, QueryConfigRsp // 创建类型
  , QueryConfigReq, QueryConfigRsp                                           // 查询类型
  , DelReq                                                                   // 删除类型
>
{
    /// <summary>
    ///     编辑配置
    /// </summary>
    Task<QueryConfigRsp> EditAsync(EditConfigReq req);

    /// <summary>
    ///     获取最新有效配置
    /// </summary>
    Task<QueryConfigRsp> GetLatestConfigAsync();

    /// <summary>
    ///     设置配置启用状态
    /// </summary>
    Task<int> SetEnabledAsync(SetConfigEnabledReq req);
}