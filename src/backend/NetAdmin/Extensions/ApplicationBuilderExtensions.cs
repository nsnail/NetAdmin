using Furion.DependencyInjection;
using Furion.SpecificationDocument;
using IGeekFan.AspNetCore.Knife4jUI;

namespace NetAdmin.Extensions;

/// <summary>
///     ApplicationBuilder对象 扩展方法
/// </summary>
[SuppressSniffer]
public static class ApplicationBuilderExtensions
{
    /// <summary>
    ///     使用 api skin （knife4j-vue）
    /// </summary>
    public static IApplicationBuilder UseSwaggerSkin(this IApplicationBuilder app)
    {
        return app.UseKnife4UI(options => {
            options.RoutePrefix = string.Empty; // 配置 Knife4UI 路由地址
            foreach (var groupInfo in SpecificationDocumentBuilder.GetOpenApiGroups()) {
                options.SwaggerEndpoint("/" + groupInfo.RouteTemplate, groupInfo.Title);
            }
        });
    }
}