using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Modules.Sys;

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
}