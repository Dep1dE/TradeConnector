namespace TradeConnector.Core.Models;

/// <summary>
/// For trading pairs, ex. tBTCUSD
/// </summary>
public class PairTicker
{
    public string Symbol { get; set; } = string.Empty; // Cимвольное обозначение валютной пары

    /// <summary>
    /// Цена последней самой высокой ставки покупки(BID)
    /// </summary>
    public decimal Bid { get; set; }

    /// <summary>
    /// Сумма из 25 самых высоких размеров ставок покупок(BID)
    /// </summary>
    public decimal BidSize { get; set; }

    /// <summary>
    /// Цена последней самой низкой ставки продажи(ASK)
    /// </summary>
    public decimal Ask { get; set; }

    /// <summary>
    /// Сумма из 25 самых низких размеров ставок продаж(ASK)
    /// </summary>
    public decimal AskSize { get; set; }

    /// <summary>
    /// НА СКОЛЬКО изменилась последняя цена со вчерашнего дня
    /// </summary>
    public decimal DailyChange { get; set; }

    /// <summary>
    /// Относительное изменение цены со вчерашнего дня (*100 для процентного изменения)
    /// </summary>
    public decimal DaileChangeRelative { get; set; }

    /// <summary>
    /// Цена последней ставки
    /// </summary>
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Ежедневный максимум
    /// </summary>
    public decimal High { get; set; }

    /// <summary>
    /// Ежедневный минимум
    /// </summary>
    public decimal Low { get; set; }

    /// <summary>
    /// Ежедневный объем
    /// </summary>
    public decimal Volume { get; set; }

}

