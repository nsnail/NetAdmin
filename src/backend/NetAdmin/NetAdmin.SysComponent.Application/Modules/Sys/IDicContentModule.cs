using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     字典内容模块
/// </summary>
public interface IDicContentModule : ICrudModule<CreateDicContentReq, QueryDicContentRsp // 创建类型
    , EditDicContentReq // 编辑类型
    , QueryDicContentReq, QueryDicContentRsp // 查询类型
    , DelReq // 删除类型
>
{
    /// <summary>
    ///     启用/禁用字典内容
    /// </summary>
    Task<int> SetEnabledAsync(SetDicContentEnabledReq req);
}