using System.Text;
using System.Text.RegularExpressions;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.Aop.Middlewares;

/// <summary>
///     删除json null属性中间件
/// </summary>
public class RemoveNullPropertyMiddleware
{
    private static readonly Regex           _nullRegex = new("\"[^\"]+?\":null,?", RegexOptions.Compiled);
    private readonly        RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RemoveNullPropertyMiddleware" /> class.
    /// </summary>
    public RemoveNullPropertyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///     InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        if ((context.Response.ContentType?.Contains(Chars.FLG_APPLICATION_JSON) ?? false) &&
            context.GetMetadata<JsonIgnoreNullAttribute>() is not null) {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var sr         = new StreamReader(context.Response.Body);
            var bodyString = await sr.ReadToEndAsync();
            bodyString = _nullRegex.Replace(bodyString, string.Empty).Replace(",}", "}");
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var bytes = Encoding.UTF8.GetBytes(bodyString);
            context.Response.Body.SetLength(bytes.Length);
            await context.Response.Body.WriteAsync(bytes);
        }
    }
}