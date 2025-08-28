namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IConstantCache" />
public sealed class ConstantCache(IDistributedCache cache, IConstantService service)
    : DistributedCache<IConstantService>(cache, service), IScoped, IConstantCache
{
    /// <inheritdoc />
    public IDictionary<string, string> GetCharsDic() {
        return Service.GetCharsDic();
    }

    /// <inheritdoc />
    public IDictionary<string, Dictionary<string, string[]>> GetEnums() {
        return Service.GetEnums();
    }

    /// <inheritdoc />
    public IDictionary<string, string> GetLocalizedStrings() {
        return Service.GetLocalizedStrings();
    }

    /// <inheritdoc />
    public IDictionary<string, long> GetNumbersDic() {
        return Service.GetNumbersDic();
    }
}