using System;
using System.Collections.Generic;

namespace Common
{
    public class ButtonsRepository : IButtonsRepository
    {
        public event Action<ButtonData> OnButtonChanged;
        public event Action OnButtonsListChanged;
    
        private readonly List<ButtonData> _buttons = new();
    
        public void AddButton(ButtonData button)
        {
            _buttons.Add(button);
            OnButtonsListChanged?.Invoke();
        }
    
        public void RemoveButton(ButtonData button)
        {
            _buttons.Remove(button);
            OnButtonsListChanged?.Invoke();
        }
    
        public void UpdateButton(ButtonData button)
        {
            var index = _buttons.FindIndex(b => b.Id == button.Id);
            _buttons[index] = button;
            OnButtonChanged?.Invoke(button);
        }
    }
    
    public interface IButtonsRepository
    {
        event Action<ButtonData> OnButtonChanged;
        event Action OnButtonsListChanged;
    
        void AddButton(ButtonData button);
        void RemoveButton(ButtonData button);
        void UpdateButton(ButtonData button);
    }
}