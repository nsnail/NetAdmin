using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Contexts;

/// <summary>
///     上下文用户信息
/// </summary>
public sealed record ContextUserInfo : QueryUserRsp
{
    /// <summary>
    ///     从HttpContext 创建上下文用户信息
    /// </summary>
    public static ContextUserInfo Create()
    {
        var ret = App.HttpContext?.Items[nameof(Chars.FLG_CONTEXT_USER_INFO)] as QueryUserRsp;
        return ret?.Adapt<ContextUserInfo>();
    }

    /// <summary>
    ///     是否存在于 HttpContext
    /// </summary>
    public static bool HasInContext()
    {
        return App.HttpContext?.Items.ContainsKey(Chars.FLG_CONTEXT_USER_INFO) ?? false;
    }
}