using Spectre.Console;
using Spectre.Console.Cli;

namespace NetAdmin.Host;

/// <summary>
///     启动类
/// </summary>
public abstract class Startup : AppStartup
{
    /// <summary>
    ///     程序入口
    /// </summary>
    public static void Entry<T>(IEnumerable<string> args, Action<IConfigurator> commandConfig = null)
        where T : class, ICommand
    {
        ShowBanner();
        var app = new CommandApp<T>();
        if (commandConfig != null) {
            app.Configure(commandConfig);
        }

        _ = app.Run(args);
    }

    /// <summary>
    ///     打印Banner
    /// </summary>
    private static void ShowBanner()
    {
        AnsiConsole.WriteLine();
        var gridInfo = new Grid().AddColumn(new GridColumn().NoWrap().Width(50).PadRight(10))
                                 .AddColumn(new GridColumn().NoWrap())
                                 .Expand();
        foreach (var kv in ApplicationHelper.GetEnvironmentInfo().OrderBy(x => x.Key)) {
            _ = gridInfo.AddRow(kv.Key, kv.Value.ToString()!.EscapeMarkup());
        }

        var gridWrap      = new Grid().AddColumn();
        var entryAssembly = Assembly.GetEntryAssembly();
        var assemblyName  = entryAssembly!.GetName();
        _ = gridWrap.AddRow(new FigletText(assemblyName.Name!).Color(Color.Green));

        _ = gridWrap.AddRow(gridInfo);
        AnsiConsole.Write(new Panel(gridWrap).Header(GlobalStatic.ProductVersion).Expand());
        AnsiConsole.WriteLine();
    }
}