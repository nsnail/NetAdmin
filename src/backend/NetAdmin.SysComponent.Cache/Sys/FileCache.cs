using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IFileCache" />
public sealed class FileCache : DistributedCache<IFileService>, IScoped, IFileCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FileCache" /> class.
    /// </summary>
    public FileCache(IDistributedCache cache, IFileService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<string> UploadAsync(IFormFile file)
    {
        return Service.UploadAsync(file);
    }
}