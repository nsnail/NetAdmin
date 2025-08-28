using NetAdmin.Domain.Dto.Sys.Doc.Catalog;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     文档分类模块
/// </summary>
public interface IDocCatalogModule : ICrudModule<CreateDocCatalogReq, QueryDocCatalogRsp // 创建类型
    , EditDocCatalogReq // 编辑类型
    , QueryDocCatalogReq, QueryDocCatalogRsp // 查询类型
    , DelReq // 删除类型
>;