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
    public enum RspCodes
    {
        /// <summary>
        ///     成功
        /// </summary>
        [Description(nameof(Ln.Succeed))]
        [Localization(typeof(Ln))]
        Succeed = 0

       ,

        /// <summary>
        ///     未处理的异常
        /// </summary>
        /// <remarks>
        ///     通过全局异常过滤器捕获的问题
        /// </remarks>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Unexpected))]
        [Localization(typeof(Ln))]
        Unexpected = 9000

       ,

        /// <summary>
        ///     无效输入
        /// </summary>
        /// <remarks>
        ///     请求参数格式不对、校验错误等问题
        /// </remarks>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Invalid_input))]
        [Localization(typeof(Ln))]
        InvalidInput = 9100

       ,

        /// <summary>
        ///     无效操作
        /// </summary>
        /// <remarks>
        ///     不允许的操作，事务失败等问题
        /// </remarks>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Invalid_operation))]
        [Localization(typeof(Ln))]
        InvalidOperation = 9200
    }
}