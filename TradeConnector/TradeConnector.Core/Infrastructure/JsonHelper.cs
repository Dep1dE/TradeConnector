using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json;

namespace TradeConnector.Core.Infrastructure;

public static class JsonHelper
{
    public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
    public static List<T> Deserialize<T>(string json) where T : new()
    {
        var settings = new JsonSerializerSettings
        {
            Converters = { new ArrayToObjectConverter<T>() }
        };

        return JsonConvert.DeserializeObject<List<T>>(json, settings);
    }
}
