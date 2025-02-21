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
    public decimal Rate { get; set; }

    /// <summary>
    /// Количество времени, в течение которого был произведен трейд
    /// </summary>
    public int Period { get; set; }

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