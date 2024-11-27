using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     文档内容服务
/// </summary>
public interface IDocContentService : IService, IDocContentModule
{
    /// <summary>
    ///     编辑文档内容
    /// </summary>
    Task<QueryDocContentRsp> EditAsync(EditDocContentReq req);

    /// <summary>
    ///     浏览文档内容
    /// </summary>
    Task<QueryDocContentRsp> ViewAsync(QueryDocContentReq req);
}