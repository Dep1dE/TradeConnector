namespace TradeConnector.Core.Infrastructure;

public static class ApiEndpoints
{
    public const string RestBaseUrl = "https://api-pub.bitfinex.com/v2";
    public const string WebSocketBaseUrl = "wss://api-pub.bitfinex.com/ws/2";

    public static string GetCurrencyTrades(string currency, int maxCount) => $"{RestBaseUrl}/trades/{currency}/hist?limit={maxCount}";
    public static string GetPairTrades(string pair, int maxCount) => $"{RestBaseUrl}/trades/{pair}/hist?limit={maxCount}";
    public static string GetCurrencyTicker(string currency) => $"{RestBaseUrl}/tickers?symbols={currency}";
    public static string GetPairTicker(string pair) => $"{RestBaseUrl}/tickers?symbols={pair}";
    public static string GetCandleSeries(string symbol, int periodInMin, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
    {
        string timeframe = periodInMin switch
        {
            1 => "1m",
            5 => "5m",
            15 => "15m",
            30 => "30m",
            60 => "1h",
            180 => "3h",
            360 => "6h",
            720 => "12h",
            1440 => "1D",
            10080 => "7D",
            302400 => "1M",
            _ => throw new ArgumentException("Invalid period in minutes", nameof(periodInMin))
        };

        string endpoint = $"{RestBaseUrl}/candles/trade:{timeframe}:{symbol}/hist?limit={count}";

        if (from.HasValue)
            endpoint += $"&start={from.Value.ToUnixTimeMilliseconds()}";
        if (to.HasValue)
            endpoint += $"&end={to.Value.ToUnixTimeMilliseconds()}";

        return endpoint;
    }
}
