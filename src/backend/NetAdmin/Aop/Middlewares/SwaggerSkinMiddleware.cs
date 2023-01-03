using System.Text;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.Aop.Middlewares;

/// <summary>
///     Api 界面 knife4j-vue 中间件
/// </summary>
public class SwaggerSkinMiddleware
{
    private const string _EMBEDDED_FILE_NAMESPACE = $"{nameof(NetAdmin)}..data.swagger_ui";

    private const string _INDEX_FILE_NAME = "index.html";

    private readonly IWebHostEnvironment  _env;
    private readonly ILoggerFactory       _logger;
    private readonly RequestDelegate      _next;
    private readonly SwaggerSkinOptions   _options;
    private readonly StaticFileMiddleware _staticFileMiddleware;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SwaggerSkinMiddleware" /> class.
    ///     Api 界面 knife4j-vue 中间件
    /// </summary>
    /// <param name="next">下一个中间件</param>
    /// <param name="env">主机环境</param>
    /// <param name="logger">日志工厂</param>
    /// <param name="options">api skin 配置项</param>
    public SwaggerSkinMiddleware(RequestDelegate              next, IWebHostEnvironment env, ILoggerFactory logger
                               , IOptions<SwaggerSkinOptions> options)
    {
        _next                 = next;
        _env                  = env;
        _logger               = logger;
        _options              = options.Value;
        _staticFileMiddleware = CreateStaticFileMiddleware();
    }

    /// <summary>
    ///     中间件主处理器
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value!;

        switch (context.Request.Method) {
            // 重定向到首页
            case Strings.FLAG_HTTP_METHOD_GET when Regexes.RegexSlash().IsMatch(path): {
                await RespondWithIndexHtml(context.Response);
                return;
            }

            // 响应首页
            case Strings.FLAG_HTTP_METHOD_GET when Regexes.RegexIndexHtml().IsMatch(path):
                await RespondWithIndexHtml(context.Response);
                return;

            // 前端特殊用途
            case Strings.FLAG_HTTP_METHOD_GET when Regexes.RegexSwaggerResources().IsMatch(path):
                // 前端特殊用途： knife4j -vue / Knife4jAsync.js line 74： 此处判断底层springfox版本
                // 1、springfox提供的分组地址   /swagger -resources
                // 2、springdoc                   -open提供的分组地址：v3 /api -docs /swagger -config
                // swagger请求api地址
                // if(this.springdoc){
                //     this.url = options.url || 'v3/api-docs/swagger-config'
                // }else{
                //     this.url = options.url || 'swagger-resources'
                // }
                await context.Response.WriteAsync(_options.Urls.Json());
                return;

            // 响应其他资源
            default:
                await _staticFileMiddleware.Invoke(context);
                break;
        }
    }

    /// <summary>
    ///     创建静态文件处理中间件
    /// </summary>
    private StaticFileMiddleware CreateStaticFileMiddleware()
    {
        var staticFileOptions = new StaticFileOptions {
                                                          RequestPath = string.Empty
                                                        , FileProvider
                                                              = new EmbeddedFileProvider(
                                                                  GetType().Assembly, _EMBEDDED_FILE_NAMESPACE)
                                                      };

        return new StaticFileMiddleware(_next, _env, Options.Create(staticFileOptions), _logger);
    }

    /// <summary>
    ///     读取资源文件（首页），替换字典，输出到 HttpResponse
    /// </summary>
    private async Task RespondWithIndexHtml(HttpResponse response)
    {
        response.StatusCode  = 200;
        response.ContentType = "text/html;charset=utf-8";

        await using var stream
            = GetType().Assembly.GetManifestResourceStream($"{_EMBEDDED_FILE_NAMESPACE}.{_INDEX_FILE_NAME}");

        var htmlBuilder = new StringBuilder(await new StreamReader(stream!).ReadToEndAsync());
        await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
    }
}