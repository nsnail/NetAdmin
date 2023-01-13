using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;
using NSExt.Extensions;

namespace NetAdmin.Host.Aop;

/// <summary>
///     全局捕异常日志
/// </summary>
public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
{
    private readonly ILogger<LogExceptionHandler> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LogExceptionHandler" /> class.
    /// </summary>
    public LogExceptionHandler(ILogger<LogExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public Task OnExceptionAsync(ExceptionContext context)
    {
        _logger.Error(context.Exception);
        return Task.CompletedTask;
    }
}