using System;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine.InputSystem;

namespace Sources.Runtime.Services.SceneReloader
{
    public class SceneReloader : IDisposable
    {
        private CharacterInput _characterInput;
        private ISceneLoader _loader;
        
        public void Initialize(CharacterInput сhararacterInput, ISceneLoader loader)
        {
            _characterInput = сhararacterInput;
            _loader = loader;
            
            _characterInput.Interactive.Reload.performed += Do;
        }
        
        public void Do(InputAction.CallbackContext context) =>
            _loader.ReloadScene();

        public void Dispose()
        {
            _characterInput.Interactive.Reload.performed -= Do;
            
            _characterInput?.Dispose();
        }
    }
}