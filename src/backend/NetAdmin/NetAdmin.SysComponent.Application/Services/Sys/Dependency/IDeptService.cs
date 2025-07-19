namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     部门服务
/// </summary>
public interface IDeptService : IService, IDeptModule
{
    /// <summary>
    ///     获取所有子部门编号
    /// </summary>
    Task<IEnumerable<long>> GetChildDeptIdsAsync(long deptId);
}