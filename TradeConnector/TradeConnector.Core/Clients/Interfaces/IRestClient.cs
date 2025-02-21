using TradeConnector.Core.Models;

namespace TradeConnector.Core.Clients.Interfaces.Bitfinex;

public interface IRestClient
{
    Task<IEnumerable<Trade>> GetTradesAsync(string pair, int maxCount);
    Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec,
        DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0);
    Task<PairTicker> GetTickerAsync(string pair, int maxCount);

}
