using UI;
using UnityEngine;
using Zenject;

namespace Common
{
    public class InfoButtonFactory : MonoBehaviour
    {
        private IButtonsRepository _buttonsRepository;
        [Inject] public void Construct( IButtonsRepository buttonsRepository) => _buttonsRepository = buttonsRepository;
        
        [SerializeField] private InfoButtonViewUI _infoButtonViewUIPrefab;
        
        public InfoButtonViewUI CreateButtonViewUI(ButtonData buttonData, Transform parent)
        {
            var buttonViewUI = Instantiate(_infoButtonViewUIPrefab, parent);
            buttonViewUI.SetButtonViewData(buttonData);
            _buttonsRepository.AddButton(buttonData);
            return buttonViewUI;
        }
    }
}