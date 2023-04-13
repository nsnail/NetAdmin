using Furion.FriendlyException;

namespace NetAdmin.Host.Filters;

/// <summary>
///     全局捕异常
/// </summary>
public sealed class GlobalExceptionHandler : IGlobalExceptionHandler, ISingleton
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GlobalExceptionHandler" /> class.
    /// </summary>
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task OnExceptionAsync(ExceptionContext context)
    {
        _logger.Error(context.Exception);

        // 将异常设置到HttpContext.Features中 以便中间件能获取到他
        context.HttpContext.Features.Set<IExceptionHandlerFeature>(
            new ExceptionHandlerFeature { Error = context.Exception });

        return Task.CompletedTask;
    }
}