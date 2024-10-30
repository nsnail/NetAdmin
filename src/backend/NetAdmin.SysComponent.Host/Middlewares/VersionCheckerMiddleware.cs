using System.Net.WebSockets;

namespace NetAdmin.SysComponent.Host.Middlewares;

/// <summary>
///     版本更新检查中间件
/// </summary>
public sealed class VersionCheckerMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     主函数
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/ws/version") {
            if (context.WebSockets.IsWebSocketRequest) {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync().ConfigureAwait(false);
                await ConnectionAsync(webSocket).ConfigureAwait(false);
            }
            else {
                context.Response.StatusCode = 400;
            }
        }
        else {
            await next(context).ConfigureAwait(false);
        }
    }

    private static async Task ConnectionAsync(WebSocket webSocket)
    {
        var buffer = new byte[1024];
        while (webSocket.State == WebSocketState.Open) {
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(false);
            if (receiveResult.MessageType != WebSocketMessageType.Text) {
                continue;
            }

            var ver = await App.GetService<IToolsCache>().GetVersionAsync().ConfigureAwait(false);
            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(ver)), WebSocketMessageType.Text, true, CancellationToken.None)
                           .ConfigureAwait(false);
        }

        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).ConfigureAwait(false);
    }
}