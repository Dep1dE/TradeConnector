namespace TradeConnector.Core.Models;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class JsonArrayIndex : Attribute
{
    public int Index { get; }

    public JsonArrayIndex(int index)
    {
        Index = index;
    }
}
