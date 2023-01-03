using System.Text.Json;
using System.Text.Json.Serialization;
using Furion;
using NetAdmin.Aop.Filters;
using NetAdmin.Infrastructure.Extensions;
using NSExt.Extensions;
using Serilog;

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
        Serve.Run(RunOptions.Default.WithArgs(args)
                            .ConfigureBuilder(builder => builder.UseSerilogDefault(config => config.Init())));
    }

    /// <summary>
    ///     配置应用程序中间件
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app
            #if DEBUG
            .UseDeveloperExceptionPage() //                       开发者异常信息页
            #else
           .UseHttpsRedirection() //                              强制https
            #endif

            .UseAuthentication()                                   //                        认证授权
            .UseAuthorization()                                    //                        认证授权
            .UseInject(string.Empty)                               //             Furion基础中间件
            .UseCorsAccessor()                                     //                        跨域访问中间件
            .UseSerilogRequestLogging()                            //                        使用Serilog接管HTTP请求日志
            .UseRouting()                                          //                        控制器路由映射
            .UseSwaggerSkin()                                      //                        新版swagger ui（knife4j）中间件
            .UseEndpoints(endpoints => endpoints.MapControllers()) //            配置端点
            ;
    }

    /// <summary>
    ///     配置服务容器
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddJwt<JwtHandler>(enableGlobalAuthorize: true) //    Jwt 授权处理器
                .Services
                #if DEBUG
                .AddMonitorLogging() //                                打印日志监视信息，便于调试
                #endif
                .AddMvcFilter<RequestAuditActionFilter>()     //       请求审计日志
                .AddMvcFilter<RequestAuditResultFilter>()     //       请求审计日志
                .AddSnowflake()                               //       雪花id生成器
                .AddEventBus()                                //       事件总线
                .AddFreeSql()                                 //       注册freeSql
                .AddAllOptions()                              //       注册配置项
                .AddCorsAccessor()                            //       支持跨域访问
                .AddRemoteRequest()                           //       远程请求
                .AddControllers()                             //       注册控制器
                .AddInjectWithUnifyResult<ApiResultHandler>() //       api响应结果模板
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.CopyFrom(default(JsonSerializerOptions).NewJsonSerializerOptions());
                    options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss");
                    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                });
    }
}