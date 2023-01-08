using System.ComponentModel;
using Furion.FriendlyException;

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     枚举常量（public类型会通过接口暴露给前端）
/// </summary>
public static class Enums
{
    /// <summary>
    ///     错误码（0 表示 成功）
    /// </summary>
    [ErrorCodeType]
    public enum ErrorCodes
    {
        /// <summary>
        ///     未知错误
        /// </summary>
        [ErrorCodeItemMetadata("系统异常")] [Description(Strings.MSG_ERROR_UNKNOWN)]
        Unknown = 4000

       ,

        /// <summary>
        ///     无效输入
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(Strings.MSG_ERROR_INVALID_INPUT)]
        InvalidInput = 4010

       ,

        /// <summary>
        ///     无效操作
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(Strings.MSG_INVALID_OPERATION)]
        InvalidOperation = 4020

       ,

        /// <summary>
        ///     未登录
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(Strings.MSG_IDENTITY_MISSING)]
        IdentityMissing = 4030

       ,

        /// <summary>
        ///     权限不足
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(Strings.MSG_NO_PERMISSIONS)]
        NoPermissions = 4031

       ,

        /// <summary>
        ///     人机验证
        /// </summary>
        [ErrorCodeItemMetadata("需进行人机验证")] [Description(Strings.MSG_HUMAN_VERIFICATION)]
        HumanVerification = 4040
    }

    /// <summary>
    ///     菜单类型
    /// </summary>
    public enum MenuTypes
    {
        /// <summary>
        ///     菜单
        /// </summary>
        [Description("菜单")] Menu = 1

       ,

        /// <summary>
        ///     链接
        /// </summary>
        [Description("链接")] Link = 2

       ,

        /// <summary>
        ///     框架
        /// </summary>
        [Description("框架")] Iframe = 3
    }

    /// <summary>
    ///     排序方式
    /// </summary>
    public enum Orders
    {
        /// <summary>
        ///     顺序
        /// </summary>
        Ascending

       ,

        /// <summary>
        ///     倒序
        /// </summary>
        Descending
    }

    /// <summary>
    ///     部门表比特位
    /// </summary>
    [Flags]
    public enum SysDeptBits : long
    {
        /// <summary>
        ///     启用
        /// </summary>
        Enabled = 0b_0000_0001
    }

    /// <summary>
    ///     菜单表比特位
    /// </summary>
    [Flags]
    public enum SysMenuBits : long
    {
        /// <summary>
        ///     启用
        /// </summary>
        Enabled = 0b_0000_0001
    }

    /// <summary>
    ///     角色表比特位
    /// </summary>
    [Flags]
    public enum SysRoleBits : long
    {
        /// <summary>
        ///     启用
        /// </summary>
        Enabled = 0b_0000_0001

       ,

        /// <summary>
        ///     忽略权限控制（拥有所有权限）
        /// </summary>
        IgnorePermissionControl = 0b_0000_0010
    }

    /// <summary>
    ///     用户表比特位
    /// </summary>
    [Flags]
    public enum SysUserBits : long
    {
        /// <summary>
        ///     启用
        /// </summary>
        Enabled = 0b_0000_0001
    }

    /// <summary>
    ///     日志等级
    /// </summary>
    internal enum LogLevels
    {
        /// <summary>
        ///     Trace
        /// </summary>
        [Description("[gray]TCE[/]")] Trace

       ,

        /// <summary>
        ///     Debug
        /// </summary>
        [Description("[gray]DBG[/]")] Debug

       ,

        /// <summary>
        ///     Information
        /// </summary>
        [Description("[green]INF[/]")] Information

       ,

        /// <summary>
        ///     Warning
        /// </summary>
        [Description("[yellow]WRN[/]")] Warning

       ,

        /// <summary>
        ///     Error
        /// </summary>
        [Description("[red]ERR[/]")] Error

       ,

        /// <summary>
        ///     Critical
        /// </summary>
        [Description("[red]CTL[/]")] Critical

       ,

        /// <summary>
        ///     None
        /// </summary>
        [Description("[gray]NON[/]")] None
    }
}