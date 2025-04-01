using System.Diagnostics;
using TradeConnector.Core.Clients.Interfaces.Bitfinex;
using TradeConnector.Core.Infrastructure;
using TradeConnector.Core.Models;

namespace TradeConnector.Core.Clients;

public class RestClient: IRestClient
{
    private readonly HttpClient _httpClient;
    public RestClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<PairTrade>> GetPairTradesAsync(string pair, int maxCount)
    {
        var endpoint = ApiEndpoints.GetPairTrades(pair, maxCount);
        var response = await _httpClient.GetStringAsync(endpoint);
        var pairTrades = JsonHelper.Deserialize<PairTrade>(response);
        return pairTrades.Select(trade => { trade.Symbol = pair.Substring(1); return trade;});
    }
    public async Task<IEnumerable<CurrencyTrade>> GetCurrencyTradesAsync(string currency, int maxCount)
    {
        var endpoint = ApiEndpoints.GetCurrencyTrades(currency, maxCount);
        var response = await _httpClient.GetStringAsync(endpoint);
        var currencyTrades = JsonHelper.Deserialize<CurrencyTrade>(response);
        return currencyTrades.Select(trade => { trade.Symbol = currency.Substring(1); return trade; });
    }
    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int periodInMin,
            DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
    {
        var endpoint = ApiEndpoints.GetCandleSeries(symbol, periodInMin, from, to, count);
        var response = await _httpClient.GetStringAsync(endpoint);
        var candleSeries = JsonHelper.Deserialize<Candle>(response);
        return candleSeries.Select(candle => { candle.Symbol = symbol.Substring(1); return candle; });
    }
    public async Task<IEnumerable<PairTicker>> GetPairTickerAsync(string pair)
    {
        var endpoint = ApiEndpoints.GetPairTicker(pair);
        var response = await _httpClient.GetStringAsync(endpoint);
        return JsonHelper.Deserialize<PairTicker>(response);
    }
    public async Task<IEnumerable<CurrencyTicker>> GetCurrencyTickerAsync(string currency)
    {
        var endpoint = ApiEndpoints.GetCurrencyTicker(currency);
        var response = await _httpClient.GetStringAsync(endpoint);
        return JsonHelper.Deserialize<CurrencyTicker>(response);
    }

}
