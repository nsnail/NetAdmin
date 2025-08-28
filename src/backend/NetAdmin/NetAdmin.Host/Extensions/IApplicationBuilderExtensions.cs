#if DEBUG
using NetAdmin.ApiSkin;

#else
using Prometheus;
using Prometheus.HttpMetrics;
#endif

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
    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder me) {
        return me.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
                #if !DEBUG
                _ = endpoints.MapMetrics();
                #endif
            }
        );
    }
    #if DEBUG
    /// <summary>
    ///     使用 api skin （knife4j-vue）
    /// </summary>
    public static IApplicationBuilder UseApiSkin(this IApplicationBuilder me) {
        return me.UseApiSkin(options =>
            {
                options.RoutePrefix = string.Empty; // 配置 Knife4UI 路由地址
                foreach (var groupInfo in SpecificationDocumentBuilder.GetOpenApiGroups()) {
                    options.SwaggerEndpoint(groupInfo.RouteTemplate, groupInfo.Title);
                }
            }
        );
    }
    #else
    /// <summary>
    ///     使用 Prometheus
    /// </summary>
    public static IApplicationBuilder UsePrometheus(this IApplicationBuilder me) {
        return me.UseHttpMetrics(opt =>
            {
                opt.RequestDuration.Histogram = Metrics.CreateHistogram(
                    "http_request_duration_seconds", "The duration of HTTP requests processed by an ASP.NET Core application."
                    , HttpRequestLabelNames.All, new HistogramConfiguration { Buckets = Histogram.PowersOfTenDividedBuckets(-2, 2, 4) }
                );
            }
        );
    }
    #endif
}