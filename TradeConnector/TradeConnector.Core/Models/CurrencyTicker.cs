namespace TradeConnector.Core.Models;

/// <summary>
/// For funding currencies, ex. fUSD
/// </summary>
public class CurrencyTicker
{
    [JsonArrayIndex(0)]
    public string Symbol { get; set; } = string.Empty; // Cимвольное обозначение валюты

    /// <summary>
    /// FRR — средняя ставка по всем фиксированным ставкам финансирования за последний час
    /// </summary>
    [JsonArrayIndex(1)]
    public decimal Frr { get; set; }

    /// <summary>
    /// Цена последней самой высокой ставки покупки(BID)
    /// </summary>
    [JsonArrayIndex(2)]
    public decimal Bid { get; set; }

    /// <summary>
    /// Сумма из 25 самых высоких размеров ставок покупок(BID)
    /// </summary>
    [JsonArrayIndex(4)]
    public decimal BidSize { get; set; }

    /// <summary>
    /// Период покупки(BID), охваченный в днях
    /// </summary>
    [JsonArrayIndex(3)]
    public int BidPeriod { get; set; }

    /// <summary>
    /// Цена последней самой низкой ставки продажи(ASK)
    /// </summary>
    [JsonArrayIndex(5)]
    public decimal Ask { get; set; }

    /// <summary>
    /// Сумма из 25 самых низких размеров ставок продаж(ASK)
    /// </summary>
    [JsonArrayIndex(7)]
    public decimal AskSize { get; set; }

    /// <summary>
    /// Период продажи(ASK), охваченный в днях
    /// </summary>
    [JsonArrayIndex(6)]
    public int AskPeriod { get; set; }

    /// <summary>
    /// НА СКОЛЬКО изменилась последняя цена со вчерашнего дня
    /// </summary>
    [JsonArrayIndex(8)]
    public decimal DailyChange { get; set; }

    /// <summary>
    /// Относительное изменение цены со вчерашнего дня (*100 для процентного изменения)
    /// </summary>
    [JsonArrayIndex(9)]
    public decimal DaileChangePerc { get; set; }

    /// <summary>
    /// Цена последней ставки
    /// </summary>
    [JsonArrayIndex(10)]
    public decimal LastPrice { get; set; }

    /// <summary>
    /// Ежедневный максимум
    /// </summary>
    [JsonArrayIndex(12)]
    public decimal High { get; set; }

    /// <summary>
    /// Ежедневный минимум
    /// </summary>
    [JsonArrayIndex(13)]
    public decimal Low { get; set; }

    /// <summary>
    /// Ежедневный объем
    /// </summary>
    [JsonArrayIndex(11)]
    public decimal Volume { get; set; }

    /// <summary>
    /// Сумма финансирования, доступная по ставке мгновенного возврата
    /// </summary>
    [JsonArrayIndex(14)]
    public decimal FrrAmountAvailable { get; set; }

}
