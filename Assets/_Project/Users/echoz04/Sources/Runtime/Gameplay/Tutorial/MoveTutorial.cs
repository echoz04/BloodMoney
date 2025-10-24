using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Sources.Runtime.Gameplay.Tutorial
{
    public class MoveTutorial : ITutorial, ITickable
    {
        private readonly CharacterInput _characterInput;
        private readonly GameObject _textPopup;
        private readonly Animator _doorAnimator;
        private readonly JumpTutorial _jumpTutorial;

        private const float TimeToHide = 1f;
        
        private float _currentTime;
        private bool _isShown = false;
        private bool _isFinished = false;
        
        public MoveTutorial(CharacterInput characterInput, GameObject textPopup, Animator doorAnimator, JumpTutorial jumpTutorial)
        {
            _characterInput = characterInput;
            _textPopup = textPopup;
            _doorAnimator = doorAnimator;
            _jumpTutorial = jumpTutorial;
        }
        
        public void Show()
        {
            Debug.Log("Show");
            
            _doorAnimator.enabled = false;
            _isShown = true;
            _currentTime = 0f;
            _textPopup.SetActive(true);
        }

        public void Tick()
        {
            if (_isShown == false) 
                return;
            
            Vector2 moveInput = _characterInput.Movement.Move.ReadValue<Vector2>();
            
            if (moveInput != Vector2.zero)
            {
                _currentTime += Time.deltaTime;
                
                if (_currentTime >= TimeToHide)
                    Hide();
            }
        }

        public void Hide()
        {
            if(_isFinished == true)
                return;
            
            _isFinished = true;
            
            _doorAnimator.enabled = true;
            _textPopup.SetActive(false);
            _jumpTutorial.Show();
        }
    }
}