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
    [JsonArrayIndex(3)]
    public decimal Price { get; set; }

    /// <summary>
    /// Объем трейда
    /// </summary>
    [JsonArrayIndex(2)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Направление (buy/sell)
    /// </summary>
    public string Side => Amount > 0 ? "buy" : "sell";

    /// <summary>
    /// Время трейда
    /// </summary>
    [JsonArrayIndex(1)]
    public long TimestampMs { get; set; }
    public DateTimeOffset Time => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);
    // Преобразование миллисекунд в DateTimeOffset

    /// <summary>
    /// Id трейда
    /// </summary>
    [JsonArrayIndex(0)]
    public int Id { get; set; }
}

/// <summary>
/// Мне не хватило стандартного класса модели (Trade) из примера, так как данная модель  
/// предназначена только для работы с валютной парой. Однако мне хочется получать информацию  
/// также и об отдельных валютах (currency symbols, ex. fUSD), для чего была создана модель  
/// CurrencyTrade.  
/// </summary>


