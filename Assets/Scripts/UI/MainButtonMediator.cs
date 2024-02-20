using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Common;
using Cysharp.Threading.Tasks;
using RestAPI.Service;

namespace UI
{
    public class MainButtonMediator : MonoBehaviour
    {
        private IHttpClient _httpClient;
        private IButtonsRepository _buttonsRepository;
        private MainButtonView _mainButtonView;
        private InfoButtonFactory _buttonFactory;
        private PopViewUI _popViewUI;
        private ErrorPopUp _errorPopup;
        
        private Dictionary<int, InfoButtonViewUI> _buttonDict = new();
        private PopupConfig _deletePopupInfo;
        private PopupConfig _updatePopupInfo;
        private PopupConfig _refreshPopupInfo;

        [Inject]
        public void Construct(IHttpClient httpClient, IButtonsRepository buttonsRepository,
            MainButtonView mainButtonView, InfoButtonFactory buttonFactory, PopViewUI popViewUI, ErrorPopUp errorPopUp)
        {
            _httpClient = httpClient;
            _buttonsRepository = buttonsRepository;
            _mainButtonView = mainButtonView;
            _buttonFactory = buttonFactory;
            _popViewUI = popViewUI;
            _errorPopup = errorPopUp;
        }

        private void InitUIText()
        {
            _deletePopupInfo = new PopupConfig("Delete Button", "Enter the ID of the button you want to delete:");
            _updatePopupInfo = new PopupConfig("Update Button", "Enter the ID of the button you want to update:");
            _refreshPopupInfo = new PopupConfig("Refresh Button",
                "Enter the ID of the button if you want to update specific button, else will be refreshed all buttons:");
            _refreshPopupInfo.SetConfirmText("Specific");
            _refreshPopupInfo.SetCancelText("All");
        }

        private void Start()
        {
            RefreshInfoButton();
            
            _mainButtonView.SetCreateButtonCallback(CreateInfoButton);
            _mainButtonView.SetDeleteButtonCallback( () => _popViewUI.ShowPopup(_deletePopupInfo,DeleteInfoButton));
            _mainButtonView.SetUpdateButtonCallback(() => _popViewUI.ShowPopup(_updatePopupInfo, UpdateInfoButton));
            _mainButtonView.SetRefreshButtonCallback(() =>  _popViewUI.ShowPopup(_refreshPopupInfo, RefreshInfoButtonById, RefreshInfoButton));

            InitUIText();
        }

        private async void CreateInfoButton()
        {
            var button = await _httpClient.CreateButton();
            button.AnimationType = true;
            var buttonObject = _buttonFactory.CreateButtonViewUI(button, gameObject.transform);
            _buttonDict.Add(button.Id, buttonObject);
            
            buttonObject.PlayButtonAnimation(button);
        }

        private async void DeleteInfoButton(int buttonId)
        {
            var id = buttonId.ToString();
            if (!_buttonDict.ContainsKey(buttonId))
            {
                OpenErrorPopUp(id);
                return;
            }
            
            var button = await _httpClient.GetButtonById(id);
            await _httpClient.DeleteButton(id);
            _buttonsRepository.RemoveButton(button);

            if (!_buttonDict.ContainsKey(button.Id)) return;
            Destroy(_buttonDict[button.Id].gameObject);
            _buttonDict.Remove(button.Id);
        }

        private async void UpdateInfoButton(int buttonId)
        {
            var id = buttonId.ToString();
            if (!_buttonDict.ContainsKey(buttonId))
            {
                OpenErrorPopUp(id);
                return;
            }
            
            var button = await _httpClient.GetButtonById(id);
            RandomColor(button);
            _buttonsRepository.UpdateButton(button);
            _buttonDict[buttonId].SetButtonViewData(button);
            await _httpClient.UpdateButton(button);
            

        }

        private async void RefreshInfoButtonById(int buttonId)
        {
            var id = buttonId.ToString();
         
            if (_buttonDict.TryGetValue(buttonId, out var value))
            {
                value.gameObject.SetActive(false);
            }
            else
            {
                OpenErrorPopUp(id);
                return;
            }
            var button = await _httpClient.GetButtonById(id);
            _buttonsRepository.UpdateButton(button);
            _buttonDict[buttonId].gameObject.SetActive(true);
        }
        
        private async void RefreshInfoButton()
        {
            var buttonsData = await _httpClient.GetButtonsCollection();
            foreach (var button in buttonsData) _buttonsRepository.RemoveButton(button);
            foreach (var buttonObject in _buttonDict.Values) Destroy(buttonObject.gameObject);
            _buttonDict.Clear();
            
            await UniTask.Delay(1000);
    
            foreach (var button in buttonsData)
            {
                var buttonObject = _buttonFactory.CreateButtonViewUI(button, gameObject.transform);
                _buttonDict.TryAdd(button.Id, buttonObject);
            }
        }

        private void OpenErrorPopUp(string id)
        {
            _errorPopup.OpenPopUp(id);
        }
        private static void RandomColor(ButtonData button)
        {
            var randomH = Random.Range(0f, 1f);
            var randomS = Random.Range(0.5f, 1f);
            var randomL = Random.Range(0.2f, 0.8f);
            var newColor = new[] { randomH, randomS, randomL };

            button.Color = newColor;
        }
    }
}
