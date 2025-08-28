using Gurion.FriendlyException;

namespace NetAdmin.Host.Filters;

/// <inheritdoc cref="Gurion.FriendlyException.IGlobalExceptionHandler" />
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IGlobalExceptionHandler, ISingleton
{
    /// <inheritdoc />
    public Task OnExceptionAsync(ExceptionContext context) {
        if (context.Exception is NetAdminException and not NetAdminUnexpectedException) {
            logger.Warn(context.Exception);
        }
        else {
            logger.Error(context.Exception);
        }

        // 将异常设置到HttpContext.Features中 以便中间件能获取到他
        context.HttpContext.Features.Set<IExceptionHandlerFeature>(new ExceptionHandlerFeature { Error = context.Exception });

        return Task.CompletedTask;
    }
}