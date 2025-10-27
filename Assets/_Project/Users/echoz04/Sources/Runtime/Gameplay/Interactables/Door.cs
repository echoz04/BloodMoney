using System;
using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Gameplay.Helpers;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using VContainer;

namespace Sources.Runtime.Gameplay.Interactables
{
    public class Door : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private Scene _nextSceneToLoad;
        private OpenDoorPopup _openDoorPopup;
        private CharacterInput _characterInput;
        
        private bool _isOpen = false;
        
        [Inject]
        public void Construct(ISceneLoader sceneLoader, Scene nextSceneToLoad, OpenDoorPopup openDoorPopup,
            CharacterInput characterInput)
        {
            _sceneLoader = sceneLoader;
            _nextSceneToLoad = nextSceneToLoad;
            _openDoorPopup = openDoorPopup;
            _characterInput = characterInput;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CharacterRoot>() != null)
            {
                _openDoorPopup.Show();

                _characterInput.Interactive.Interact.performed += context => Open();
            }
        }

        private void Open()
        {
            if (_isOpen == true)
                return;
            
            _isOpen = true;
            
            _sceneLoader.LoadScene(_nextSceneToLoad);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<CharacterRoot>() != null)
            {
                _characterInput.Interactive.Interact.performed -= context => Open();
                _openDoorPopup.Hide();
            }
        }
    }
}