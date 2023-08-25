namespace NetAdmin.Host.Attributes;

/// <summary>
///     标记一个Action启用事务
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class TransactionAttribute : Attribute
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