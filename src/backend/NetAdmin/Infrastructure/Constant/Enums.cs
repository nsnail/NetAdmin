using System.ComponentModel;
using Furion.FriendlyException;

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
        All = 1

       ,

        /// <summary>
        ///     本部门和下级部门
        /// </summary>
        DeptWithChild = 2

       ,

        /// <summary>
        ///     本部门
        /// </summary>
        Dept = 3

       ,

        /// <summary>
        ///     本人数据
        /// </summary>
        Self = 4

       ,

        /// <summary>
        ///     指定部门
        /// </summary>
        Custom = 5
    }

    /// <summary>
    ///     错误码（0 表示 成功）
    /// </summary>
    [ErrorCodeType]
    public enum ErrorCodes
    {
        /// <summary>
        ///     未知错误
        /// </summary>
        [ErrorCodeItemMetadata("{0}")] [Description(Strings.MSG_ERROR_UNKNOWN)]
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
    ///     FreeSql全局过滤器
    /// </summary>
    public enum FreeSqlGlobalFilters
    {
        /// <summary>
        ///     数据权限
        /// </summary>
        [Description("数据权限")] Delete

       ,

        /// <summary>
        ///     删除
        /// </summary>
        [Description("删除")] Self

       ,

        /// <summary>
        ///     本人权限
        /// </summary>
        [Description("本人权限")] Tenant

       ,

        /// <summary>
        ///     租户
        /// </summary>
        [Description("租户")] Data
    }

    /// <summary>
    ///     日志等级
    /// </summary>
    public enum LogLevels
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
    ///     操作者
    /// </summary>
    public enum Operators
    {
        /// <summary>
        ///     用户
        /// </summary>
        User = 1

       ,

        /// <summary>
        ///     管理员
        /// </summary>
        Administrator = 2

       ,

        /// <summary>
        ///     服务程序
        /// </summary>
        Service = 3
    }

    /// <summary>
    ///     权限类型
    /// </summary>
    public enum PermissionTypes
    {
        /// <summary>
        ///     分组
        /// </summary>
        [Description("分组")] Group = 1

       ,

        /// <summary>
        ///     菜单
        /// </summary>
        [Description("菜单")] Menu = 2

       ,

        /// <summary>
        ///     权限点
        /// </summary>
        [Description("权限点")] Dot = 3
    }

    /// <summary>
    ///     短信验证码类型
    /// </summary>
    public enum SmsCodeTypes
    {
        /// <summary>
        ///     注册账号
        /// </summary>
        CreateUser = 1

       ,

        /// <summary>
        ///     登录
        /// </summary>
        Login = 2
    }

    /// <summary>
    ///     Sql命令类型
    /// </summary>
    public enum SqlCommandTypes
    {
        /// <summary>
        ///     Select
        /// </summary>
        Select

       ,

        /// <summary>
        ///     Insert
        /// </summary>
        Insert

       ,

        /// <summary>
        ///     Update
        /// </summary>
        Update

       ,

        /// <summary>
        ///     Delete
        /// </summary>
        Delete
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
        Enabled = 0b0000_0000_0000_0000_0000_0000_0000_0001
    }

    /// <summary>
    ///     用户类型
    /// </summary>
    public enum UserTypes
    {
        /// <summary>
        ///     默认用户
        /// </summary>
        DefaultUser = 1

       ,

        /// <summary>
        ///     租户管理员
        /// </summary>
        TenantAdmin = 10

       ,

        /// <summary>
        ///     平台管理员
        /// </summary>
        PlatformAdmin = 100
    }
}