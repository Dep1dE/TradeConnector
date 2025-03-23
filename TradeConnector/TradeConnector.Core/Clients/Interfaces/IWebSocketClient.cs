using System.Diagnostics;
using TradeConnector.Core.Models;

namespace TradeConnector.Core.Clients.Interfaces;

public interface IWebSocketClient
{
    /// <summary>
    /// For trading pairs, ex. tBTCUSD
    /// </summary>
    event Action<PairTrade> NewBuyPairTrade;
    event Action<PairTrade> NewSellPairTrade;

    /// <summary>
    /// For funding currency symbols (ex. fUSD)
    /// </summary>
    event Action<CurrencyTrade> NewBuyCurrencyTrade;
    event Action<CurrencyTrade> NewSellCurrencyTrade;

    void SubscribePairTrades(string pair, int maxCount = 100);
    void UnsubscribePairTrades(string pair);

    void SubscribeCurrencyTrades(string currency, int maxCount = 100);
    void UnsubscribeCurrencyTrades(string currency);

    event Action<Candle> CandleSeriesProcessing;

    void SubscribeCandles(string symbol, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0);
    void UnsubscribeCandles(string symbol);

    Task ConnectAsync();
    Task DisconnectAsync();
}
