using Furion;
using NetAdmin.Application.Services.Sys;
using NetAdmin.Host.Aop.Filters;
using NetAdmin.Host.Aop.Middlewares;
using NetAdmin.Host.Extensions;
using Spectre.Console;

namespace NetAdmin.Host;

/// <summary>
///     启动类
/// </summary>
public class Startup : AppStartup
{
    /// <summary>
    ///     程序入口
    /// </summary>
    public static void Main(string[] args)
    {
        ShowBanner();

        // 启动主机
        Serve.Run(RunOptions.Default.WithArgs(args));
    }

    /// <summary>
    ///     配置应用程序中间件
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app //
            .EnableBuffering() // /                                                             启用 Body 重复读功能
            .UseMiddleware<RequestAuditMiddleware>() //                                         请求审计
            #if DEBUG
            .UseOpenApiSkin() // /                                                              Swagger皮肤中间件
            #else
            .UseHttpsRedirection() //                                                           强制https
            #endif
            .UseInject(string.Empty) // /                                           Furion基础中间件
            .UseUnifyResultStatusCodes() //                                                     状态码中间件
            .UseCorsAccessor() //                                                               跨域访问中间件
            .UseRouting() //                                                                    控制器路由映射
            .UseAuthentication() // /                                                           认证中间件
            .UseAuthorization() //                                                              授权中间件
            .UseMiddleware<RemoveNullPropertyMiddleware>() //                                   删除json null属性
            .UseEndpoints(endpoints => endpoints.MapControllers()); // /    端点映射
    }

    /// <summary>
    ///     配置服务容器
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter()                           // /                                      控制台日志模板
                .AddAllOptions()                                 // /                                      注册配置项
                .AddJwt<JwtHandler>(enableGlobalAuthorize: true) //                                        Jwt 授权处理器
                .Services.AddSnowflake()                         // /                                      雪花id生成器
                .AddEventBus()                                   //                                        事件总线
                .AddFreeSql()                                    //                                        注册freeSql
                .AddCorsAccessor()                               //                                        支持跨域访问
                .AddContextUser()                                //                                        注册上下文用户
                .AddMemCache()                                   //                                        注册内存缓存

                // IMvcBuilder
                .AddControllers()    //                                      注册控制器
                .AddJsonSerializer() //                                      json序列化配置
                .AddFurion()         //                                      注册Furion
            ;
    }

    private static void ShowBanner()
    {
        AnsiConsole.WriteLine();
        var gridInfo = new Grid().AddColumn(new GridColumn().NoWrap().PadRight(10))
                                 .AddColumn(new GridColumn().NoWrap())
                                 .Expand();
        foreach (var kv in ToolsService.EnvironmentInfoStatic()) {
            gridInfo.AddRow(kv.Key, kv.Value.ToString()!.EscapeMarkup());
        }

        var gridWrap = new Grid().AddColumn()
                                 .AddRow(new FigletText(nameof(NetAdmin)).Color(Color.Green))
                                 .AddRow(gridInfo);
        AnsiConsole.Write(new Panel(gridWrap).Header(ToolsService.VersionStatic()).Expand());
        AnsiConsole.WriteLine();
    }
}