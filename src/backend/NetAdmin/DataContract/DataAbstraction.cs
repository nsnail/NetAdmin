using NSExt.Extensions;

namespace NetAdmin.DataContract;

/// <summary>
///     数据基类
/// </summary>
public abstract record DataAbstraction
{
    /// <inheritdoc />
    public override string ToString()
    {
        return this.Json();
    }
}