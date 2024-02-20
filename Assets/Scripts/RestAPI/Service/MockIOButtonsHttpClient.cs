using Common;
using Cysharp.Threading.Tasks;
using Zenject;

namespace RestAPI.Service
{
    public class MockIOButtonsHttpClient : IHttpClient
    {
        [Inject] private ITransport _transport;
        [Inject] private ISerializer _serializer;
    
        private const string PROJECT_TOKEN = "65d3c2de522627d501098407";
        private const string COLLECTION_NAME = "ButtonCollection";
    
        private static readonly string SERVICE_URL = $"https://{PROJECT_TOKEN}.mockapi.io/{COLLECTION_NAME}";
     
        public async UniTask<ButtonData[]> GetButtonsCollection()
        {
            var endpoint = SERVICE_URL;
            var result = await _transport.Get(endpoint);
            return _serializer.Deserialize<ButtonData[]>(result);
        }

        public async UniTask<ButtonData> GetButtonById(string id)
        {
            var endpoint = SERVICE_URL + $"/{id}";
            var result = await _transport.Get(endpoint);
            return _serializer.Deserialize<ButtonData>(result);
        }

        public async UniTask<ButtonData> CreateButton(ButtonData button = null)
        {
            var endpoint = SERVICE_URL;

            string result;
            if (button != null)
            {
                result = await _transport.Post(endpoint, button);
                return _serializer.Deserialize<ButtonData>(result);
            }
        
            result = await _transport.Post<ButtonData>(endpoint);
            return _serializer.Deserialize<ButtonData>(result);
        }

        public async UniTask<ButtonData> UpdateButton(ButtonData button)
        {
            var endpoint = SERVICE_URL + $"/{button.Id}";
            var result = await _transport.Put(endpoint, button);
            return _serializer.Deserialize<ButtonData>(result);
        }

        public async UniTask DeleteButton(string id)
        {
            var endpoint = SERVICE_URL + $"/{id}";
            await _transport.Delete(endpoint);
        }
    }
}