namespace TradeConnector.Core.Models;

/// <summary>
/// For funding currency symbols (ex. fUSD) or pair symbols (ex. tBTCUSD)
/// </summary>
public class Candle
{
    public string Symbol { get; set; } = string.Empty; // Символьное обозначение валюты или валютной пары

    /// <summary>
    /// Цена открытия
    /// </summary>
    [JsonArrayIndex(1)]
    public int OpenPrice { get; set; }

    /// <summary>
    /// Цена закрытия
    /// </summary>
    [JsonArrayIndex(2)]
    public int ClosePrice { get; set; }

    /// <summary>
    /// Максимальная цена
    /// </summary>
    [JsonArrayIndex(3)]
    public int HighPrice { get; set; }

    /// <summary>
    /// Минимальная цена
    /// </summary>
    [JsonArrayIndex(4)]
    public int LowPrice { get; set; }

    /// <summary>
    /// Объём символов, торгуемых в течение таймфрейма
    /// </summary>
    [JsonArrayIndex(5)]
    public decimal Volume { get; set; }

    /// <summary>
    /// Время
    /// </summary>
    [JsonArrayIndex(0)]
    public long TimestampMs { get; set; }
    public DateTimeOffset OpenTime => DateTimeOffset.FromUnixTimeSeconds(TimestampMs);
    // Преобразование миллисекунд в DateTimeOffset
}

/// <summary>
/// Стандартный класс модели (Candle) из примера подходит как для работы с валютной парой,  
/// так и для работы с отдельными валютами (currency symbols, ex. fUSD). Что было изменено,
/// — это название свойства Pair на Symbol, также убрано свойство TotalPrice, ибо нельзя 
/// вычислить общую сумму сделок, так как с API можно получить только минимальную и максимальную 
/// цену.
/// </summary>

