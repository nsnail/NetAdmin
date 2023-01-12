using Furion;
using Furion.DynamicApiController;
using Microsoft.Extensions.Logging;

namespace NetAdmin.Application.Service;

/// <summary>
///     服务基类
/// </summary>
public abstract class ServiceBase<TLogger> : IDynamicApiController
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceBase{TLogger}" /> class.
    /// </summary>
    protected ServiceBase()
    {
        Logger = App.GetService<ILogger<TLogger>>();
    }

    /// <summary>
    ///     日志记录器
    /// </summary>
    protected ILogger<TLogger> Logger { get; }
}