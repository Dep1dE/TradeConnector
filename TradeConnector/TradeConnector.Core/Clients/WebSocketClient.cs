using TradeConnector.Core.Clients.Interfaces;
using TradeConnector.Core.Infrastructure;
using TradeConnector.Core.Models;

namespace TradeConnector.Core.Clients;

public class WebSocketClient : IWebSocketClient
{
    private readonly WebSocketHelper _webSocketHelper;
    private readonly string _url;

    public event Action<PairTrade> NewBuyPairTrade;
    public event Action<PairTrade> NewSellPairTrade;
    public event Action<CurrencyTrade> NewBuyCurrencyTrade;
    public event Action<CurrencyTrade> NewSellCurrencyTrade;
    public event Action<Candle> CandleSeriesProcessing;

    public WebSocketClient()
    {
        _url = ApiEndpoints.WebSocketBaseUrl;
        _webSocketHelper = new WebSocketHelper(_url);
        _webSocketHelper.OnMessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(string message)
    {
        var tradeMessage = JsonHelper.Deserialize<List<object>>(message); 

        if (tradeMessage.Count > 0)
        {
            if (tradeMessage[0] is PairTrade pairTrade)
            {
                if (pairTrade.Side == "buy")
                    NewBuyPairTrade?.Invoke(pairTrade);
                else if (pairTrade.Side == "sell")
                    NewSellPairTrade?.Invoke(pairTrade);
            }
            else if (tradeMessage[0] is CurrencyTrade currencyTrade)
            {
                if (currencyTrade.Side == "buy")
                    NewBuyCurrencyTrade?.Invoke(currencyTrade);
                else if (currencyTrade.Side == "sell")
                    NewSellCurrencyTrade?.Invoke(currencyTrade);
            }
            else if (tradeMessage[0] is Candle candle)
            {
                CandleSeriesProcessing?.Invoke(candle);
            }
        }
    }

    public void SubscribePairTrades(string pair, int maxCount = 100)
    {
        var subscribeMessage = new
        {
            eventType = "subscribe",
            channel = "trades",
            symbol = pair,
            limit = maxCount
        };

        _webSocketHelper.SendMessageAsync(subscribeMessage);
    }

    public void UnsubscribePairTrades(string pair)
    {
        var unsubscribeMessage = new
        {
            eventType = "unsubscribe",
            channel = "trades",
            symbol = pair
        };

        _webSocketHelper.SendMessageAsync(unsubscribeMessage);
    }

    public void SubscribeCurrencyTrades(string currency, int maxCount = 100)
    {
        var subscribeMessage = new
        {
            eventType = "subscribe",
            channel = "trades",
            symbol = currency,
            limit = maxCount
        };

        _webSocketHelper.SendMessageAsync(subscribeMessage);
    }

    public void UnsubscribeCurrencyTrades(string currency)
    {
        var unsubscribeMessage = new
        {
            eventType = "unsubscribe",
            channel = "trades",
            symbol = currency
        };

        _webSocketHelper.SendMessageAsync(unsubscribeMessage);
    }

    public void SubscribeCandles(string symbol, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
    {
        var subscribeMessage = new
        {
            eventType = "subscribe",
            channel = "candles",
            symbol = symbol,
            period = periodInSec,
            from = from?.ToUnixTimeSeconds(),
            to = to?.ToUnixTimeSeconds(),
            limit = count
        };

        _webSocketHelper.SendMessageAsync(subscribeMessage);
    }

    public void UnsubscribeCandles(string symbol)
    {
        var unsubscribeMessage = new
        {
            eventType = "unsubscribe",
            channel = "candles",
            symbol = symbol
        };

        _webSocketHelper.SendMessageAsync(unsubscribeMessage);
    }

    public async Task ConnectAsync()
    {
        await _webSocketHelper.ConnectAsync();
    }

    public async Task DisconnectAsync()
    {
        await _webSocketHelper.DisconnectAsync();
    }
}
