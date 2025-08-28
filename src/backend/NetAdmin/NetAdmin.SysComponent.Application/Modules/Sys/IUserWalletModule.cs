using NetAdmin.Domain.Dto.Sys.UserWallet;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     用户钱包模块
/// </summary>
public interface IUserWalletModule : ICrudModule<CreateUserWalletReq, QueryUserWalletRsp // 创建类型
    , EditUserWalletReq // 编辑类型
    , QueryUserWalletReq, QueryUserWalletRsp // 查询类型
    , DelReq // 删除类型
>;