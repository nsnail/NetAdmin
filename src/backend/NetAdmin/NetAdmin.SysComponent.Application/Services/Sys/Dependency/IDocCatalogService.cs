using NetAdmin.Domain.Dto.Sys.Doc.Catalog;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     文档分类服务
/// </summary>
public interface IDocCatalogService : IService, IDocCatalogModule
{
    /// <summary>
    ///     编辑文档分类
    /// </summary>
    Task<int> EditAsync(EditDocCatalogReq req);
}