namespace NetAdmin.Host.Extensions;

/// <summary>
///     HttpContext 扩展方法
/// </summary>
public static class HttpContextExtensions
{
    private static readonly Regex _nullRegex = new("\"[^\"]+?\":null,?", RegexOptions.Compiled);

    /// <summary>
    ///     删除 response json body 中value 为null的节点
    /// </summary>
    public static async Task RemoveJsonNodeWithNullValueAsync(this HttpContext me) {
        // 非json格式，退出
        if (!(me.Response.ContentType?.Contains(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON) ?? false)) {
            return;
        }

        // 流不可读，退出
        if (!me.Response.Body.CanSeek || !me.Response.Body.CanRead || !me.Response.Body.CanWrite) {
            return;
        }

        _ = me.Response.Body.Seek(0, SeekOrigin.Begin);
        var sr = new StreamReader(me.Response.Body);
        var bodyString = await sr.ReadToEndAsync().ConfigureAwait(false);
        bodyString = _nullRegex.Replace(bodyString, string.Empty).Replace(",}", "}");
        _ = me.Response.Body.Seek(0, SeekOrigin.Begin);
        var bytes = Encoding.UTF8.GetBytes(bodyString);
        me.Response.Body.SetLength(bytes.Length);
        await me.Response.Body.WriteAsync(bytes).ConfigureAwait(false);
    }
}