namespace TradeConnector.Core.Models;

/// <summary>
/// For trading pairs, ex. tBTCUSD
/// </summary>
public class PairTicker
{
    [JsonArrayIndex(0)]
    public string Symbol { get; set; } = string.Empty; // Cимвольное обозначение валютной пары

    /// <summary>
    /// Цена последней самой высокой ставки покупки(BID)
    /// </summary>
    [JsonArrayIndex(1)]
    public decimal Bid { get; set; }

    /// <summary>
    /// Сумма из 25 самых высоких размеров ставок покупок(BID)
    /// </summary>
    [JsonArrayIndex(2)]
    public decimal BidSize { get; set; }

    /// <summary>
    /// Цена последней самой низкой ставки продажи(ASK)
    /// </summary>
    [JsonArrayIndex(3)]
    public decimal Ask { get; set; }

    /// <summary>
    /// Сумма из 25 самых низких размеров ставок продаж(ASK)
    /// </summary>
    [JsonArrayIndex(4)]
    public decimal AskSize { get; set; }

    /// <summary>
    /// НА СКОЛЬКО изменилась последняя цена со вчерашнего дня
    /// </summary>
    [JsonArrayIndex(5)]
    public decimal DailyChange { get; set; }

    /// <summary>
    /// Относительное изменение цены со вчерашнего дня (*100 для процентного изменения)
    /// </summary>
    [JsonArrayIndex(6)]
    public decimal DaileChangeRelative { get; set; }

    /// <summary>
    /// Цена последней ставки
    /// </summary>
    [JsonArrayIndex(7)]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Ежедневный максимум
    /// </summary>
    [JsonArrayIndex(9)]
    public decimal High { get; set; }

    /// <summary>
    /// Ежедневный минимум
    /// </summary>
    [JsonArrayIndex(10)]
    public decimal Low { get; set; }

    /// <summary>
    /// Ежедневный объем
    /// </summary>
    [JsonArrayIndex(8)]
    public decimal Volume { get; set; }

}

