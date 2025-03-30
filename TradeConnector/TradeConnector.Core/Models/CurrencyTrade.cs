namespace TradeConnector.Core.Models;

/// <summary>
/// For funding currency symbols (ex. fUSD)
/// </summary>
public class CurrencyTrade
{
    public string Symbol { get; set; } = string.Empty; // Символьное обозначение валюты

    /// <summary>
    /// Ставка, по которой произошел трейд
    /// </summary>
    [JsonArrayIndex(3)]
    public decimal Rate { get; set; }

    /// <summary>
    /// Количество времени, в течение которого был произведен трейд
    /// </summary>
    [JsonArrayIndex(4)]
    public int Period { get; set; }

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