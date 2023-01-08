using System.ComponentModel;
using Furion.FriendlyException;
using NetAdmin.Lang;
using NSExt.Attributes;

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
        [ErrorCodeItemMetadata("{0}")] [Description(nameof(Str.UNKNOWN_ERROR))] [Localization(typeof(Str))]
        Unknown = 4000

       ,

        /// <summary>
        ///     无效输入
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(nameof(Str.INVALID_INPUT))] [Localization(typeof(Str))]
        InvalidInput = 4010

       ,

        /// <summary>
        ///     无效操作
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(nameof(Str.INVALID_OPERATION))] [Localization(typeof(Str))]
        InvalidOperation = 4020

       ,

        /// <summary>
        ///     未登录
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(nameof(Str.NOT_LOGGED_IN))] [Localization(typeof(Str))]
        IdentityMissing = 4030

       ,

        /// <summary>
        ///     权限不足
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(nameof(Str.INSUFFICIENT_PERMISSIONS))] [Localization(typeof(Str))]
        NoPermissions = 4031

       ,

        /// <summary>
        ///     人机验证
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(nameof(Str.MAN_MACHINE_VERIFICATION))] [Localization(typeof(Str))]
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
        [Description(nameof(Str.MENU))] [Localization(typeof(Str))]
        Menu = 1

       ,

        /// <summary>
        ///     链接
        /// </summary>
        [Description(nameof(Str.LINK))] [Localization(typeof(Str))]
        Link = 2

       ,

        /// <summary>
        ///     框架
        /// </summary>
        [Description(nameof(Str.IFRAME))] [Localization(typeof(Str))]
        Iframe = 3
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