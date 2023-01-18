using NetAdmin.Application.Modules.Sys;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Application.Services.Sys.Dependency;

/// <summary>
///     菜单服务
/// </summary>
public interface IMenuService : IService, IMenuModule
{
    /// <summary>
    ///     用户菜单
    /// </summary>
    Task<IEnumerable<QueryMenuRsp>> UserMenus(QueryUserRsp userInfo);
}