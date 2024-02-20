using Cysharp.Threading.Tasks;

namespace RestAPI.Service
{
    public interface ITransport
    {
        UniTask<string> Get(string endpoint);
        UniTask<string> Post<T>(string endpoint, T data = null) where T : class;
        UniTask<string> Put<T>(string endpoint, T data = null) where T : class;
        UniTask Delete(string endpoint);
    }
}