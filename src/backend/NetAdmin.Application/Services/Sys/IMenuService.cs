using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     菜单服务
/// </summary>
public interface IMenuService : ICrudService<CreateMenuReq, QueryMenuRsp // 创建类型
  , QueryMenuReq, QueryMenuRsp                                           // 查询类型
  , UpdateMenuReq, QueryMenuRsp                                          // 修改类型
  , DelReq                                                               // 删除类型
>
{
    /// <summary>
    ///     批量删除菜单
    /// </summary>
    ValueTask<int> BulkDelete(BulkDelReq req);

    /// <summary>
    ///     当前用户菜单
    /// </summary>
    ValueTask<List<QueryMenuRsp>> UserMenus();
}