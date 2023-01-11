using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NetAdmin.Infrastructure.Utils;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IToolsApi" />
public class ToolsApi : ApiBase<IToolsApi>, IToolsApi
{
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
    private readonly XmlCommentHelper                    _xmlCommentHelper;

    /// <inheritdoc cref="IToolsApi" />
    public ToolsApi( //
        IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, XmlCommentHelper xmlCommentHelper)
    {
        _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        _xmlCommentHelper                   = xmlCommentHelper;
    }

    /// <inheritdoc />
    public dynamic EnvironmentInfo()
    {
        return EnvironmentInfoInternal();
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <summary>
    ///     Test
    /// </summary>
    [AllowAnonymous]
    public dynamic Test()
    {
        foreach (var item in _actionDescriptorCollectionProvider.ActionDescriptors.Items.GroupBy(
                     x => x.GetType().GetProperty(nameof(ControllerActionDescriptor.ControllerTypeInfo))!
                           .GetValue(x) as Type)) {
            var f = item;
        }

        var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                                                            Summary
                                                                = _xmlCommentHelper.GetComments(
                                                                    x.GetType().GetProperty(nameof(MethodInfo))!
                                                                     .GetValue(x) as MemberInfo)
                                                          , Action     = x.RouteValues["Action"]
                                                          , Controller = x.RouteValues["Controller"]
                                                          , x.AttributeRouteInfo!.Name
                                                          , Method = x.ActionConstraints
                                                                      ?.OfType<HttpMethodActionConstraint>()
                                                                      .FirstOrDefault()
                                                                      ?.HttpMethods.First()
                                                          , x.AttributeRouteInfo.Template
                                                        })
                                                        .ToList();
        return routes;
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public string Version()
    {
        return VersionInternal();
    }

    internal static Dictionary<string, object> EnvironmentInfoInternal()
    {
        return typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                  .Where(x => x.Name != nameof(Environment.StackTrace))
                                  .ToDictionary(x => x.Name, x => x.GetValue(null));
    }

    internal static string VersionInternal()
    {
        return typeof(ToolsApi).Assembly.GetName().Version!.ToString();
    }
}