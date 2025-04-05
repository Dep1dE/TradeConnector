using Moq;
using TradeConnector.Core.Connectors.Interfaces;
using TradeConnector.Core.Models;
using TradeConnector.Core.Services;

namespace TradeConnector.Tests.Integration;

public class PortfolioCalculatorTests
{
    private readonly Mock<IConnector> _connectorMock;
    private readonly PortfolioCalculator _calculator;

    public PortfolioCalculatorTests()
    {
        _connectorMock = new Mock<IConnector>();
        _calculator = new PortfolioCalculator(_connectorMock.Object);
    }

    [Fact]
    public async Task GetTotalBalanceAsyncTest()
    {
        var baseCurrency = "USD";

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tBTCUSD"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 50000m } });

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tXRPUSD"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 0.5m } });

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tXMRUSD"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 150m } });

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tDASHUSD"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 100m } });

        var totalBalance = await _calculator.GetTotalBalanceAsync(baseCurrency);

        // BTC: 1 * 50000 = 50000
        // XRP: 15000 * 0.5 = 7500
        // XMR: 50 * 150 = 7500
        // DASH: 30 * 100 = 3000
        // Total = 68000
        Assert.Equal(68000m, totalBalance);
    }

    [Fact]
    public async Task GetTotalBalanceAsyncTest_RandomBaseCurrency()
    {
        var baseCurrency = "EUR";

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tBTCEUR"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 40000m } });

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tXRPEUR"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 0.4m } });

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tXMREUR"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 100m } });

        _connectorMock.Setup(x => x.GetNewPairTickerAsync("tDASHEUR"))
            .ReturnsAsync(new List<PairTicker> { new() { LastPrice = 80m } });

        var totalBalance = await _calculator.GetTotalBalanceAsync(baseCurrency);

        // BTC: 1 * 40000 = 40000
        // XRP: 15000 * 0.4 = 6000
        // XMR: 50 * 100 = 5000
        // DASH: 30 * 80 = 2400
        // Total = 53400
        Assert.Equal(53400m, totalBalance);
    }
}
