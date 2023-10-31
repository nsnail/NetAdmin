namespace NetAdmin.BizServer.Host.Extensions;

/// <summary>
///     ApplicationBuilder对象 扩展方法
/// </summary>
[SuppressSniffer]

// ReSharper disable once InconsistentNaming
public static class IApplicationBuilderExtensions
{
    private const           string              _INDEX_HTML_PATH = ".index.html";
    private const           string              _RES_PFX = $"{nameof(NetAdmin)}.{nameof(BizServer)}.{nameof(Host)}.UI";
    private static readonly Regex               _regex = new(@"\.(\w+)$", RegexOptions.Compiled);
    private static          IEnumerable<string> _allResNames;

    /// <summary>
    ///     托管管理后台
    /// </summary>
    public static IApplicationBuilder UseVueAdmin(this IApplicationBuilder me)
    {
        if (Assembly.GetExecutingAssembly().GetManifestResourceInfo(_RES_PFX + _INDEX_HTML_PATH) == null) {
            return me;
        }

        _allResNames = Assembly.GetExecutingAssembly()
                               .GetManifestResourceNames()
                               .Where(x => x.StartsWith(_RES_PFX, StringComparison.OrdinalIgnoreCase))
                               .Select(x => x[_RES_PFX.Length..]);

        _ = me.Use(UseVueAdminAsync);

        return me;
    }

    private static Stream GetManifestResourceStream(string path)
    {
        return Assembly.GetExecutingAssembly().GetManifestResourceStream(_RES_PFX + path);
    }

    private static async Task UseVueAdminAsync(HttpContext context, Func<Task> next)
    {
        if (!context.Request.Path.StartsWithSegments(new PathString("/api"))) {
            var path = context.Request.Path.Value;
            if (path == "/" || path?.Length == 0) {
                path = _INDEX_HTML_PATH;
            }

            path = path!.Replace('/', '.');

            var output = GetManifestResourceStream(path);
            if (output == null) {
                var resName = _allResNames.FirstOrDefault(x => path.EndsWith(x, StringComparison.OrdinalIgnoreCase));
                output = resName == null
                    ? GetManifestResourceStream(_INDEX_HTML_PATH)
                    : GetManifestResourceStream(resName);
            }

            if (output != null) {
                context.Response.ContentLength = output.Length;
                context.Response.ContentType = MimeTypeHelper.GetMimeTypeByExtName(_regex.Match(path!).Groups[1].Value);
                await output.CopyToAsync(context.Response.Body);
                return;
            }
        }

        await next.Invoke();
    }
}