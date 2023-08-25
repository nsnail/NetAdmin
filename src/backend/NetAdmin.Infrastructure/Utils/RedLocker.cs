using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     Redis锁
/// </summary>
#pragma warning disable DesignedForInheritance
public class RedLocker : IDisposable, ISingleton
    #pragma warning restore DesignedForInheritance
{
    // Track whether Dispose has been called.
    private bool _disposed;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RedLocker" /> class.
    /// </summary>
    public RedLocker(IOptions<RedisOptions> redisOptions)
    {
        RedlockFactory = RedLockFactory.Create( //
            new List<RedLockMultiplexer>        //
            {
                ConnectionMultiplexer.Connect( //
                    redisOptions.Value.Instances.First(x => x.Name == Chars.FLG_REDIS_INSTANCE_DATA_CACHE).ConnStr)
            });
    }

    /// <summary>
    ///     Finalizes an instance of the <see cref="RedLocker" /> class.
    ///     Use C# finalizer syntax for finalization code.
    ///     This finalizer will run only if the Dispose method
    ///     does not get called.
    ///     It gives your base class the opportunity to finalize.
    ///     Do not provide finalizer in types derived from this class.
    /// </summary>
    ~RedLocker()
    {
        // Do not re-create Dispose clean-up code here.
        // Calling Dispose(disposing: false) is optimal in terms of
        // readability and maintainability.
        Dispose(false);
    }

    /// <summary>
    ///     RedlockFactory
    /// </summary>
    public RedLockFactory RedlockFactory { get; }

    /// <summary>
    ///     Implement IDisposable.
    ///     Do not make this method virtual.
    ///     A derived class should not be able to override this method.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);

        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC.SuppressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Dispose(bool disposing) executes in two distinct scenarios.
    ///     If disposing equals true, the method has been called directly
    ///     or indirectly by a user's code. Managed and unmanaged resources
    ///     can be disposed.
    ///     If disposing equals false, the method has been called by the
    ///     runtime from inside the finalizer and you should not reference
    ///     other objects. Only unmanaged resources can be disposed.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (_disposed) {
            return;
        }

        // If disposing equals true, dispose all managed
        // and unmanaged resources.
        if (disposing) {
            RedlockFactory.Dispose();
        }

        // Call the appropriate methods to clean up
        // unmanaged resources here.
        // If disposing is false,
        // only the following code is executed.

        // Note disposing has been done.
        _disposed = true;
    }
}