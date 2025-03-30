using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using TradeConnector.Core.Models;

namespace TradeConnector.Core.Infrastructure;

public class ArrayToObjectConverter<T> : JsonConverter where T : new()
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(T);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var instance = new T();
        var properties = typeof(T).GetProperties()
            .Select(p => new
            {
                Property = p,
                Index = p.GetCustomAttribute<JsonArrayIndex>()?.Index
            })
            .Where(x => x.Index.HasValue)
            .ToList();

        var array = JArray.Load(reader);

        foreach (var prop in properties)
        {
            if (prop.Index.Value < array.Count)
            {
                var value = array[prop.Index.Value].ToObject(prop.Property.PropertyType);
                prop.Property.SetValue(instance, value);
            }
        }

        return instance;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
