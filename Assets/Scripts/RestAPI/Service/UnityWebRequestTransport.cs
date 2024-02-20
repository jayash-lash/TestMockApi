using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace RestAPI.Service
{
    public class UnityWebRequestTransport : ITransport
    {
        public async UniTask<string> Get(string endpoint)
        {
            var request = UnityWebRequest.Get(endpoint);
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }

        public async UniTask<string> Post<T>(string endpoint, T data = null) where T : class
        {
            var request = new UnityWebRequest(endpoint, "POST");
            var json = data == null ? string.Empty : JsonConvert.SerializeObject(data);
            var bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }

        public async UniTask<string> Put<T>(string endpoint, T data = null) where T : class
        {
            var request = new UnityWebRequest(endpoint, "PUT");
            var json = data == null ? string.Empty : JsonConvert.SerializeObject(data);
            var bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }

        public async UniTask Delete(string endpoint)
        {
            var request = new UnityWebRequest(endpoint, "DELETE");
            await request.SendWebRequest();
        }
    }
}