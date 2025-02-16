using System.ComponentModel.Design;

namespace TradeConnector.Core.Models;

public class Ticker
{
    public string Pair { get; set; } = string.Empty;

    /// <summary>
    /// Цена последней самой высокой ставки
    /// </summary>
    public decimal LastHighPrice { get; set; }

    /// <summary>
    /// Сумма из 25 самых высоких размеров ставок
    /// </summary>
    public decimal HighSumPrice { get; set; }

    /// <summary>
    /// Цена последней самой низкой ставки
    /// </summary>
    public decimal LastLowPrice { get; set; }   

    /// <summary>
    /// Сумма из 25 самых низких размеров ставок
    /// </summary>
    public decimal LowSumPrice { get; set; }


    /// <summary>
    /// НА СКОЛЬКО изменилась последняя цена со вчерашнего дня
    /// </summary>
    public decimal AverageDailyChange { get; set; }

    /// <summary>
    /// Относительное изменение цены со вчерашнего дня (*100 для процентного изменения)
    /// </summary>
    public decimal DaileChangeRelativite { get; set; }

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

    /// <summary>
    /// Время
    /// </summary>
    public DateTimeOffset OpenTime { get; set; }
}

