using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Tpl.Example;

namespace NetAdmin.Application.Modules.Tpl;

/// <summary>
///     示例模块
/// </summary>
public interface IExampleModule : ICrudModule<CreateExampleReq, QueryExampleRsp // 创建类型
  , QueryExampleReq, QueryExampleRsp                                            // 查询类型
  , UpdateExampleReq, QueryExampleRsp                                           // 修改类型
  , DelReq                                                                      // 删除类型
> { }