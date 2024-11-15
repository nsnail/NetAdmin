using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     字典内容服务
/// </summary>
public interface IDicContentService : IService, IDicContentModule
{
    /// <summary>
    ///     编辑字典内容
    /// </summary>
    Task<QueryDicContentRsp> EditAsync(EditDicContentReq req);

    /// <summary>
    ///     通过分类键查询字典内容
    /// </summary>
    Task<List<QueryDicContentRsp>> QueryByCatalogCodeAsync(string catalogCode);
}