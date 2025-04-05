using TradeConnector.Core.Models;

namespace TradeConnector.Core.Connectors.Interfaces;

public interface IConnector
{
    #region Rest

    Task<IEnumerable<PairTrade>> GetNewPairTradesAsync(string pair, int maxCount);
    Task<IEnumerable<CurrencyTrade>> GetNewCurrencyTradesAsync(string currency, int maxCount);

    Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int periodInMin,
        DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0);
    Task<IEnumerable<PairTicker>> GetNewPairTickerAsync(string pair);
    Task<IEnumerable<CurrencyTicker>> GetNewCurrencyTickerAsync(string currency);

    #endregion

    #region Socket


    event Action<PairTrade> NewBuyPairTrade;
    event Action<PairTrade> NewSellPairTrade;
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

    #endregion
}
