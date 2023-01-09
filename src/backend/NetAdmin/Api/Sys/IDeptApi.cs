using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Dept;

namespace NetAdmin.Api.Sys;

/// <summary>
///     部门接口
/// </summary>
public interface IDeptApi : ICrudApi<CreateDeptReq, QueryDeptRsp // 创建类型
                              , QueryDeptReq, QueryDeptRsp       // 查询类型
                              , UpdateDeptReq, QueryDeptRsp      // 修改类型
                              , DelReq                           // 删除类型
                            >, IRestfulApi { }