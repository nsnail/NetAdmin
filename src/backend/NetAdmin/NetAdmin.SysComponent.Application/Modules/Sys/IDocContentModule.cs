using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     文档内容模块
/// </summary>
public interface IDocContentModule : ICrudModule<CreateDocContentReq, QueryDocContentRsp // 创建类型
    , EditDocContentReq // 编辑类型
    , QueryDocContentReq, QueryDocContentRsp // 查询类型
    , DelReq // 删除类型
>
{
    /// <summary>
    ///     启用/禁用文档内容
    /// </summary>
    Task<int> SetEnabledAsync(SetDocContentEnabledReq req);
}