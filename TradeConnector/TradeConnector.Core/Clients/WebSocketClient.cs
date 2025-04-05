using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TradeConnector.Core.Clients.Interfaces;
using TradeConnector.Core.Infrastructure;
using TradeConnector.Core.Infrastructure.Interfaces;
using TradeConnector.Core.Models;

namespace TradeConnector.Core.Clients;

public class WebSocketClient : IWebSocketClient
{
    private readonly IWebSocketHelper _webSocketHelper;
    private readonly string _url;

    public event Action<PairTrade> NewBuyPairTrade;
    public event Action<PairTrade> NewSellPairTrade;
    public event Action<CurrencyTrade> NewBuyCurrencyTrade;
    public event Action<CurrencyTrade> NewSellCurrencyTrade;
    public event Action<Candle> CandleSeriesProcessing;

    public WebSocketClient(IWebSocketHelper webSocketHelper)
    {
        _webSocketHelper = webSocketHelper;
        _url = ApiEndpoints.WebSocketBaseUrl;
        _webSocketHelper.OnMessageReceived += OnMessageReceived;
    }

    public void OnMessageReceived(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return;

        try
        {
            var token = JToken.Parse(message);

            if (token is JArray array && array.Count > 0)
            {
                var firstItem = array.First;

                if (firstItem["Amount"] != null) // Проверяем наличие свойства Amount, если присутствует, то можем десериализовать в PairTrade/CurrencyTrade
                {
                    if (firstItem["Period"] == null) // Проверяем наличие свойства Period, если отсутствует, то десериализуем в PairTrade
                    {
                        var trades = array.ToObject<List<PairTrade>>()
                            .Select(trade => { trade.Symbol = trade.Symbol.Substring(1); return trade; });
                        foreach (var trade in trades)
                        {
                            if (trade.Amount > 0)
                                NewBuyPairTrade?.Invoke(trade);
                            else if (trade.Amount < 0)
                                NewSellPairTrade?.Invoke(trade);
                        }
                        return;
                    }
                    else // Если присутствует свойство Period, то десериализуем в CurrencyTrade
                    {
                        var trades = array.ToObject<List<CurrencyTrade>>() 
                            .Select(trade => { trade.Symbol = trade.Symbol.Substring(1); return trade; }); 
                        foreach (var trade in trades)
                        {
                            if (trade.Amount > 0)
                                NewBuyCurrencyTrade?.Invoke(trade);
                            else if (trade.Amount < 0)
                                NewSellCurrencyTrade?.Invoke(trade);
                        }
                        return;
                    }
                }
                else if (firstItem["OpenTime"] != null) // Проверяем наличие свойства OpenTime, если присутствует, то можем десериализовать в Candle
                {
                    var candles = array.ToObject<List<Candle>>()
                        .Select(candle => { candle.Symbol = candle.Symbol.Substring(1); return candle; });
                    foreach (var candle in candles)
                    {
                        CandleSeriesProcessing?.Invoke(candle);
                    }
                    return;
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Ошибка парсинга JSON: {ex.Message}");
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
