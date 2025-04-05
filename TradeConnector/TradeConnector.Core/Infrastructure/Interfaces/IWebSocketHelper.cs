namespace TradeConnector.Core.Infrastructure.Interfaces;

public interface IWebSocketHelper
{
    event Action<string>? OnMessageReceived;
    event Action? OnConnected;
    event Action? OnDisconnected;

    Task ConnectAsync();
    Task DisconnectAsync();
    Task SendMessageAsync<T>(T message);
}
