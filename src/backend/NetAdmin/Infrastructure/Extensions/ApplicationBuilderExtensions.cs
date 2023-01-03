using NetAdmin.Aop.Middlewares;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     ApplicationBuilder对象 扩展方法
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     使用 api skin （knife4j-vue）
    /// </summary>
    public static IApplicationBuilder UseSwaggerSkin(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SwaggerSkinMiddleware>();
    }
}