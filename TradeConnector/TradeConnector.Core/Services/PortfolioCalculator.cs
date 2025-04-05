using TradeConnector.Core.Connectors.Interfaces;

namespace TradeConnector.Core.Services;

public class PortfolioCalculator
{
    private readonly IConnector _connector;

    private readonly Dictionary<string, decimal> _portfolio = new()
        {
            {"BTC", 1},
            {"XRP", 15000},
            {"XMR", 50},
            {"DASH", 30}
        };

    public PortfolioCalculator(IConnector connector)
    {
        _connector = connector;
    }

    public async Task<decimal> GetTotalBalanceAsync(string baseCurrency)
    {
        decimal totalBalance = 0;

        var btcToBaseCurrency = await GetExchangeRateAsync("BTC", baseCurrency);
        var xrpToBaseCurrency = await GetExchangeRateAsync("XRP", baseCurrency);
        var xmrToBaseCurrency = await GetExchangeRateAsync("XMR", baseCurrency);
        var dashToBaseCurrency = await GetExchangeRateAsync("DASH", baseCurrency);

        totalBalance += _portfolio["BTC"] * btcToBaseCurrency;
        totalBalance += _portfolio["XRP"] * xrpToBaseCurrency;
        totalBalance += _portfolio["XMR"] * xmrToBaseCurrency;
        totalBalance += _portfolio["DASH"] * dashToBaseCurrency;

        return totalBalance;
    }

    private async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
    {
        string pair = $"t{fromCurrency}{toCurrency}";
        var ticker = await _connector.GetNewPairTickerAsync(pair);

        return ticker.First().LastPrice;
    }
}
