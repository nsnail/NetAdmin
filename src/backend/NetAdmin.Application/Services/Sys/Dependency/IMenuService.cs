using NetAdmin.Application.Modules.Sys;

namespace NetAdmin.Application.Services.Sys.Dependency;

/// <summary>
///     菜单服务
/// </summary>
public interface IMenuService : IService, IMenuModule
{
    /// <summary>
    ///     用户菜单
    /// </summary>
    ValueTask<List<QueryMenuRsp>> UserMenus(QueryUserRsp userInfo);
}