using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;

namespace NetAdmin.Api.Sys;

/// <summary>
///     菜单接口
/// </summary>
public interface IMenuApi : ICrudApi<CreateMenuReq // 创建类型
                              , MenuInfo, MenuInfo // 查询类型
                              , NopReq             // 修改类型
                              , NopReq             // 删除类型
                            >, IRestfulApi { }