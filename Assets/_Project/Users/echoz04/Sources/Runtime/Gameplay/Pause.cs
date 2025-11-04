using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Sources
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        
        private CharacterInput _characterInput;
        private ISceneLoader _sceneLoader;
        private CursorLocker _cursorLocker;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader, CharacterInput characterInput, CursorLocker cursorLocker)
        {
            _sceneLoader = sceneLoader;
            _characterInput = characterInput;
            _cursorLocker = cursorLocker;
            
            _characterInput.Interactive.Pause.performed += context => HandleActivity();
        }

        public void HandleActivity()
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);

            if (_pauseMenu.activeSelf == true)
            {
                _cursorLocker.Unlock();
                _characterInput.Disable();
            }
            else
            {
                _cursorLocker.Lock();
                _characterInput.Enable();
            }
        }

        public void LoadMenu()
        {
            _sceneLoader.LoadScene(Scene.Menu);
        }

        private void OnDestroy()
        {
            _characterInput.Interactive.Pause.performed -= context => HandleActivity();
        }
    }
}
