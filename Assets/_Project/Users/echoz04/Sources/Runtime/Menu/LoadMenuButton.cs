using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Sources.Runtime.Menu
{
    public class LoadMenuButton : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadMenu()
        {
            _sceneLoader.LoadScene(Scene.Menu);
        }
    }
}