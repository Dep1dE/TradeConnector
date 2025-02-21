namespace TradeConnector.Core.Models;

/// <summary>
/// For trading pair symbols (ex. tBTCUSD)
/// </summary>
public class PairTrade 
{
    public string Symbol { get; set; } = string.Empty; // Cимвольное обозначение валютной пары

    /// <summary>
    /// Цена трейда
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Объем трейда
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Направление (buy/sell)
    /// </summary>
    public string Side { get; set; } = string.Empty;

    /// <summary>
    /// Время трейда
    /// </summary>
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// Id трейда
    /// </summary>
    public Guid Id { get; set; }
}

/// <summary>
/// Мне не хватило стандартного класса модели (Trade) из примера, так как данная модель  
/// предназначена только для работы с валютной парой. Однако мне хочется получать информацию  
/// также и об отдельных валютах (currency symbols, ex. fUSD), для чего была создана модель  
/// CurrencyTrade.  
/// </summary>


