using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.Runtime.Gameplay.Tutorial
{
    public class JumpTutorial : ITutorial
    {
        private readonly CharacterInput _characterInput;
        private readonly GameObject _textPopup;
        
        public JumpTutorial(CharacterInput characterInput, GameObject textPopup)
        {
            _characterInput = characterInput;
            _textPopup = textPopup;
        }
        
        public void Show()
        {
            _textPopup.SetActive(true);
            _characterInput.Movement.Jump.performed += OnJumpPerformed;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            Hide();
        }

        public void Hide()
        {
            _characterInput.Movement.Jump.performed -= OnJumpPerformed;
            _textPopup.SetActive(false);
        }
    }
}