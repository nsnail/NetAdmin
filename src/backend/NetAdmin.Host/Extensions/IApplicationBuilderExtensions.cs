using Furion.DependencyInjection;
using Furion.SpecificationDocument;
using IGeekFan.AspNetCore.Knife4jUI;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     ApplicationBuilder对象 扩展方法
/// </summary>
[SuppressSniffer]

// ReSharper disable once InconsistentNaming
public static class IApplicationBuilderExtensions
{
    /// <summary>
    ///     使用 api skin （knife4j-vue）
    /// </summary>
    public static IApplicationBuilder UseOpenApiSkin(this IApplicationBuilder me)
    {
        return me.UseKnife4UI(options => {
            options.RoutePrefix = string.Empty; // 配置 Knife4UI 路由地址
            foreach (var groupInfo in SpecificationDocumentBuilder.GetOpenApiGroups()) {
                options.SwaggerEndpoint("/" + groupInfo.RouteTemplate, groupInfo.Title);
            }
        });
    }
}