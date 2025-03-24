using System.Net.WebSockets;
using System.Text;

namespace TradeConnector.Core.Infrastructure;

public class WebSocketHelper
{
    private readonly ClientWebSocket _webSocket = new();
    private readonly Uri _uri;
    public event Action<string>? OnMessageReceived;
    public event Action? OnConnected;
    public event Action? OnDisconnected;

    public WebSocketHelper(string url)
    {
        _uri = new Uri(url);
    }

    public async Task ConnectAsync()
    {
        await _webSocket.ConnectAsync(_uri, CancellationToken.None);
        OnConnected?.Invoke();
        _ = ListenAsync();
    }

    public async Task DisconnectAsync()
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            OnDisconnected?.Invoke();
        }
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var jsonMessage = JsonHelper.Serialize(message);
        var buffer = Encoding.UTF8.GetBytes(jsonMessage);
        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task ListenAsync()
    {
        var buffer = new byte[4096];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await DisconnectAsync();
                break;
            }

            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var deserializedMessage = JsonHelper.Deserialize<string>(message);

            OnMessageReceived?.Invoke(deserializedMessage);
        }
    }
}
