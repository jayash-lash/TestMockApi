using Common;
using Cysharp.Threading.Tasks;

namespace RestAPI.Service
{
    public interface IHttpClient
    {
        UniTask<ButtonData[]> GetButtonsCollection();
        UniTask<ButtonData> GetButtonById(string id);
        UniTask<ButtonData> CreateButton(ButtonData button = null);
        UniTask<ButtonData> UpdateButton(ButtonData button);
        UniTask DeleteButton(string id);
    }
}