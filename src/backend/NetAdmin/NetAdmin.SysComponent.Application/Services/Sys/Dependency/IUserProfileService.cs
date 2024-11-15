using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     用户档案服务
/// </summary>
public interface IUserProfileService : IService, IUserProfileModule
{
    /// <summary>
    ///     编辑用户档案
    /// </summary>
    Task<int> EditAsync(EditUserProfileReq req);

    /// <summary>
    ///     获取当前用户配置
    /// </summary>
    Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync();

    /// <summary>
    ///     设置当前用户配置
    /// </summary>
    Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req);
}