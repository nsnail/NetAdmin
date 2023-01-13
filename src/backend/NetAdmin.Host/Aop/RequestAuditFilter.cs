using System.Diagnostics;
using Furion;
using Furion.DependencyInjection;
using Furion.EventBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetAdmin.DataContract.Dto.Sys.Log;
using NetAdmin.Host.Events.Sources;
using NSExt.Extensions;

namespace NetAdmin.Host.Aop;

/// <summary>
///     请求审计日志
/// </summary>
[SuppressSniffer]
public class RequestAuditFilter : IAsyncActionFilter
{
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestAuditFilter" /> class.
    /// </summary>
    public RequestAuditFilter(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var timeOperation = Stopwatch.StartNew();
        var resultContext = await next();
        timeOperation.Stop();
        var duration = (uint)timeOperation.ElapsedMilliseconds;

        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var (retType, retData) = GetReturnData(resultContext);

        var auditData = new CreateOperationLogReq {
                                                      ClientIp = context.HttpContext.GetRemoteIpAddressToIPv4()
                                                    , Duration = duration
                                                    , Environment = App.WebHostEnvironment.EnvironmentName
                                                    , Method = context.HttpContext.Request.Method
                                                    , ReferUrl = context.HttpContext.Request.GetRefererUrlAddress()
                                                    , RequestContentType = context.HttpContext.Request.ContentType
                                                    , RequestParameters = context.ActionArguments.Json()
                                                    , RequestUrl = context.HttpContext.Request.GetRequestUrlAddress()
                                                    , ResponseRawType
                                                          = actionDescriptor?.MethodInfo.ReturnType.ToString()
                                                    , Exception = resultContext.Exception?.ToString()
                                                    , ResponseWrapType = retType?.ToString()
                                                    , ResponseResult = retData?.Json()
                                                    , ServerIp = context.HttpContext.GetLocalIpAddressToIPv4()
                                                    , UserAgent = context.HttpContext.Request.Headers["User-Agent"]
                                                    , ApiId = actionDescriptor?.AttributeRouteInfo?.Template
                                                  };

        // 发布审计事件
        await _eventPublisher.PublishAsync(new OperationEvent(auditData));
    }

    /// <summary>
    ///     检查是否是有效的结果（可进行规范化的结果）
    /// </summary>
    private static bool CheckVaildResult(IActionResult result, out object data)
    {
        data = default;

        // 排除以下结果，跳过规范化处理
        var isDataResult = result switch {
                               ViewResult             => false
                             , PartialViewResult      => false
                             , FileResult             => false
                             , ChallengeResult        => false
                             , SignInResult           => false
                             , SignOutResult          => false
                             , RedirectToPageResult   => false
                             , RedirectToRouteResult  => false
                             , RedirectResult         => false
                             , RedirectToActionResult => false
                             , LocalRedirectResult    => false
                             , ForbidResult           => false
                             , ViewComponentResult    => false
                             , PageResult             => false
                             , NotFoundResult         => false
                             , NotFoundObjectResult   => false
                             , _                      => true
                           };

        // 目前支持返回值 ActionResult
        if (isDataResult) {
            data = result switch {
                       // 处理内容结果
                       ContentResult content => content.Content
                      ,

                       // 处理对象结果
                       ObjectResult obj => obj.Value
                      ,

                       // 处理 JSON 对象
                       JsonResult json => json.Value
                     , _               => null
                   };
        }

        return isDataResult;
    }

    private static (Type Type, object Data) GetReturnData(ActionExecutedContext resultContext)
    {
        Type type;

        // 解析返回值
        if (CheckVaildResult(resultContext.Result, out var data)) {
            type = data?.GetType();
        }

        // 处理文件类型
        else if (resultContext.Result is FileResult fileResult) {
            data = new {
                           FileName = fileResult.FileDownloadName
                         , fileResult.ContentType
                         , Length = fileResult is FileContentResult fileContentResult
                               ? (object)fileContentResult.FileContents.Length
                               : null
                       };

            type = fileResult.GetType();
        }
        else {
            type = resultContext.Result?.GetType();
        }

        return (type, data);
    }
}