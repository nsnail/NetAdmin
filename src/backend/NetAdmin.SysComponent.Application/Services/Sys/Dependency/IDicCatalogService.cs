using NetAdmin.Domain.Dto.Sys.Dic.Catalog;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     字典目录服务
/// </summary>
public interface IDicCatalogService : IService, IDicCatalogModule
{
    /// <summary>
    ///     编辑字典目录
    /// </summary>
    Task<int> EditAsync(EditDicCatalogReq req);
}