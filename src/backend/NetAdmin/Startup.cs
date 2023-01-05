using Furion;
using NetAdmin.Aop.Filters;
using NetAdmin.Infrastructure.Extensions;
using Spectre.Console;

namespace NetAdmin;

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
        // 打印banner
        AnsiConsole.Write(new FigletText(nameof(NetAdmin)).LeftJustified().Color(Color.Green));
        AnsiConsole.WriteLine();

        // 启动主机
        Serve.Run(RunOptions.Default.WithArgs(args));
    }

    /// <summary>
    ///     配置应用程序中间件
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app
            #if DEBUG
            .UseDeveloperExceptionPage() //                                                  开发者异常信息页
            .UseSwaggerSkin()            //                                                  Swagger皮肤中间件
            #else
            .UseHttpsRedirection() //                                                        强制https
            #endif

            .UseAuthentication()                                   //                        认证中间件
            .UseAuthorization()                                    //                        授权中间件
            .UseInject(string.Empty)                               //             Furion基础中间件
            .UseUnifyResultStatusCodes()                           //                        状态码中间件
            .UseCorsAccessor()                                     //                        跨域访问中间件
            .UseRouting()                                          //                        控制器路由映射
            .UseEndpoints(endpoints => endpoints.MapControllers()) //    端点映射
            ;
    }

    /// <summary>
    ///     配置服务容器
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConsoleFormatter()                           //    控制台日志模板
                .AddJwt<JwtHandler>(enableGlobalAuthorize: true) //    Jwt 授权处理器
                .Services
                #if DEBUG
                .AddMonitorLogging() //                                日志监视信息
                #endif
                .AddMvcFilter<RequestAuditFilter>()           //       请求审计日志
                .AddSnowflake()                               //       雪花id生成器
                .AddEventBus()                                //       事件总线
                .AddFreeSql()                                 //       注册freeSql
                .AddAllOptions()                              //       注册配置项
                .AddCorsAccessor()                            //       支持跨域访问
                .AddControllers()                             //       注册控制器
                .AddInjectWithUnifyResult<ApiResultHandler>() //       api响应结果模板
                .AddJsonSerializer();                         //       json序列化配置
    }
}