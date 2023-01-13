using System.Globalization;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Lang;
using NetAdmin.Infrastructure.Utils;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="IFileService" />
public class FileService : ServiceBase<IFileService>, IFileService, IDynamicApiController
{
    private readonly MinioHelper   _minioHelper;
    private readonly UploadOptions _uploadOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FileService" /> class.
    /// </summary>
    public FileService(IOptions<UploadOptions> uploadOptions, MinioHelper minioHelper) //
    {
        _minioHelper   = minioHelper;
        _uploadOptions = uploadOptions.Value;
    }

    /// <inheritdoc />
    public async Task<string> Upload(IFormFile file)
    {
        if (file is null || file.Length < 1) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.File_cannot_be_empty);
        }

        if (!_uploadOptions.ContentTypes.Contains(file.ContentType)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation
              , string.Format(CultureInfo.InvariantCulture, Str.The_allowed_file_formats_are
                            , string.Join(",", _uploadOptions.ContentTypes)));
        }

        if (!(file.Length <= _uploadOptions.MaxSize)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation
              , string.Format(CultureInfo.InvariantCulture, Str.Maximum_number_of_file_bytes_allowed
                            , _uploadOptions.MaxSize));
        }

        var             fileName   = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var             objectName = $"{User.Id}/{fileName}";
        await using var fs         = file.OpenReadStream();
        var             ret        = await _minioHelper.Upload(objectName, fs, file.ContentType, file.Length);
        return ret;
    }
}