using NetAdmin.Domain.Dto.Sys.CodeTemplate;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     代码模板模块
/// </summary>
public interface ICodeTemplateModule : ICrudModule<CreateCodeTemplateReq, QueryCodeTemplateRsp // 创建类型
  , EditCodeTemplateReq                                                                        // 编辑类型
  , QueryCodeTemplateReq, QueryCodeTemplateRsp                                                 // 查询类型
  , DelReq                                                                                     // 删除类型
>
{
    /// <summary>
    ///     设置代码模板启用状态
    /// </summary>
    Task<int> SetEnabledAsync(SetCodeTemplateEnabledReq req);
}