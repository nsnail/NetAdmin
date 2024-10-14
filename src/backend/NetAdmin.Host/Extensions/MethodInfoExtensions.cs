namespace NetAdmin.Host.Extensions;

/// <summary>
///     Type 扩展方法
/// </summary>
public static class MethodInfoExtensions
{
    /// <summary>
    ///     获取路由路径
    /// </summary>
    public static string GetRoutePath(this MethodInfo me, IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IActionDescriptorCollectionProvider>()
                              .ActionDescriptors.Items.FirstOrDefault(x => x.DisplayName!.StartsWith( //
                                                                          $"{me.DeclaringType}.{me.Name}", StringComparison.Ordinal))
                              ?.AttributeRouteInfo?.Template;
    }
}