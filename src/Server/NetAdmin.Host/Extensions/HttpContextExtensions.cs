using System.Text;
using System.Text.RegularExpressions;

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
    public static async Task RemoveJsonNodeWithNullValue(this HttpContext me)
    {
        // 非json格式，退出
        if (!(me.Response.ContentType?.Contains(Chars.FLG_APPLICATION_JSON) ?? false)) {
            return;
        }

        // 流不可读，退出
        if (!me.Response.Body.CanSeek || !me.Response.Body.CanRead || !me.Response.Body.CanWrite) {
            return;
        }

        me.Response.Body.Seek(0, SeekOrigin.Begin);
        var sr         = new StreamReader(me.Response.Body);
        var bodyString = await sr.ReadToEndAsync();
        bodyString = _nullRegex.Replace(bodyString, string.Empty).Replace(",}", "}");
        me.Response.Body.Seek(0, SeekOrigin.Begin);
        var bytes = Encoding.UTF8.GetBytes(bodyString);
        me.Response.Body.SetLength(bytes.Length);
        await me.Response.Body.WriteAsync(bytes);
    }
}