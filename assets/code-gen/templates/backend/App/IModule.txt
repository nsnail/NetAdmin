namespace {0}.Application.Modules.{1};

/// <summary>
///     {3}模块
/// </summary>
public interface I{2}Module : ICrudModule<Create{2}Req, Query{2}Rsp // 创建类型
  , Query{2}Req, Query{2}Rsp                                        // 查询类型
  , Update{2}Req, Query{2}Rsp                                       // 修改类型
  , DelReq                                                          // 删除类型
> {{ }}