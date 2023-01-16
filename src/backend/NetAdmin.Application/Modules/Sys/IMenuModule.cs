using NetAdmin.DataContract.Dto.Sys.Menu;

namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     菜单模块
/// </summary>
public interface IMenuModule : ICrudModule<CreateMenuReq, QueryMenuRsp // 创建类型
  , QueryMenuReq, QueryMenuRsp                                         // 查询类型
  , UpdateMenuReq, QueryMenuRsp                                        // 修改类型
  , DelReq                                                             // 删除类型
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