using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.Dic.Catalog;

namespace NetAdmin.Application.Modules.Sys.Dic;

/// <summary>
///     字典目录模块
/// </summary>
public interface IDicCatalogModule : ICrudModule<CreateDicCatalogReq, QueryDicCatalogRsp // 创建类型
  , QueryDicCatalogReq, QueryDicCatalogRsp                                               // 查询类型
  , UpdateDicCatalogReq, QueryDicCatalogRsp                                              // 修改类型
  , DelReq                                                                               // 删除类型
> { }