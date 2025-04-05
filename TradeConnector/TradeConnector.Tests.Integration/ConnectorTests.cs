using Moq;
using TradeConnector.Core.Clients.Interfaces;
using TradeConnector.Core.Clients.Interfaces.Bitfinex;
using TradeConnector.Core.Connectors;
using TradeConnector.Core.Models;

namespace TradeConnector.Tests.Integration;

public class ConnectorTests
{
    private readonly Mock<IRestClient> _restClientMock;
    private readonly Mock<IWebSocketClient> _webSocketClientMock;
    private readonly Connector _connector;

    public ConnectorTests()
    {
        _restClientMock = new Mock<IRestClient>();
        _webSocketClientMock = new Mock<IWebSocketClient>();
        _connector = new Connector(_restClientMock.Object, _webSocketClientMock.Object);
    }

    [Fact]
    public async Task GetNewPairTradesAsyncTest()
    {
        var expected = new List<PairTrade> { new() { Symbol = "BTCUSD" } };
        _restClientMock.Setup(x => x.GetPairTradesAsync("BTCUSD", 10)).ReturnsAsync(expected);

        var result = await _connector.GetNewPairTradesAsync("BTCUSD", 10);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetNewCurrencyTradesAsyncTest()
    {
        var expected = new List<CurrencyTrade> { new() { Symbol = "BTC" } };
        _restClientMock.Setup(x => x.GetCurrencyTradesAsync("BTC", 10)).ReturnsAsync(expected);

        var result = await _connector.GetNewCurrencyTradesAsync("BTC", 10);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetCandleSeriesAsyncTest()
    {
        var expected = new List<Candle> { new() { Symbol = "BTCUSD" } };
        _restClientMock.Setup(x => x.GetCandleSeriesAsync("BTCUSD", 1, null, null, 0)).ReturnsAsync(expected);

        var result = await _connector.GetCandleSeriesAsync("BTCUSD", 1, null);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetNewPairTickerAsyncTest()
    {
        var expected = new List<PairTicker> { new() { Symbol = "BTCUSD" } };
        _restClientMock.Setup(x => x.GetPairTickerAsync("BTCUSD")).ReturnsAsync(expected);

        var result = await _connector.GetNewPairTickerAsync("BTCUSD");

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetNewCurrencyTickerAsyncTest()
    {
        var expected = new List<CurrencyTicker> { new() { Symbol = "BTC" } };
        _restClientMock.Setup(x => x.GetCurrencyTickerAsync("BTC")).ReturnsAsync(expected);

        var result = await _connector.GetNewCurrencyTickerAsync("BTC");

        Assert.Equal(expected, result);
    }

    [Fact]
    public void SubscribePairTradesTest()
    {
        _connector.SubscribePairTrades("BTCUSD", 20);

        _webSocketClientMock.Verify(x => x.SubscribePairTrades("BTCUSD", 20), Times.Once);
    }

    [Fact]
    public void UnsubscribePairTradesTest()
    {
        _connector.UnsubscribePairTrades("BTCUSD");

        _webSocketClientMock.Verify(x => x.UnsubscribePairTrades("BTCUSD"), Times.Once);
    }

    [Fact]
    public void SubscribeCurrencyTradesTest()
    {
        _connector.SubscribeCurrencyTrades("BTC", 50);

        _webSocketClientMock.Verify(x => x.SubscribeCurrencyTrades("BTC", 50), Times.Once);
    }

    [Fact]
    public void UnsubscribeCurrencyTradesTest()
    {
        _connector.UnsubscribeCurrencyTrades("BTC");

        _webSocketClientMock.Verify(x => x.UnsubscribeCurrencyTrades("BTC"), Times.Once);
    }

    [Fact]
    public void SubscribeCandlesTest()
    {
        _connector.SubscribeCandles("BTCUSD", 60);

        _webSocketClientMock.Verify(x => x.SubscribeCandles("BTCUSD", 60, null, null, 0), Times.Once);
    }

    [Fact]
    public void UnsubscribeCandlesTest()
    {
        _connector.UnsubscribeCandles("BTCUSD");

        _webSocketClientMock.Verify(x => x.UnsubscribeCandles("BTCUSD"), Times.Once);
    }

    [Fact]
    public async Task ConnectAsyncTest()
    {
        await _connector.ConnectAsync();

        _webSocketClientMock.Verify(x => x.ConnectAsync(), Times.Once);
    }

    [Fact]
    public async Task DisconnectAsyncTest()
    {
        await _connector.DisconnectAsync();

        _webSocketClientMock.Verify(x => x.DisconnectAsync(), Times.Once);
    }

    [Fact]
    public void NewBuyPairTradeTest()
    {
        var trade = new PairTrade();
        PairTrade result = null;
        _connector.NewBuyPairTrade += t => result = t;

        _webSocketClientMock.Raise(m => m.NewBuyPairTrade += null, trade);

        Assert.Equal(trade, result);
    }

    [Fact]
    public void NewSellPairTradeTest()
    {
        var trade = new PairTrade();
        PairTrade result = null;
        _connector.NewSellPairTrade += t => result = t;

        _webSocketClientMock.Raise(m => m.NewSellPairTrade += null, trade);

        Assert.Equal(trade, result);
    }

    [Fact]
    public void NewBuyCurrencyTradeTest()
    {
        var trade = new CurrencyTrade();
        CurrencyTrade result = null;
        _connector.NewBuyCurrencyTrade += t => result = t;

        _webSocketClientMock.Raise(m => m.NewBuyCurrencyTrade += null, trade);

        Assert.Equal(trade, result);
    }

    [Fact]
    public void NewSellCurrencyTradeTest()
    {
        var trade = new CurrencyTrade();
        CurrencyTrade result = null;
        _connector.NewSellCurrencyTrade += t => result = t;

        _webSocketClientMock.Raise(m => m.NewSellCurrencyTrade += null, trade);

        Assert.Equal(trade, result);
    }

    [Fact]
    public void CandleSeriesProcessingTest()
    {
        var candle = new Candle();
        Candle result = null;
        _connector.CandleSeriesProcessing += c => result = c;

        _webSocketClientMock.Raise(m => m.CandleSeriesProcessing += null, candle);

        Assert.Equal(candle, result);
    }
}
