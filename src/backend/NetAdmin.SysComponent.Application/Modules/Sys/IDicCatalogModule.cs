using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     字典目录模块
/// </summary>
public interface IDicCatalogModule : ICrudModule<CreateDicCatalogReq, QueryDicCatalogRsp // 创建类型
  , QueryDicCatalogReq, QueryDicCatalogRsp                                               // 查询类型
  , UpdateDicCatalogReq, QueryDicCatalogRsp                                              // 修改类型
  , DelReq                                                                               // 删除类型
>;