using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ErrorPopUp : MonoBehaviour
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private GameObject _backFon;
        [SerializeField] private TextMeshProUGUI _errorText;

        private void Start()
        {
            _okButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                _backFon.SetActive(false);
            });
        }

        public async void OpenPopUp(string value)
        {
            await UniTask.Delay(100);
            transform.localScale = Vector3.zero;
            transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            
            _backFon.SetActive(true);
            gameObject.SetActive(true);
            _errorText.text = $"By id:{value} is nothing found. Please try another one";
        }
    }
}