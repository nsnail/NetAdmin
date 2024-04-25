using NetAdmin.AdmServer.Host;
using NetAdmin.AdmServer.Host.Extensions;
using NetAdmin.Host.Extensions;
using NetAdmin.Host.Middlewares;
using NetAdmin.SysComponent.Host.Extensions;
using Spectre.Console.Cli;
using ValidationResult = Spectre.Console.ValidationResult;
#if !DEBUG
using Prometheus;
#endif

NetAdmin.Host.Startup.Entry<Startup>(args);

namespace NetAdmin.AdmServer.Host
{
    /// <summary>
    ///     启动类
    /// </summary>
    public sealed class Startup : NetAdmin.Host.Startup, ICommand<CommandLineArgs>
    {
        /// <summary>
        ///     命令行参数
        /// </summary>
        public static CommandLineArgs Args { get; private set; }

        /// <summary>
        ///     配置应用程序中间件
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifeTime)
        {
            _ = app                                        //
                .UseMiddleware<SafetyShopHostMiddleware>() // 安全停机中间件
                .EnableBuffering()                         // 启用请求体缓冲，允许多次读取请求体
                .UseMiddleware<RequestAuditMiddleware>()   // 使用RequestAuditMiddleware中间件，执行请求审计
                #if DEBUG
                .UseOpenApiSkin() // 使用OpenApiSkin中间件（仅在调试模式下），提供Swagger UI皮肤
                #else
                .UseVueAdmin()    // 托管管理后台，仅在非调试模式下
                .UseHttpMetrics() // 使用HttpMetrics中间件，启用HTTP性能监控
                #endif
                .UseInject(string.Empty)                   // 使用Inject中间件，Furion脚手架的依赖注入支持
                .UseUnifyResultStatusCodes()               // 使用UnifyResultStatusCodes中间件，用于统一处理结果状态码
                .UseCorsAccessor()                         // 使用CorsAccessor中间件，启用跨域资源共享（CORS）支持
                .UseRouting()                              // 使用Routing中间件，配置路由映射
                .UseAuthentication()                       // 使用Authentication中间件，启用身份验证
                .UseAuthorization()                        // 使用Authorization中间件，启用授权
                .UseMiddleware<RemoveNullNodeMiddleware>() // 使用RemoveNullNodeMiddleware中间件，删除JSON中的空节点
                .UseEndpoints();                           // 配置端点以处理请求
            _ = lifeTime.ApplicationStopping.Register(SafetyShopHostMiddleware.OnStopping);
        }

        /// <summary>
        ///     配置服务容器
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddConsoleFormatter() // 添加控制台日志模板
                        .AddAllOptions()       // 添加配置项
                        .AddJwt()              // 添加 Jwt 授权处理器
                        .AddSnowflake()        // 添加雪花编号生成器
                        .AddEventBus()         // 添加事件总线
                        .AddFreeSqlWithArgs()  // 添加 freeSql
                        .AddRemoteRequest()    // 添加远程请求
                        .AddCorsAccessor()     // 添加支持跨域访问
                        .AddContextUser()      // 添加上下文用户
                        .AddRedisCache()       // 添加 Redis 缓存
                        .AddSchedules()        // 添加计划任务

                        // IMvcBuilder
                        .AddControllers()             // 添加控制器
                        .AddJsonSerializer(true)      // 添加JSON序列化器，并设置显示枚举名而非数字枚举值
                        .AddDefaultApiResultHandler() // 添加默认的API结果处理程序
                ;
        }

        /// <inheritdoc />
        #pragma warning disable ASA001
        public Task<int> Execute(CommandContext context, CommandLineArgs settings)
            #pragma warning restore ASA001
        {
            Args = settings;
            _    = Serve.Run(RunOptions.Default.WithArgs(context.Remaining.Raw.ToArray()));
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        #pragma warning disable ASA001
        public Task<int> Execute(CommandContext context, CommandSettings settings)
            #pragma warning restore ASA001
        {
            return Execute(context, (settings as CommandLineArgs)!);
        }

        /// <inheritdoc />
        public ValidationResult Validate(CommandContext context, CommandSettings settings)
        {
            return ValidationResult.Success();
        }
    }
}