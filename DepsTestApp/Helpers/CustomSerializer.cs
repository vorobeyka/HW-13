using System.IO;
using System.Text.Json;

namespace DepsTestApp.Helpers
{
    internal static class CustomSerializer
    {
        public static T GetObject<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var result = JsonSerializer.Deserialize<T>(json);
            return result;
        }

        public static string ToJson<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
