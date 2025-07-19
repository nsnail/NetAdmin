namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     用户角-色映射服务
/// </summary>
public interface IUserRoleService : IService, IUserRoleModule
{
    /// <summary>
    ///     通过用户id删除
    /// </summary>
    Task<int> BulkDeleteByUserIdAsync(long userId);
}