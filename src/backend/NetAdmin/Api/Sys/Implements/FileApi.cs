using System.Globalization;
using Furion;
using Furion.FriendlyException;
using Microsoft.Extensions.Options;
using NetAdmin.Infrastructure.Configuration.Options;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Extensions;
using NetAdmin.Infrastructure.Utils;
using NetAdmin.Lang;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Api.Sys.IFileApi" />
public class FileApi : ApiBase<IFileApi>, IFileApi
{
    private readonly MinioHelper   _minioHelper;
    private readonly UploadOptions _uploadOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FileApi" /> class.
    /// </summary>
    public FileApi(IOptions<UploadOptions> uploadOptions, MinioHelper minioHelper)
    {
        _minioHelper   = minioHelper;
        _uploadOptions = uploadOptions.Value;
    }

    /// <inheritdoc />
    public async Task<string> Upload(IFormFile file)
    {
        if (file is null || file.Length < 1) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.FILE_CANNOT_BE_EMPTY);
        }

        if (!_uploadOptions.ContentTypes.Contains(file.ContentType)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation
              , string.Format(CultureInfo.InvariantCulture, Str.THE_ALLOWED_FILE_FORMATS_ARE
                            , string.Join(",", _uploadOptions.ContentTypes)));
        }

        if (!(file.Length <= _uploadOptions.MaxSize)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation
              , string.Format(CultureInfo.InvariantCulture, Str.MAXIMUM_NUMBER_OF_FILE_BYTES_ALLOWED
                            , _uploadOptions.MaxSize));
        }

        var             fileName   = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var             objectName = $"{App.User.AsContextUser().Id}/{fileName}";
        await using var fs         = file.OpenReadStream();
        var             ret        = await _minioHelper.Upload(objectName, fs, file.ContentType, file.Length);
        return ret;
    }
}