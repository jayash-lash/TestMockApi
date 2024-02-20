using Newtonsoft.Json;

namespace RestAPI.Service
{
    public class JsonNetSerializer : ISerializer
    {
        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}