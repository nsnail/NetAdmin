using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Modules.Sys;

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
}