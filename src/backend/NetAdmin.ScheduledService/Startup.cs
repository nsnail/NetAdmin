using NetAdmin.Host.Extensions;
using NetAdmin.Host.Middlewares;
using NetAdmin.ScheduledService.Extensions;
using Prometheus;

namespace NetAdmin.ScheduledService;

/// <summary>
///     启动类
/// </summary>
public sealed class Startup : Host.Startup
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
            .UseRealIp()                             // 使用RealIp中间件，用于获取真实客户端IP地址
            .EnableBuffering()                       // 启用请求体缓冲，允许多次读取请求体
            .UseMiddleware<RequestAuditMiddleware>() // 使用RequestAuditMiddleware中间件，执行请求审计
            #if DEBUG
            .UseOpenApiSkin() // 使用OpenApiSkin中间件（仅在调试模式下），提供Swagger UI皮肤
            #endif
            .UseInject(string.Empty)                   // 使用Inject中间件，Furion脚手架的依赖注入支持
            .UseUnifyResultStatusCodes()               // 使用UnifyResultStatusCodes中间件，用于统一处理结果状态码
            .UseCorsAccessor()                         // 使用CorsAccessor中间件，启用跨域资源共享（CORS）支持
            .UseRouting()                              // 使用Routing中间件，配置路由映射
            .UseHttpMetrics()                          // 使用HttpMetrics中间件，启用HTTP性能监控
            .UseMiddleware<RemoveNullNodeMiddleware>() // 使用RemoveNullNodeMiddleware中间件，删除JSON中的空节点
            .UseEndpoints();                           // 配置端点以处理请求
    }

    /// <summary>
    ///     配置服务容器
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services               //
            .AddConsoleFormatter() // 添加控制台日志格式化器
            .AddAllOptions()       // 添加所有的配置选项
            .AddSnowflake()        // 添加雪花算法生成器
            .AddEventBus()         // 添加事件总线
            .AddFreeSql()          // 添加FreeSql数据库访问
            .AddCorsAccessor()     // 添加跨域资源共享（CORS）访问支持
            .AddRedisCache()       // 添加 Redis 缓存支持
            .AddContextUser()      // 添加上下文用户支持
            .AddSchedules()        // 添加计划任务
            .AddRemoteRequest()    // 添加远程请求支持

            // IMvcBuilder
            .AddControllers()             // 添加控制器
            .AddJsonSerializer()          // 添加JSON序列化器
            .AddDefaultApiResultHandler() // 添加默认的API结果处理程序
            ;
    }
}