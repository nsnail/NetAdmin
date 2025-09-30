namespace NetAdmin.SysComponent.Application.Services.Sys.Dependency;

/// <summary>
///     用户邀请服务
/// </summary>
public interface IUserInviteService : IService, IUserInviteModule
{
    /// <summary>
    ///     获取关联用户Id
    /// </summary>
    Task<List<long>> GetAssociatedUserIdAsync(
        long userId
        , bool up = true
    );
}