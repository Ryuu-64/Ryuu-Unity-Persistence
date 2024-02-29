using System;
using Newtonsoft.Json;
using Ryuu.Unity.Serialization.Json;

namespace Ryuu.Unity.Persistence
{
    public static class PersistenceUtility
    {
        public static T Read<T>(string path, Func<T> fallbackProvider)
        {
            try
            {
                string jsonString = FileUtility.Read(path);
                return JsonUtility.Deserialize(jsonString, fallbackProvider);
            }
            catch (Exception)
            {
                return fallbackProvider.Invoke();
            }
        }

        public static void Write(string path, object value)
        {
            string jsonString = JsonConvert.SerializeObject(value);
            FileUtility.Write(path, jsonString);
        }
    }
}