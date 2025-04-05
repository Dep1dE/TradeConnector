using Moq;
using TradeConnector.Core.Clients;
using TradeConnector.Core.Models;
using Newtonsoft.Json;
using TradeConnector.Core.Infrastructure.Interfaces;
using Xunit.Abstractions;

public class WebSocketClientTests
{
    private readonly Mock<IWebSocketHelper> _webSocketHelperMock;
    private readonly WebSocketClient _webSocketClient;
    private readonly ITestOutputHelper _output;

    public WebSocketClientTests(ITestOutputHelper output)
    {
        _webSocketHelperMock = new Mock<IWebSocketHelper>();
        _webSocketClient = new WebSocketClient(_webSocketHelperMock.Object);
        _output = output;
    }

    [Fact]
    public async Task SubscribePairTradesTest()
    {
        var pair = "tBTCUSD";
        var maxCount = 100;

        _webSocketClient.SubscribePairTrades(pair, maxCount);

        _webSocketHelperMock.Verify(helper => helper.SendMessageAsync(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task UnsubscribePairTradesTest()
    {
        var pair = "tBTCUSD";

        _webSocketClient.UnsubscribePairTrades(pair);

        _webSocketHelperMock.Verify(helper => helper.SendMessageAsync(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task SubscribeCurrencyTradesTest()
    {
        var currency = "fBTC";
        var maxCount = 100;

        _webSocketClient.SubscribeCurrencyTrades(currency, maxCount);

        _webSocketHelperMock.Verify(helper => helper.SendMessageAsync(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task UnsubscribeCurrencyTradesTest()
    {
        var currency = "fBTC";

        _webSocketClient.UnsubscribeCurrencyTrades(currency);

        _webSocketHelperMock.Verify(helper => helper.SendMessageAsync(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task SubscribeCandlesTest()
    {
        var symbol = "tBTCUSD";
        var periodInSec = 60;
        var from = DateTimeOffset.UtcNow.AddHours(-1);
        var to = DateTimeOffset.UtcNow;
        long count = 100;

        _webSocketClient.SubscribeCandles(symbol, periodInSec, from, to, count);

        _webSocketHelperMock.Verify(helper => helper.SendMessageAsync(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public async Task UnsubscribeCandlesTest()
    {
        var symbol = "tBTCUSD";

        _webSocketClient.UnsubscribeCandles(symbol);

        _webSocketHelperMock.Verify(helper => helper.SendMessageAsync(It.IsAny<object>()), Times.Once);
    }

    [Fact]
    public void OnMessageReceived_NewBuyPairTradeEvent_Test()
    {
        var pairTrade = new PairTrade
        {
            Symbol = "tBTCUSD",
            Price = 50000m,
            Amount = 0.1m,
            TimestampMs = 253402300,
            Id = 123
        };

        var message = JsonConvert.SerializeObject(new List<PairTrade> { pairTrade });

        bool eventTriggered = false;
        _webSocketClient.NewBuyPairTrade += p =>
        {
            eventTriggered = true;
            Assert.InRange(p.Amount - pairTrade.Amount, -0.0001m, 0.0001m);
            Assert.Equal(pairTrade.Id, p.Id);
            Assert.InRange(p.Price - pairTrade.Price, -0.0001m, 0.0001m);
            Assert.Equal(pairTrade.Side, p.Side);
            Assert.Equal(pairTrade.Symbol.Substring(1), p.Symbol);
        };

        _webSocketClient.OnMessageReceived(message);

        Assert.True(eventTriggered);
    }

    [Fact]
    public void OnMessageReceived_NewSellPairTradeEvent_Test()
    {
        var pairTrade = new PairTrade
        {
            Symbol = "tBTCUSD",
            Price = 50000m,
            Amount = -0.1m,
            TimestampMs = 253402300,
            Id = 123
        };

        var message = JsonConvert.SerializeObject(new List<PairTrade> { pairTrade });

        bool eventTriggered = false;
        _webSocketClient.NewSellPairTrade += p =>
        {
            eventTriggered = true;
            Assert.Equal(pairTrade.Amount, p.Amount);
            Assert.Equal(pairTrade.Id, p.Id);
            Assert.Equal(pairTrade.Price, p.Price);
            Assert.Equal(pairTrade.Side, p.Side);
            Assert.Equal(pairTrade.Symbol.Substring(1), p.Symbol);
        };

        _webSocketClient.OnMessageReceived(message);

        Assert.True(eventTriggered);
    }

    [Fact]
    public void OnMessageReceived_NewBuyCurrencyTradeEvent_Test()
    {
        var currencyTrade = new CurrencyTrade
        {
            Symbol = "fUSD",
            Rate = 20000m,
            Amount = 1000m,
            TimestampMs = 253402300,
            Id = 123,
            Period = 60
        };

        var message = JsonConvert.SerializeObject(new List<CurrencyTrade> { currencyTrade });

        bool eventTriggered = false;
        _webSocketClient.NewBuyCurrencyTrade += p =>
        {
            eventTriggered = true;
            Assert.InRange(p.Amount - currencyTrade.Amount, -0.0001m, 0.0001m);
            Assert.Equal(currencyTrade.Id, p.Id);
            Assert.InRange(p.Rate - currencyTrade.Rate, -0.0001m, 0.0001m);
            Assert.Equal(currencyTrade.Side, p.Side);
            Assert.Equal(currencyTrade.Symbol.Substring(1), p.Symbol);
            Assert.Equal(currencyTrade.Period, p.Period);
        };

        _webSocketClient.OnMessageReceived(message);

        Assert.True(eventTriggered);
    }

    [Fact]
    public void OnMessageReceived_NewSellCurrencyTradeEvent_Test()
    {
        var currencyTrade = new CurrencyTrade
        {
            Symbol = "fUSD",
            Rate = 20000m,
            Amount = -1000m,
            TimestampMs = 253402300,
            Id = 123,
            Period = 60
        };

        var message = JsonConvert.SerializeObject(new List<CurrencyTrade> { currencyTrade });

        bool eventTriggered = false;
        _webSocketClient.NewSellCurrencyTrade += p =>
        {
            eventTriggered = true;
            Assert.InRange(p.Amount - currencyTrade.Amount, -0.0001m, 0.0001m);
            Assert.Equal(currencyTrade.Id, p.Id);
            Assert.InRange(p.Rate - currencyTrade.Rate, -0.0001m, 0.0001m);
            Assert.Equal(currencyTrade.Side, p.Side);
            Assert.Equal(currencyTrade.Symbol.Substring(1), p.Symbol);
            Assert.Equal(currencyTrade.Period, p.Period);
        };

        _webSocketClient.OnMessageReceived(message);

        Assert.True(eventTriggered);
    }

    [Fact]
    public void OnMessageReceived_CandleSeriesProcessingEvent_Test()
    {
        var candle = new Candle
        {
            Symbol = "tBTCUSD",
            TimestampMs = 253402300,
            OpenPrice = 50000,
            ClosePrice = 50500,
            HighPrice = 51000,
            LowPrice = 49500,
            Volume = 1000M
        };

        var message = JsonConvert.SerializeObject(new List<Candle> { candle });

        var eventTriggered = false;

        _webSocketClient.CandleSeriesProcessing += (c) =>
        {
            eventTriggered = true;
            Assert.Equal(candle.Symbol.Substring(1), c.Symbol);
            Assert.Equal(candle.TimestampMs, c.TimestampMs);
            Assert.Equal(candle.OpenPrice, c.OpenPrice);
            Assert.Equal(candle.ClosePrice, c.ClosePrice);
            Assert.Equal(candle.HighPrice, c.HighPrice);
            Assert.Equal(candle.LowPrice, c.LowPrice);
            Assert.Equal(candle.Volume, c.Volume);
        };

        _webSocketClient.OnMessageReceived(message);

        Assert.True(eventTriggered);
       
    }

    [Fact]
    public async Task ConnectAsyncTest()
    {
        await _webSocketClient.ConnectAsync();

        _webSocketHelperMock.Verify(helper => helper.ConnectAsync(), Times.Once);
    }

    [Fact]
    public async Task DisconnectAsyncTest()
    {
        await _webSocketClient.DisconnectAsync();

        _webSocketHelperMock.Verify(helper => helper.DisconnectAsync(), Times.Once);
    }
}
