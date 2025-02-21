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
    public int OpenPrice { get; set; }

    /// <summary>
    /// Цена закрытия
    /// </summary>
    public int ClosePrice { get; set; }

    /// <summary>
    /// Максимальная цена
    /// </summary>
    public int HighPrice { get; set; }

    /// <summary>
    /// Минимальная цена
    /// </summary>
    public int LowPrice { get; set; }

    /// <summary>
    /// Объём символов, торгуемых в течение таймфрейма
    /// </summary>
    public decimal Volume { get; set; }

    /// <summary>
    /// Время
    /// </summary>
    public DateTimeOffset OpenTime { get; set; }
}

/// <summary>
/// Стандартный класс модели (Candle) из примера подходит как для работы с валютной парой,  
/// так и для работы с отдельными валютами (currency symbols, ex. fUSD). Что было изменено,
/// — это название свойства Pair на Symbol, также убрано свойство TotalPrice, ибо нельзя 
/// вычислить общую сумму сделок, так как с API можно получить только минимальную и максимальную 
/// цену.
/// </summary>

