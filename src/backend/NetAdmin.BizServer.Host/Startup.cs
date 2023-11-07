using NetAdmin.BizServer.Host.Extensions;
using NetAdmin.Host.Extensions;
using NetAdmin.Host.Middlewares;
#if !DEBUG
using Prometheus;
#endif

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
        #pragma warning disable ASP0001
        _ = app                                      //
            .UseRealIp()                             // 使用RealIp中间件，用于获取真实客户端IP地址
            .EnableBuffering()                       // 启用请求体缓冲，允许多次读取请求体
            .UseMiddleware<RequestAuditMiddleware>() // 使用RequestAuditMiddleware中间件，执行请求审计
            #if DEBUG
            .UseOpenApiSkin() // 使用OpenApiSkin中间件（仅在调试模式下），提供Swagger UI皮肤
            #endif
            .UseInject(string.Empty)     // 使用Inject中间件，Furion脚手架的依赖注入支持
            .UseUnifyResultStatusCodes() // 使用UnifyResultStatusCodes中间件，用于统一处理结果状态码
            .UseCorsAccessor()           // 使用CorsAccessor中间件，启用跨域资源共享（CORS）支持
            #if !DEBUG
            .UseVueAdmin() // 托管管理后台，仅在非调试模式下
            #endif
            .UseRouting()                              // 使用Routing中间件，配置路由映射
            #if !DEBUG
            .UseHttpMetrics()                          // 使用HttpMetrics中间件，启用HTTP性能监控
            #endif
            .UseAuthentication()                       // 使用Authentication中间件，启用身份验证
            .UseAuthorization()                        // 使用Authorization中间件，启用授权
            .UseMiddleware<RemoveNullNodeMiddleware>() // 使用RemoveNullNodeMiddleware中间件，删除JSON中的空节点
            .UseEndpoints();                           // 配置端点以处理请求

        #pragma warning restore ASP0001
    }

    /// <summary>
    ///     配置服务容器
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddConsoleFormatter() // 控制台日志模板
                    .AddAllOptions()       // 注册配置项
                    .AddJwt()              // Jwt 授权处理器
                    .AddSnowflake()        // 雪花编号生成器
                    .AddEventBus()         // 事件总线
                    .AddFreeSql()          // freeSql
                    .AddRemoteRequest()    // 注册远程请求
                    .AddCorsAccessor()     // 支持跨域访问
                    .AddContextUser()      // 上下文用户
                    .AddRedisCache()       // Redis缓存

                    // IMvcBuilder
                    .AddControllers()             // 添加控制器
                    .AddJsonSerializer(true)      // 添加JSON序列化器，并设置显示枚举名而非数字枚举值
                    .AddDefaultApiResultHandler() // 添加默认的API结果处理程序
            ;
    }
}