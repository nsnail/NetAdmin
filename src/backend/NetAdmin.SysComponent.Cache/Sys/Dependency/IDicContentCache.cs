using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     字典内容缓存
/// </summary>
public interface IDicContentCache : ICache<IDistributedCache, IDicContentService>, IDicContentModule
{
    /// <summary>
    ///     通过分类键查询字典内容
    /// </summary>
    Task<List<QueryDicContentRsp>> QueryByCatalogCodeAsync(string catalogCode);
}