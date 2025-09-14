using System.Text.Json;
using System.Text.Json.Serialization;

namespace MidiHueInterface.App.Extensions;

public static class ObjectExtensions
{
    public static string ToJson<T>(this T obj, bool pretty = false)
    {
        var options = pretty ? PrettyPrintJsonOptions : JsonOptions;
        
        return JsonSerializer.Serialize(obj, options);
    }

    private static JsonSerializerOptions PrettyPrintJsonOptions => new ()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        WriteIndented = true
    };

    private static JsonSerializerOptions JsonOptions => new ()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
}