using Spectre.Console;

namespace NetAdmin.Host;

/// <summary>
///     启动类
/// </summary>
public abstract class Startup : AppStartup
{
    /// <summary>
    ///     打印Banner
    /// </summary>
    protected static void ShowBanner()
    {
        AnsiConsole.WriteLine();
        var gridInfo = new Grid().AddColumn(new GridColumn().NoWrap().PadRight(10))
                                 .AddColumn(new GridColumn().NoWrap())
                                 .Expand();
        foreach (var kv in ApplicationHelper.GetEnvironmentInfo()) {
            _ = gridInfo.AddRow(kv.Key, kv.Value.ToString()!.EscapeMarkup());
        }

        var gridWrap      = new Grid().AddColumn();
        var entryAssembly = Assembly.GetEntryAssembly();
        var assemblyName  = entryAssembly!.GetName();
        foreach (var str in assemblyName.Name!.Split('.')) {
            _ = gridWrap.AddRow(new FigletText(str).Color(Color.Green));
        }

        _ = gridWrap.AddRow(gridInfo);
        AnsiConsole.Write(new Panel(gridWrap).Header(Global.ProductVersion).Expand());
        AnsiConsole.WriteLine();
    }
}