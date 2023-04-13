using NetAdmin.BizServer.Host.Extensions;
using NetAdmin.Host.Extensions;
using NetAdmin.Host.Middlewares;
using Prometheus;

namespace NetAdmin.BizServer.Host;

/// <summary>
///     启动类
/// </summary>
public sealed class Startup : NetAdmin.Host.Startup
{
    /// <summary>
    ///     程序入口
    /// </summary>
    public static void Main(string[] args)
    {
        ShowBanner();
        _ = Serve.Run(RunOptions.Default.WithArgs(args));
    }

    /// <summary>
    ///     配置应用程序中间件
    /// </summary>
    public void Configure(IApplicationBuilder app)
    {
        _ = app                                      //
            .UseRealIP()                             // 获取真实IP
            .EnableBuffering()                       // 启用 Body 重读
            .UseMiddleware<RequestAuditMiddleware>() // 请求审计
            #if DEBUG
            .UseOpenApiSkin() // Swagger皮肤
            #endif
            .UseInject(string.Empty) // /                                            Furion脚手架
            .UseUnifyResultStatusCodes() //                                                     状态码拦截
            .UseCorsAccessor() //                                                               跨域访问
            .UseRouting() //                                                                    路由映射
            .UseHttpMetrics() //                                    性能监控
            .UseAuthentication() // /                                                           认证
            .UseAuthorization() //                                                              授权
            .UseMiddleware<RemoveNullNodeMiddleware>() //                                       删除json空节点
            .UseEndpoints(); //                                                                 执行匹配的端点
    }

    /// <summary>
    ///     配置服务容器
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddConsoleFormatter() // 控制台日志模板
                    .AddAllOptions()       // 注册配置项
                    .AddJwt()              // Jwt 授权处理器
                    .AddSnowflake()        // 雪花id生成器
                    .AddEventBus()         // 事件总线
                    .AddFreeSql()          // freeSql
                    .AddCorsAccessor()     // 支持跨域访问
                    .AddContextUser()      // 上下文用户
                    .AddRedisCache()       // Redis缓存

                    // IMvcBuilder
                    .AddControllers()             // 注册控制器
                    .AddJsonSerializer(true)      // json序列化配置
                    .AddDefaultApiResultHandler() // Api结果处理器
            ;
    }
}