using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;

namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     菜单服务
/// </summary>
public interface IMenuService : ICrudService<CreateMenuReq, QueryMenuRsp // 创建类型
                                  , QueryMenuReq, QueryMenuRsp           // 查询类型
                                  , UpdateMenuReq, QueryMenuRsp          // 修改类型
                                  , DelReq                               // 删除类型
                                >, IService
{
    /// <summary>
    ///     批量删除菜单
    /// </summary>
    Task<int> BulkDelete(BulkDelReq req);
}