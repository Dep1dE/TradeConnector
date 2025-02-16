namespace TradeConnector.Core.Models;

public class Trade
{
    public string Pair { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public string Side { get; set; } = string.Empty;    
    public DateTimeOffset Time { get; set; }
    public Guid Id { get; set; }

}
