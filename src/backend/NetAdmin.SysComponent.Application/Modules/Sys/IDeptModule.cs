using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     部门模块
/// </summary>
public interface IDeptModule : ICrudModule<CreateDeptReq, QueryDeptRsp // 创建类型
  , QueryDeptReq, QueryDeptRsp                                         // 查询类型
  , DelReq                                                             // 删除类型
>
{
    /// <summary>
    ///     编辑部门
    /// </summary>
    Task<QueryDeptRsp> EditAsync(EditDeptReq req);

    /// <summary>
    ///     启用/禁用部门
    /// </summary>
    Task<int> SetEnabledAsync(SetDeptEnabledReq req);
}