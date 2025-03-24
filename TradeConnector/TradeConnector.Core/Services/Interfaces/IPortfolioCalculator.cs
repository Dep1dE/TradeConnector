namespace TradeConnector.Core.Services.Interfaces;

public interface IPortfolioCalculator
{
    Task<Decimal> GetTotalBalanceAsync(string baseCurrency);
}
