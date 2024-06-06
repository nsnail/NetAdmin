using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.SysComponent.Application.Modules.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     站内信标记服务
/// </summary>
public interface ISiteMsgFlagService : IService, ISiteMsgFlagModule
{
    /// <summary>
    ///     设置用户站内信状态
    /// </summary>
    Task SetUserSiteMsgStatusAsync(SetUserSiteMsgStatusReq req);
}