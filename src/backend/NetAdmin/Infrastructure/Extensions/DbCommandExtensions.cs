// ReSharper disable TemplateIsNotCompileTimeConstantProblem

using System.Data.Common;
using Furion;
using Furion.DependencyInjection;
using NSExt.Extensions;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     DbCommand 扩展方法
/// </summary>
[SuppressSniffer]
public static class DbCommandExtensions
{
    /// <summary>
    ///     sql语句执行后回调
    /// </summary>
    /// <param name="log">执行SQL命令，返回的结果</param>
    public static void Executed(string log)
    {
        var executeTime = log[..log.IndexOf('\n').Is(-1, log.Length)];

        // 输出sql日志到日志管道
        App.GetService<ILogger<DbCommand>>()?.LogDebug(executeTime);
    }

    /// <summary>
    ///     sql语句执行前回调
    /// </summary>
    public static void Executing(this DbCommand me)
    {
        var sqlText = me.ParameterFormat();

        // 输出sql日志到miniProfiler
        // 输出sql日志到日志管道
        App.GetService<ILogger<DbCommand>>()?.LogDebug(sqlText.RemoveWrapped());
    }
}