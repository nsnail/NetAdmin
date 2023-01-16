using System.ComponentModel;
using Furion.FriendlyException;
using NetAdmin.Infrastructure.Attributes;
using NetAdmin.Infrastructure.Lang;
using NSExt.Attributes;

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     基础枚举常量
/// </summary>
public static class Enums
{
    /// <summary>
    ///     错误码
    /// </summary>
    [ErrorCodeType]
    [Export]
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
    ///     日志等级
    /// </summary>
    public enum LogLevels
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

    /// <summary>
    ///     模块类型
    /// </summary>
    [Export]
    public enum ModuleTypes
    {
        /// <summary>
        ///     系统模块
        /// </summary>
        [Description(nameof(Str.System_module))]
        [Localization(typeof(Str))]
        Sys

       ,

        /// <summary>
        ///     业务模块
        /// </summary>
        [Description(nameof(Str.Business_module))]
        [Localization(typeof(Str))]
        Biz
    }

    /// <summary>
    ///     排序方式
    /// </summary>
    [Export]
    public enum Orders
    {
        /// <summary>
        ///     顺序
        /// </summary>
        [Description(nameof(Str.Sort_in_order))]
        [Localization(typeof(Str))]
        Ascending

       ,

        /// <summary>
        ///     倒序
        /// </summary>
        [Description(nameof(Str.Sort_in_reverse_order))]
        [Localization(typeof(Str))]
        Descending
    }
}