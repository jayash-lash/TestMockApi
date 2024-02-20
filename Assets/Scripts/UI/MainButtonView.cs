using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainButtonView : MonoBehaviour
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _refreshButton;
        [SerializeField] private Button _updateButton;

        public Button CreateButton => _createButton;
        public Button DeleteButton => _deleteButton;
        public Button RefreshButton => _refreshButton;
        public Button UpdateButton => _updateButton;

        public void SetCreateButtonCallback(UnityEngine.Events.UnityAction callback)
        {
            _createButton.onClick.RemoveAllListeners();
            _createButton.onClick.AddListener(callback);
        }

        public void SetDeleteButtonCallback(UnityEngine.Events.UnityAction callback)
        {
            _deleteButton.onClick.RemoveAllListeners();
            _deleteButton.onClick.AddListener(callback);
        }

        public void SetRefreshButtonCallback(UnityEngine.Events.UnityAction callback)
        {
            _refreshButton.onClick.RemoveAllListeners();
            _refreshButton.onClick.AddListener(callback);
        }

        public void SetUpdateButtonCallback(UnityEngine.Events.UnityAction callback)
        {
            _updateButton.onClick.RemoveAllListeners();
            _updateButton.onClick.AddListener(callback);
        }
    }
}