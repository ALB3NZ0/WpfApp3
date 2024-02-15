using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NoteApp.Utilities
{
    public static class JsonSerializer
    {
        public static void Serialize<T>(List<T> list, string filePath)
        {
            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(filePath, json);
        }

        public static List<T> Deserialize<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
