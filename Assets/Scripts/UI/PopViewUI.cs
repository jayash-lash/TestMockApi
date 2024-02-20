using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Zenject;

namespace UI
{
    public class PopViewUI : MonoBehaviour
    {
        [Inject] private ErrorPopUp _errorPopUp;
        
        [SerializeField] private GameObject _popUp;
        [SerializeField] private GameObject _backFon;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _actionDescription;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TextMeshProUGUI _confirmButtonText;
        [SerializeField] private TextMeshProUGUI _cancelButtonText;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _closePopUpButton;
        [SerializeField] private Button _cancelPopUpButton;

        private Action<int> _confirmAction;
        private Action _cancelAction;

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(ConfirmAction);
            _closePopUpButton.onClick.AddListener(ClosePopUp);
            _cancelPopUpButton.onClick.AddListener(OnCancel);
        }
        

        private void SetPopupText(PopupConfig config)
        {
            _title.text = config.Title;
            _actionDescription.text = config.Description;
        }

        public void ShowPopup(PopupConfig config, Action<int> callback, Action cancelCallback = null)
        {
            _cancelPopUpButton.gameObject.SetActive(cancelCallback != null);
            PlayAnimation(1f);
            _backFon.SetActive(true);
            
            _cancelAction = cancelCallback;
            _cancelButtonText.text = config.CancelBtnText;

            _confirmAction = callback;
            _confirmButtonText.text = config.ConfirmBtnText;
            
            SetPopupText(config);
            gameObject.SetActive(true);
        }

        private void PlayAnimation(float destinationValue)
        {
            var value = destinationValue;
            transform.localScale = Vector3.zero;
            transform.DOScale(value, 0.5f).SetEase(Ease.OutBack);
        }

        private void ConfirmAction()
        {
            if (int.TryParse(_inputField.text, out var buttonId))
            {
                _confirmAction?.Invoke(buttonId);
                ClosePopUp();
            }
            else
            {
                _errorPopUp.OpenPopUp(_inputField.text);
            }
        }

        private void OnCancel()
        {
            _cancelAction?.Invoke();
            gameObject.SetActive(false);
            _backFon.SetActive(false);
        }
        private void ClosePopUp()
        {
            _popUp.SetActive(false);
            _backFon.SetActive(false);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(ConfirmAction);
            _closePopUpButton.onClick.RemoveListener(ClosePopUp);
            _cancelPopUpButton.onClick.RemoveListener(OnCancel);
        }
    }

    public class PopupConfig
    {
        public readonly string Title;
        public readonly string Description;

        private string _confirmBtnText = "Ok";
        private string _cancelBtnText = "Cancel";

        public string ConfirmBtnText => _confirmBtnText;
        public string CancelBtnText => _cancelBtnText;
        
        public PopupConfig(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void SetConfirmText(string text)
        {
            _confirmBtnText = text;
        }
        public void SetCancelText(string text)
        {
            _cancelBtnText = text;
        }
    }

}
