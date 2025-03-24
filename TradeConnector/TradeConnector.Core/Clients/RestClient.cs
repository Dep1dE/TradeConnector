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
    private async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetStringAsync(endpoint);
        return JsonHelper.Deserialize<T>(response);
    }
    public async Task<IEnumerable<PairTrade>> GetPairTradesAsync(string pair, int maxCount)
    {
        var endpoint = ApiEndpoints.GetPairTrades(pair, maxCount);
        return await GetAsync<IEnumerable<PairTrade>>(endpoint);
    }
    public async Task<IEnumerable<CurrencyTrade>> GetCurrencyTradesAsync(string currency, int maxCount)
    {
        var endpoint = ApiEndpoints.GetCurrencyTrades(currency, maxCount);
        return await GetAsync<IEnumerable<CurrencyTrade>>(endpoint);
    }
    public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string symbol, int periodInMin,
            DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
    {
        var endpoint = ApiEndpoints.GetCandleSeries(symbol, periodInMin, from, to, count);
        return await GetAsync<IEnumerable<Candle>>(endpoint);
    }
    public async Task<PairTicker> GetPairTickerAsync(string pair)
    {
        var endpoint = ApiEndpoints.GetPairTicker(pair);
        return await GetAsync<PairTicker>(endpoint);
    }
    public async Task<CurrencyTicker> GetCurrencyTickerAsync(string currency)
    {
        var endpoint = ApiEndpoints.GetCurrencyTicker(currency);
        return await GetAsync<CurrencyTicker>(endpoint);
    }
}
