using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Dept;

namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     部门服务
/// </summary>
public interface IDeptService : ICrudService<CreateDeptReq, QueryDeptRsp // 创建类型
                                  , QueryDeptReq, QueryDeptRsp           // 查询类型
                                  , UpdateDeptReq, QueryDeptRsp          // 修改类型
                                  , DelReq                               // 删除类型
                                >, IService { }