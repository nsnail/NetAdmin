using NetAdmin.Application.Modules.Sys;
using NetAdmin.DataContract.Dto.Sys.Menu;
using NetAdmin.DataContract.Dto.Sys.User;

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