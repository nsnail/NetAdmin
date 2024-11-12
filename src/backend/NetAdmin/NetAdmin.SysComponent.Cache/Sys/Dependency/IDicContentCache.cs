using NetAdmin.SysComponent.Domain.Dto.Sys.Dic.Content;

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