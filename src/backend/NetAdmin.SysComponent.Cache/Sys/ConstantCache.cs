using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IConstantCache" />
public sealed class ConstantCache : DistributedCache<IConstantService>, IScoped, IConstantCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConstantCache" /> class.
    /// </summary>
    public ConstantCache(IDistributedCache cache, IConstantService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public IDictionary<string, string> GetCharsDic()
    {
        return Service.GetCharsDic();
    }

    /// <inheritdoc />
    public IDictionary<string, Dictionary<string, string[]>> GetEnums()
    {
        return Service.GetEnums();
    }

    /// <inheritdoc />
    public IDictionary<string, string> GetLocalizedStrings()
    {
        return Service.GetLocalizedStrings();
    }

    /// <inheritdoc />
    public IDictionary<string, long> GetNumbersDic()
    {
        return Service.GetNumbersDic();
    }
}