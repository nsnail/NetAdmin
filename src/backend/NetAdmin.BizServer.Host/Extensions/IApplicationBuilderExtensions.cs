namespace NetAdmin.BizServer.Host.Extensions;

/// <summary>
///     ApplicationBuilder对象 扩展方法
/// </summary>
[SuppressSniffer]

// ReSharper disable once InconsistentNaming
public static class IApplicationBuilderExtensions
{
    private const           string _INDEX_HTML_PATH = ".index.html";
    private static readonly Regex  _regex           = new(@"\.(\w+)$", RegexOptions.Compiled);

    /// <summary>
    ///     托管管理后台
    /// </summary>
    public static IApplicationBuilder UseVueAdmin(this IApplicationBuilder me)
    {
        const string resPfx = $"{nameof(NetAdmin)}.{nameof(BizServer)}.{nameof(Host)}.UI";
        if (Assembly.GetExecutingAssembly().GetManifestResourceInfo($"{resPfx}.index.html") == null) {
            return me;
        }

        var allRes = Assembly.GetExecutingAssembly()
                             .GetManifestResourceNames()
                             .Where(x => x.StartsWith(resPfx, StringComparison.OrdinalIgnoreCase))
                             .Select(x => x[resPfx.Length..]);

        _ = me.Use(async (context, next) => {
            if (!context.Request.Path.StartsWithSegments(new PathString("/api"))) {
                var path = context.Request.Path.Value;
                if (path == "/" || path?.Length == 0) {
                    path = _INDEX_HTML_PATH;
                }

                path = path!.Replace('/', '.');

                var output = GetManifestResourceStream(path);
                if (output == null) {
                    var resName = allRes.FirstOrDefault(x => path.EndsWith(x, StringComparison.OrdinalIgnoreCase));
                    output = resName == null
                        ? GetManifestResourceStream(_INDEX_HTML_PATH)
                        : GetManifestResourceStream(resName);
                }

                if (output != null) {
                    context.Response.ContentLength = output.Length;
                    context.Response.ContentType
                        = MimeTypeHelper.GetMimeTypeByExtName(_regex.Match(path!).Groups[1].Value);
                    await output.CopyToAsync(context.Response.Body);
                    return;
                }
            }

            await next.Invoke();
        });

        return me;
    }

    private static Stream GetManifestResourceStream(string path)
    {
        return Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream($"{nameof(NetAdmin)}.{nameof(BizServer)}.{nameof(Host)}.UI{path}");
    }
}