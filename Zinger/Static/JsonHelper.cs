using System.IO;
using System.Text.Json;

namespace Zinger.Static
{
    internal static class JsonHelper
    {
        internal static T DeserializeFile<T>(string fileName)
        {
            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<T>(json);
        }

        internal static void SerializeFile<T>(string fileName, T @object)
        {
            var path = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var json = JsonSerializer.Serialize<T>(@object);
            File.WriteAllText(fileName, json);
        }
    }
}
