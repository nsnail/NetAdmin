using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.HttpOverrides;
using Prometheus;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     ApplicationBuilder对象 扩展方法
/// </summary>
[SuppressSniffer]

// ReSharper disable once InconsistentNaming
public static class IApplicationBuilderExtensions
{
    /// <summary>
    ///     执行匹配的端点
    /// </summary>
    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder me)
    {
        _ = me.UseEndpoints(endpoints => {
            _ = endpoints.MapControllers();
            _ = endpoints.MapMetrics();
        });
        return me;
    }

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
    ///     获取客户端真实Ip
    /// </summary>
    public static IApplicationBuilder UseRealIp(this IApplicationBuilder me)
    {
        _ = me.UseForwardedHeaders(new ForwardedHeadersOptions //
                                   {
                                       ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                                          ForwardedHeaders.XForwardedProto
                                   });
        return me;
    }
}