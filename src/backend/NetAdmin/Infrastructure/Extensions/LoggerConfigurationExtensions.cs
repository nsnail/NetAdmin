// ReSharper disable UnusedMember.Global

using System.Globalization;
using System.Text;
using NetAdmin.Infrastructure.Constant;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     Serilog配置类扩展
/// </summary>
public static class LoggerConfigurationExtensions
{
    /// <summary>
    ///     以日志来源为文件名配置文件日志
    /// </summary>
    public static void FileLogSaveBySource(this LoggerConfiguration config, string sourceName)
    {
        config.WriteTo.Logger(subConfig => {
            subConfig.Filter.ByIncludingOnly(Matching.FromSource(sourceName));
            subConfig.WriteTo.File($"logs/{sourceName}/.log", outputTemplate: Strings.TEMP_LOG_OUPUT
                                 , rollingInterval: RollingInterval.Day, encoding: Encoding.UTF8
                                 , formatProvider: CultureInfo.InvariantCulture);
        });
    }

    /// <summary>
    ///     以日志类型为文件名配置文件日志
    /// </summary>
    public static void FileLogSaveByType<T>(this LoggerConfiguration config)
    {
        FileLogSaveBySource(config, typeof(T).FullName);
    }

    /// <summary>
    ///     初始化Serilog日志组件配置
    /// </summary>
    public static LoggerConfiguration Init(this LoggerConfiguration me)
    {
        //  //日志按日保存，这样会在文件名称后自动加上日期后缀
        //  , rollingInterval: RollingInterval.Day
        //  //,rollOnFileSizeLimit: false          // 限制单个文件的最大长度
        //  //,retainedFileCountLimit: 10,         // 最大保存文件数,等于null时永远保留文件。
        //  //,fileSizeLimitBytes: 10 * 1024,      // 最大单个文件大小
        //   , encoding: Encoding.UTF8 // 文件字符编码

        //写入控制台
        me.WriteTo.Console( //
            outputTemplate: Strings.TEMP_LOG_OUPUT
          , theme: new SystemConsoleTheme(new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> {
                                              [ConsoleThemeStyle.Text] = new() { Foreground = ConsoleColor.White }
                                            , [ConsoleThemeStyle.SecondaryText]
                                                  = new() { Foreground = ConsoleColor.Gray }
                                            , [ConsoleThemeStyle.TertiaryText]
                                                  = new() { Foreground = ConsoleColor.DarkGray }
                                            , [ConsoleThemeStyle.Invalid]
                                                  = new() { Foreground = ConsoleColor.Yellow }
                                            , [ConsoleThemeStyle.Null]   = new() { Foreground = ConsoleColor.Blue }
                                            , [ConsoleThemeStyle.Name]   = new() { Foreground = ConsoleColor.Gray }
                                            , [ConsoleThemeStyle.String] = new() { Foreground = ConsoleColor.Cyan }
                                            , [ConsoleThemeStyle.Number]
                                                  = new() { Foreground = ConsoleColor.Magenta }
                                            , [ConsoleThemeStyle.Boolean] = new() { Foreground = ConsoleColor.Blue }
                                            , [ConsoleThemeStyle.Scalar]  = new() { Foreground = ConsoleColor.Green }
                                            , [ConsoleThemeStyle.LevelVerbose]
                                                  = new() { Foreground = ConsoleColor.Gray }
                                            , [ConsoleThemeStyle.LevelDebug]
                                                  = new() { Foreground = ConsoleColor.White }
                                            , [ConsoleThemeStyle.LevelInformation]
                                                  = new() { Foreground = ConsoleColor.Green }
                                            , [ConsoleThemeStyle.LevelWarning]
                                                  = new() { Foreground = ConsoleColor.Yellow }
                                            , [ConsoleThemeStyle.LevelError]
                                                  = new() {
                                                              Foreground = ConsoleColor.White
                                                            , Background = ConsoleColor.Red
                                                          }
                                            , [ConsoleThemeStyle.LevelFatal]
                                                  = new() {
                                                              Foreground = ConsoleColor.White
                                                            , Background = ConsoleColor.Red
                                                          }
                                          }), formatProvider: CultureInfo.InvariantCulture);
        Console.OutputEncoding = Encoding.UTF8;

        //写入文件
        // me.WriteTo.File("logs/.log",
        //     outputTemplate: Const.LOG_OUTPUT_TEMPLATE,
        //     rollingInterval: RollingInterval.Day,
        //     encoding: Encoding.UTF8);

        // LogEventLevel.Warning 类型级别以上 写入文件
        me.WriteTo.File(Path.Combine(AppContext.BaseDirectory, ".logs/.log"), outputTemplate: Strings.TEMP_LOG_OUPUT
                      , rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Warning
                      , encoding: Encoding.UTF8, formatProvider: CultureInfo.InvariantCulture);

        // LogEventLevel.Error 类型级别以上保存到数据库
        // me.WriteTo.Logger(subConfig => {
        //                       subConfig.Filter.ByIncludingOnly(x => x.Level >= LogEventLevel.Error);
        //                       subConfig.WriteTo.MSSqlServer(
        //                                                         App.GetOptions<ConnectionsOptions>()
        //                                                            .SerilogConnection
        //
        //                                                    ,
        //                                                     new MSSqlServerSinkOptions {
        //                                                         TableName          = "LogTasks",
        //                                                         AutoCreateSqlTable = true
        //                                                     });
        //                   });
        return me;
    }
}