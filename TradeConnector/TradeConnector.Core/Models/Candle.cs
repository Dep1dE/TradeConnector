﻿namespace TradeConnector.Core.Models;

public class Candle
{
    public string Pair { get; set; } = string.Empty;
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalVolume { get; set; }
    public DateTimeOffset OpenTime { get; set; }
}
