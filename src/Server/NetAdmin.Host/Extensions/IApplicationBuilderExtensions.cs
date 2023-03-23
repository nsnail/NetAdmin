using IGeekFan.AspNetCore.Knife4jUI;
using NetAdmin.Host.Middlewares;

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
        _ = me.UseKnife4UI(options => {
            options.RoutePrefix = string.Empty; // 配置 Knife4UI 路由地址
            foreach (var groupInfo in SpecificationDocumentBuilder.GetOpenApiGroups()) {
                options.SwaggerEndpoint($"/{groupInfo.RouteTemplate}", groupInfo.Title);
            }
        });

        return me;
    }

    /// <summary>
    ///     使用 删除json空节点
    /// </summary>
    public static IApplicationBuilder UseRemoveNullNode(this IApplicationBuilder me)
    {
        _ = me.UseMiddleware<RemoveNullNodeMiddleware>();
        return me;
    }

    /// <summary>
    ///     使用 请求审计中间件
    /// </summary>
    public static IApplicationBuilder UseRequestAudit(this IApplicationBuilder me)
    {
        return me.UseMiddleware<RequestAuditMiddleware>();
    }
}