/*
                  _ooOoo_
                 o8888888o
                 88" . "88
                 (| -_- |)
                 O\  =  /O
              ____/`---'\____
            .'  \\|     |//  `.
           /  \\|||  :  |||//  \
          /  _||||| -:- |||||-  \
          |   | \\\  -  /// |   |
          | \_|  ''\---/''  |   |
          \  .-\__  `-`  ___/-. /
        ___`. .'  /--.--\  `. . __
     ."" '<  `.___\_<|>_/___.'  >'"".
    | | :  `- \`.;`\ _ /`;.`/ - ` : | |
    \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======
                  `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
           佛祖保佑       永无BUG
*/

using NetAdmin.Host.Extensions;
using NetAdmin.Host.Middlewares;
using NetAdmin.SysComponent.Host.Extensions;
using NetAdmin.SysComponent.Host.Middlewares;
using Spectre.Console.Cli;
using YourSolution.AdmServer.Host;
using YourSolution.AdmServer.Host.Extensions;
using ValidationResult = Spectre.Console.ValidationResult;

NetAdmin.Host.Startup.Entry<Startup>(args);

namespace YourSolution.AdmServer.Host
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
        #pragma warning disable S2325
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifeTime)
            #pragma warning restore S2325
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _ = app                                        //
                .UseMiddleware<SafetyShopHostMiddleware>() // 安全停机中间件
                .EnableBuffering()                         // 启用请求体缓冲，允许多次读取请求体
                .UseMiddleware<RequestAuditMiddleware>()   // 使用RequestAuditMiddleware中间件，执行请求审计
                #if DEBUG
                .UseApiSkin() // 使用OpenApiSkin中间件（仅在调试模式下），提供Swagger UI皮肤
                #else
                .UseVueAdmin()    // 托管管理后台，仅在非调试模式下
                .UsePrometheus()  // 使用Prometheus中间件，启用HTTP性能监控
                #endif
                .UseInject(string.Empty)                   // 使用Inject中间件，Gurion脚手架的依赖注入支持
                .UseUnifyResultStatusCodes()               // 使用UnifyResultStatusCodes中间件，用于统一处理结果状态码
                .UseCorsAccessor()                         // 使用CorsAccessor中间件，启用跨域资源共享（CORS）支持
                .UseRouting()                              // 使用Routing中间件，配置路由映射
                .UseAuthentication()                       // 使用Authentication中间件，启用身份验证
                .UseAuthorization()                        // 使用Authorization中间件，启用授权
                .UseMiddleware<RemoveNullNodeMiddleware>() // 使用RemoveNullNodeMiddleware中间件，删除JSON中的空节点
                .UseWebSockets()                           // 使用WebSockets中间件，启用WebSocket支持
                .UseMiddleware<VersionCheckerMiddleware>() // 使用VersionUpdaterMiddleware中间件，用于检查版本
                .UseEndpoints();                           // 配置端点以处理请求
            _ = lifeTime.ApplicationStopping.Register(SafetyShopHostMiddleware.OnStopping);
        }

        /// <summary>
        ///     配置服务容器
        /// </summary>
        #pragma warning disable S2325
        public void ConfigureServices(IServiceCollection services)
            #pragma warning restore S2325
        {
            _ = services.AddConsoleFormatter() // 添加控制台日志模板
                        .AddAllOptions()       // 添加配置项
                        .AddJwt()              // 添加 Jwt 授权处理器
                        .AddSnowflake()        // 添加雪花编号生成器
                        .AddEventBus()         // 添加事件总线
                        .AddFreeSqlWithArgs()  // 添加 freeSql
                        .AddRemoteRequest()    // 添加远程请求
                        .AddCorsAccessor()     // 添加支持跨域访问
                        .AddContextUserToken() // 添加上下文用户令牌
                        .AddContextUserInfo()  // 添加上下文用户信息
                        .AddRedisCache()       // 添加 Redis 缓存
                        .AddSchedules()        // 添加计划任务
                        .AddTronScanClient()   // 添加 TronScan 客户端

                        // IMvcBuilder
                        .AddControllers()             // 添加控制器
                        .AddJsonSerializer(true)      // 添加JSON序列化器，并设置显示枚举名而非数字枚举值
                        .AddDefaultApiResultHandler() // 添加默认的API结果处理程序
                ;
        }

        /// <inheritdoc />
        public Task<int> ExecuteAsync(CommandContext context, CommandSettings settings)
        {
            return ExecuteAsync(context, (settings as CommandLineArgs)!);
        }

        /// <inheritdoc />
        public async Task<int> ExecuteAsync(CommandContext context, CommandLineArgs settings)
        {
            Args = settings;
            var webOpt = new WebApplicationOptions //
                         {
                             EnvironmentName = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT").NullOrEmpty(null)
                           , Args            = context.Remaining.Raw.ToArray()
                         };
            Serve.BuildApplication(RunOptions.Default.ConfigureOptions(webOpt), null, out var startUrl, out var app);
            await app.RunAsync(startUrl).ConfigureAwait(false);
            return 0;
        }

        /// <inheritdoc />
        public ValidationResult Validate(CommandContext context, CommandSettings settings)
        {
            return ValidationResult.Success();
        }
    }
}