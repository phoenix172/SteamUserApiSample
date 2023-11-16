
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonSerializeExtensions
{
    public static T? DeserializeAnonymousType<T>(string json, T anonymousTypeObject) => JsonSerializer.Deserialize<T>(json);
}