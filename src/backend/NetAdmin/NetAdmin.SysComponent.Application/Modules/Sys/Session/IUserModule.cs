using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

public partial interface IUserModule
{
    /// <summary>
    ///     获取当前用户应用配置
    /// </summary>
    Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync();

    /// <summary>
    ///     设置当前用户应用配置
    /// </summary>
    Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req);
}