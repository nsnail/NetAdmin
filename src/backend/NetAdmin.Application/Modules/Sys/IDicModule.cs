namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     字典模块
/// </summary>
public interface IDicModule : ICrudModule<CreateDicReq, QueryDicRsp // 创建类型
  , QueryDicReq, QueryDicRsp                                        // 查询类型
  , UpdateDicReq, QueryDicRsp                                       // 修改类型
  , DelReq                                                          // 删除类型
> { }