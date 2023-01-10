using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;

namespace NetAdmin.Api.Sys;

/// <summary>
///     菜单接口
/// </summary>
public interface IMenuApi : ICrudApi<CreateMenuReq, QueryMenuRsp // 创建类型
                              , QueryMenuReq, QueryMenuRsp       // 查询类型
                              , UpdateMenuReq, QueryMenuRsp      // 修改类型
                              , DelReq                           // 删除类型
                            >, IRestfulApi
{
    /// <summary>
    ///     批量删除菜单
    /// </summary>
    Task<int> BulkDelete(BulkDelReq req);
}