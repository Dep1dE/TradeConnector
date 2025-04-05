using TradeConnector.Core.Clients.Interfaces.Bitfinex;
using TradeConnector.Core.Clients.Interfaces;
using TradeConnector.Core.Connectors.Interfaces;
using TradeConnector.Core.Models;

namespace TradeConnector.Core.Connectors;

public class Connector: IConnector
{
    private readonly IRestClient _restClient;
    private readonly IWebSocketClient _webSocketClient;

    public event Action<PairTrade> NewBuyPairTrade;
    public event Action<PairTrade> NewSellPairTrade;
    public event Action<CurrencyTrade> NewBuyCurrencyTrade;
    public event Action<CurrencyTrade> NewSellCurrencyTrade;
    public event Action<Candle> CandleSeriesProcessing;

    public Connector(IRestClient restClient, IWebSocketClient webSocketClient)
    {
        _restClient = restClient;
        _webSocketClient = webSocketClient;

        _webSocketClient.NewBuyPairTrade += pairTrade => NewBuyPairTrade?.Invoke(pairTrade);
        _webSocketClient.NewSellPairTrade += pairTrade => NewSellPairTrade?.Invoke(pairTrade);
        _webSocketClient.NewBuyCurrencyTrade += currencyTrade => NewBuyCurrencyTrade?.Invoke(currencyTrade);
        _webSocketClient.NewSellCurrencyTrade += currencyTrade => NewSellCurrencyTrade?.Invoke(currencyTrade);
        _webSocketClient.CandleSeriesProcessing += candle => CandleSeriesProcessing?.Invoke(candle);
    }

    #region Rest

    public Task<IEnumerable<PairTrade>> GetNewPairTradesAsync(string pair, int maxCount)
    {
        return _restClient.GetPairTradesAsync(pair, maxCount);
    }

    public Task<IEnumerable<CurrencyTrade>> GetNewCurrencyTradesAsync(string currency, int maxCount)
    {
        return _restClient.GetCurrencyTradesAsync(currency, maxCount);
    }

    public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int periodInMin, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
    {
        return _restClient.GetCandleSeriesAsync(symbol, periodInMin, from, to, count);
    }

    public Task<IEnumerable<PairTicker>> GetNewPairTickerAsync(string pair)
    {
        return _restClient.GetPairTickerAsync(pair);
    }

    public Task<IEnumerable<CurrencyTicker>> GetNewCurrencyTickerAsync(string currency)
    {
        return _restClient.GetCurrencyTickerAsync(currency);
    }

    #endregion

    #region Socket

    public void SubscribePairTrades(string pair, int maxCount = 100)
    {
        _webSocketClient.SubscribePairTrades(pair, maxCount);
    }

    public void UnsubscribePairTrades(string pair)
    {
        _webSocketClient.UnsubscribePairTrades(pair);
    }

    public void SubscribeCurrencyTrades(string currency, int maxCount = 100)
    {
        _webSocketClient.SubscribeCurrencyTrades(currency, maxCount);
    }

    public void UnsubscribeCurrencyTrades(string currency)
    {
        _webSocketClient.UnsubscribeCurrencyTrades(currency);
    }

    public void SubscribeCandles(string symbol, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
    {
        _webSocketClient.SubscribeCandles(symbol, periodInSec, from, to, count);
    }

    public void UnsubscribeCandles(string symbol)
    {
        _webSocketClient.UnsubscribeCandles(symbol);
    }

    public Task ConnectAsync()
    {
        return _webSocketClient.ConnectAsync();
    }

    public Task DisconnectAsync()
    {
        return _webSocketClient.DisconnectAsync();
    }

    #endregion
}
