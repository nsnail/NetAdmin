using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Tpl.Example;

namespace NetAdmin.Application.Modules.Tpl;

/// <summary>
///     示例模块
/// </summary>
public interface IExampleModule : ICrudModule<CreateExampleReq, QueryExampleRsp // 创建类型
  , EditExampleReq                                                              // 编辑类型
  , QueryExampleReq, QueryExampleRsp                                            // 查询类型
  , DelReq                                                                      // 删除类型
>;