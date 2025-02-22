using TradeConnector.Core.Models;

namespace TradeConnector.Core.Clients.Interfaces.Bitfinex;

public interface IRestClient
{
    Task<IEnumerable<PairTrade>> GetPairTradesAsync(string pair, int maxCount);
    Task<IEnumerable<CurrencyTrade>> GetCurrencyTradesAsync(string currency, int maxCount);

    Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int periodInMin,
        DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0);
    Task<PairTicker> GetPairTickerAsync(string pair);
    Task<CurrencyTicker> GetCurrencyTickerAsync(string currency);

}
