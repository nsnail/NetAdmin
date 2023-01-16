using System.Data;
using FreeSql;

namespace NetAdmin.Host.Attributes;

/// <summary>
///     标记一个方法启用事务特性
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class TransactionAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionAttribute" /> class.
    /// </summary>
    public TransactionAttribute() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionAttribute" /> class.
    /// </summary>
    public TransactionAttribute(Propagation propagation) //
        : this(null, propagation) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionAttribute" /> class.
    /// </summary>
    public TransactionAttribute(IsolationLevel isolationLevel, Propagation propagation) //
        : this(new IsolationLevel?(isolationLevel), propagation) { }

    private TransactionAttribute(IsolationLevel? isolationLevel, Propagation propagation)
    {
        IsolationLevel = isolationLevel;
        Propagation    = propagation;
    }

    /// <summary>
    ///     事务隔离级别
    /// </summary>
    public IsolationLevel? IsolationLevel { get; }

    /// <summary>
    ///     事务传播方式
    /// </summary>
    public Propagation Propagation { get; } = Propagation.Required;
}