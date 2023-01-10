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
    ///     错误码
    /// </summary>
    [ErrorCodeType]
    public enum ErrorCodes
    {
        /// <summary>
        ///     未知错误
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Str.Unknown_error))]
        [Localization(typeof(Str))]
        Unknown = 4000

       ,

        /// <summary>
        ///     无效输入
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Str.Invalid_input))]
        [Localization(typeof(Str))]
        InvalidInput = 4010

       ,

        /// <summary>
        ///     无效操作
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Str.Invalid_operation))]
        [Localization(typeof(Str))]
        InvalidOperation = 4020

       ,

        /// <summary>
        ///     未登录
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Str.Not_logged_in))]
        [Localization(typeof(Str))]
        IdentityMissing = 4030

       ,

        /// <summary>
        ///     权限不足
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Str.Insufficient_permissions))]
        [Localization(typeof(Str))]
        NoPermissions = 4031

       ,

        /// <summary>
        ///     人机验证
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Str.Man_machine_verification))]
        [Localization(typeof(Str))]
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
        [Description(nameof(Str.Menu))]
        [Localization(typeof(Str))]
        Menu = 1

       ,

        /// <summary>
        ///     链接
        /// </summary>
        [Description(nameof(Str.Link))]
        [Localization(typeof(Str))]
        Link = 2

       ,

        /// <summary>
        ///     框架
        /// </summary>
        [Description(nameof(Str.Iframe))]
        [Localization(typeof(Str))]
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
        [Description(nameof(Str.Enabled))]
        [Localization(typeof(Str))]
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
        [Description(nameof(Str.Enabled))]
        [Localization(typeof(Str))]
        Enabled = 0b_0000_0001

       ,

        /// <summary>
        ///     隐藏
        /// </summary>
        [Description(nameof(Str.Hidden))]
        [Localization(typeof(Str))]
        Hidden = 0b_0000_0010

       ,

        /// <summary>
        ///     隐藏面包屑
        /// </summary>
        [Description(nameof(Str.Hidden_bread_crumb))]
        [Localization(typeof(Str))]
        HiddenBreadCrumb = 0b_0000_0100

       ,

        /// <summary>
        ///     整页路由
        /// </summary>
        [Description(nameof(Str.Full_page_routing))]
        [Localization(typeof(Str))]
        FullPageRouting = 0b_0000_1000
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
        [Description(nameof(Str.Enabled))]
        [Localization(typeof(Str))]
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
        [Description(nameof(Str.Enabled))]
        [Localization(typeof(Str))]
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
        [Description("[gray]TCE[/]")]
        Trace

       ,

        /// <summary>
        ///     Debug
        /// </summary>
        [Description("[gray]DBG[/]")]
        Debug

       ,

        /// <summary>
        ///     Information
        /// </summary>
        [Description("[green]INF[/]")]
        Information

       ,

        /// <summary>
        ///     Warning
        /// </summary>
        [Description("[yellow]WRN[/]")]
        Warning

       ,

        /// <summary>
        ///     Error
        /// </summary>
        [Description("[red]ERR[/]")]
        Error

       ,

        /// <summary>
        ///     Critical
        /// </summary>
        [Description("[red]CTL[/]")]
        Critical

       ,

        /// <summary>
        ///     None
        /// </summary>
        [Description("[gray]NON[/]")]
        None
    }
}