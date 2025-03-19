using NetAdmin.Domain.Dto.Sys.File;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="IFileCache" />
public sealed class FileCache(IDistributedCache cache, IFileService service) //
    : DistributedCache<IFileService>(cache, service), IScoped, IFileCache
{
    /// <inheritdoc />
    public Task<UploadFileRsp> UploadAsync(IFormFile file)
    {
        return Service.UploadAsync(file);
    }
}