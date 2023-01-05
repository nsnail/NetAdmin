using Furion;
using Furion.DynamicApiController;

namespace NetAdmin.Api;

/// <summary>
///     Api 基类
/// </summary>
public abstract class ApiBase<T> : IDynamicApiController
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiBase{T}" /> class.
    ///     Api Controller 基类
    /// </summary>
    protected ApiBase()
    {
        Logger = App.GetService<ILogger<T>>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiBase{T}" /> class.
    /// </summary>
    protected ApiBase(ILogger<T> logger)
    {
        Logger = logger;
    }

    /// <summary>
    ///     日志记录器
    /// </summary>
    protected ILogger<T> Logger { get; }
}