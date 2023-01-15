using System.ComponentModel;
using Furion.FriendlyException;
using NetAdmin.Infrastructure.Lang;
using NSExt.Attributes;

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     枚举常量（public类型会通过接口暴露给前端）
/// </summary>
public static class Enums
{
    /// <summary>
    ///     数据范围
    /// </summary>
    public enum DataScopes
    {
        /// <summary>
        ///     全部
        /// </summary>
        [Description(nameof(Str.All))]
        [Localization(typeof(Str))]
        All = 1

       ,

        /// <summary>
        ///     本部门和下级部门
        /// </summary>
        [Description(nameof(Str.This_department_and_subordinate_departments))]
        [Localization(typeof(Str))]
        DeptWithChild = 2

       ,

        /// <summary>
        ///     本部门
        /// </summary>
        [Description(nameof(Str.Department_data))]
        [Localization(typeof(Str))]
        Dept = 3

       ,

        /// <summary>
        ///     本人数据
        /// </summary>
        [Description(nameof(Str.Personal_data))]
        [Localization(typeof(Str))]
        Self = 4

       ,

        /// <summary>
        ///     指定部门
        /// </summary>
        [Description(nameof(Str.Designated_department))]
        [Localization(typeof(Str))]
        SpecificDept = 5
    }

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
    public enum ModuleTypes
    {
        /// <summary>
        ///     系统模块
        /// </summary>
        Sys

       ,

        /// <summary>
        ///     业务模块
        /// </summary>
        Biz
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
}