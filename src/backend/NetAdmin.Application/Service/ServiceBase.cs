using Furion;
using Furion.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetAdmin.DataContract;

namespace NetAdmin.Application.Service;

/// <inheritdoc />
public abstract class ServiceBase<TLogger> : ServiceBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceBase{TLogger}" /> class.
    /// </summary>
    protected ServiceBase(ContextUser user) //
        : base(user)
    {
        Logger = App.GetRequiredService<ILogger<TLogger>>();
    }

    /// <summary>
    ///     日志记录器
    /// </summary>
    protected ILogger<TLogger> Logger { get; }
}

/// <summary>
///     服务基类
/// </summary>
public abstract class ServiceBase : IService, IScoped
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ServiceBase" /> class.
    /// </summary>
    protected ServiceBase(ContextUser user)
    {
        User = user;
        Id   = Guid.NewGuid();
    }

    /// <summary>
    ///     登录用户
    /// </summary>
    public ContextUser User { get; }

    /// <inheritdoc />
    public Guid Id { get; set; }
}