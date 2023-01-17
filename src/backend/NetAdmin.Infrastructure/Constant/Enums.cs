using System.ComponentModel;
using Furion.FriendlyException;
using NetAdmin.Infrastructure.Attributes;
using NSExt.Attributes;

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     基础枚举常量
/// </summary>
/// <remarks>
///     添加[Export]特性暴露给前端
/// </remarks>
public static class Enums
{
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
        [Description(nameof(Ln.System_module))]
        [Localization(typeof(Ln))]
        Sys

       ,

        /// <summary>
        ///     业务模块
        /// </summary>
        [Description(nameof(Ln.Business_module))]
        [Localization(typeof(Ln))]
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
        [Description(nameof(Ln.Sort_in_order))]
        [Localization(typeof(Ln))]
        Ascending

       ,

        /// <summary>
        ///     倒序
        /// </summary>
        [Description(nameof(Ln.Sort_in_reverse_order))]
        [Localization(typeof(Ln))]
        Descending
    }

    /// <summary>
    ///     状态码
    /// </summary>
    [ErrorCodeType]
    [Export]
    public enum StatusCodes
    {
        /// <summary>
        ///     成功
        /// </summary>
        [Description(nameof(Ln.Succeed))]
        [Localization(typeof(Ln))]
        Succeed = 0

       ,

        /// <summary>
        ///     意外错误
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Unexpected))]
        [Localization(typeof(Ln))]
        Unexpected = 4000

       ,

        /// <summary>
        ///     无效输入
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Invalid_input))]
        [Localization(typeof(Ln))]
        InvalidInput = 4010

       ,

        /// <summary>
        ///     无效操作
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Invalid_operation))]
        [Localization(typeof(Ln))]
        InvalidOperation = 4020

       ,

        /// <summary>
        ///     未登录
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Not_logged_in))]
        [Localization(typeof(Ln))]
        IdentityMissing = 4030

       ,

        /// <summary>
        ///     权限不足
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Insufficient_permissions))]
        [Localization(typeof(Ln))]
        NoPermissions = 4031

       ,

        /// <summary>
        ///     人机验证
        /// </summary>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Man_machine_verification))]
        [Localization(typeof(Ln))]
        HumanVerification = 4040
    }
}