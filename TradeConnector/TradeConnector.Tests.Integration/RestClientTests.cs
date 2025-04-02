using RichardSzalay.MockHttp;
using TradeConnector.Core.Clients;

namespace TradeConnector.Tests.Integration;

public class RestClientTests
{
    [Fact]
    public async Task GetPairTradesAsyncTest()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api-pub.bitfinex.com/v2/trades/tBTCUSD/hist?limit=5")
            .Respond("application/json", "[[1742027455, 1742920552754, -0.00016931, 87880], [1742027454, 1742920552753, -0.00008835, 87881]]");

        var httpClient = new HttpClient(mockHttp);
        var restClient = new RestClient(httpClient);

        var trades = await restClient.GetPairTradesAsync("tBTCUSD", 5);
        var trade = trades.First();
        Assert.NotNull(trades);
        Assert.Equal(2, trades.Count());
        Assert.Equal("BTCUSD", trade.Symbol);
        Assert.Equal(1742027455m, trade.Id);
        Assert.Equal(1742920552754m, trade.TimestampMs);
        Assert.Equal(-0.00016931m, trade.Amount);
        Assert.Equal("sell", trade.Side);
        Assert.Equal(87880m, trade.Price);
    }

    [Fact]
    public async Task GetCurrencyTradesAsyncTest()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api-pub.bitfinex.com/v2/trades/fUSD/hist?limit=3")
            .Respond("application/json", "[[356834889, 1743304710629, 289.657, 0.000147, 2]]");

        var httpClient = new HttpClient(mockHttp);
        var restClient = new RestClient(httpClient);

        var trades = await restClient.GetCurrencyTradesAsync("fUSD", 3);
        var trade = trades.First();
        Assert.NotNull(trades);
        Assert.Single(trades);
        Assert.Equal("USD", trade.Symbol);
        Assert.Equal(356834889m, trade.Id);
        Assert.Equal(1743304710629m, trade.TimestampMs);
        Assert.Equal(289.657m, trade.Amount);
        Assert.Equal("buy", trade.Side);
        Assert.Equal(0.000147m, trade.Rate);
        Assert.Equal(2m, trade.Period);
    }

    [Fact]
    public async Task GetCandleSeriesAsyncTest()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api-pub.bitfinex.com/v2/candles/trade:1m:tBTCUSD/hist?limit=3")
            .Respond("application/json", "[[1742920552754, 87880, 87900, 87750, 87800, 50], [1742920552753, 87850, 87950, 87800, 87900, 30], [1742920552752, 87900, 88000, 87850, 87950, 20]]");

        var httpClient = new HttpClient(mockHttp);
        var restClient = new RestClient(httpClient);

        var candles = await restClient.GetCandleSeriesAsync("tBTCUSD", 1, null, null, 3);
        var candle = candles.First();
        Assert.NotNull(candles);
        Assert.Equal(3, candles.Count());
        Assert.Equal("BTCUSD", candle.Symbol);
        Assert.Equal(1742920552754m, candle.TimestampMs);
        Assert.Equal(87880m, candle.OpenPrice);
        Assert.Equal(87900m, candle.ClosePrice);
        Assert.Equal(87750m, candle.HighPrice);
        Assert.Equal(87800m, candle.LowPrice);
        Assert.Equal(50m, candle.Volume);
    }

    [Fact]
    public async Task GetPairTickerAsyncTest()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api-pub.bitfinex.com/v2/tickers?symbols=tBTCUSD")
            .Respond("application/json", "[[\"tBTCUSD\", 88293, 7.93307334, 88294, 8.1553634, 2928, 0.03429977, 88293, 369.7917345, 88868, 85147]]");

        var httpClient = new HttpClient(mockHttp);
        var restClient = new RestClient(httpClient);

        var tickers = await restClient.GetPairTickerAsync("tBTCUSD");
        var ticker = tickers.First();
        Assert.NotNull(tickers);
        Assert.Single(tickers);
        Assert.Equal("BTCUSD", ticker.Symbol);
        Assert.Equal(88293m, ticker.Bid);
        Assert.Equal(7.93307334m, ticker.BidSize);
        Assert.Equal(88294m, ticker.Ask);
        Assert.Equal(8.1553634m, ticker.AskSize);
        Assert.Equal(2928m, ticker.DailyChange);
        Assert.Equal(0.03429977m, ticker.DaileChangeRelative);
        Assert.Equal(88293m, ticker.LastPrice);
        Assert.Equal(369.7917345m, ticker.Volume);
        Assert.Equal(88868m, ticker.High);
        Assert.Equal(85147m, ticker.Low);
    }

    [Fact]
    public async Task GetCurrencyTickerAsyncTest()
    {
        var mockHttp = new MockHttpMessageHandler();
        mockHttp.When("https://api-pub.bitfinex.com/v2/tickers?symbols=fUSD")
            .Respond("application/json", "[[\"fUSD\", 0.001, 10000, 7, 0.002, 20000, 14, 0.0015, 0.05, 0.0012, 12345, 0.0021, 0.0009, 5000, 0.03]]");

        var httpClient = new HttpClient(mockHttp);
        var restClient = new RestClient(httpClient);

        var tickers = await restClient.GetCurrencyTickerAsync("fUSD");
        var ticker = tickers.First();
        Assert.NotNull(tickers);
        Assert.Single(tickers);
        Assert.Equal("USD", ticker.Symbol);
        Assert.Equal(0.001m, ticker.Frr);
        Assert.Equal(10000m, ticker.Bid);
        Assert.Equal(7, ticker.BidPeriod);
        Assert.Equal(0.002m, ticker.BidSize);
        Assert.Equal(20000m, ticker.Ask);
        Assert.Equal(14, ticker.AskPeriod);
        Assert.Equal(0.0015m, ticker.AskSize);
        Assert.Equal(0.05m, ticker.DailyChange);
        Assert.Equal(0.0012m, ticker.DaileChangePerc);
        Assert.Equal(12345m, ticker.LastPrice);
        Assert.Equal(0.0021m, ticker.Volume);
        Assert.Equal(0.0009m, ticker.High);
        Assert.Equal(5000m, ticker.Low);
        Assert.Equal(0.03m, ticker.FrrAmountAvailable);
    }
}