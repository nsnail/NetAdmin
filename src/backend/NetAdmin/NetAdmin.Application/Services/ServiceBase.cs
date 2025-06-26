using NetAdmin.Domain.Contexts;

namespace NetAdmin.Application.Services;

/// <inheritdoc />
public abstract class ServiceBase<TLogger> : ServiceBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceBase{TLogger}" /> class.
    /// </summary>
    protected ServiceBase() //
    {
        Logger = S<ILogger<TLogger>>();
    }

    /// <summary>
    ///     日志记录器
    /// </summary>
    protected ILogger<TLogger> Logger { get; }
}

/// <summary>
///     服务基类
/// </summary>
public abstract class ServiceBase : IScoped, IService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceBase" /> class.
    /// </summary>
    protected ServiceBase()
    {
        UserToken = S<ContextUserToken>();
        ServiceId = Guid.NewGuid();
    }

    /// <inheritdoc />
    public Guid ServiceId { get; init; }

    /// <inheritdoc />
    public ContextUserToken UserToken { get; set; }

    /// <summary>
    ///     获取服务
    /// </summary>
    #pragma warning disable RCS1036
    #pragma warning restore RCS1036

    // ReSharper disable once MemberCanBeMadeStatic.Global
    #pragma warning disable CA1822, S2325
    protected T S<T>()
        #pragma warning restore S2325, CA1822
        where T : class
    {
        return App.GetService<T>();
    }
}