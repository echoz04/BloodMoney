using UnityEngine;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using Sources.Runtime.Services.SceneLoader;

namespace Sources.Runtime.Boostrap
{
    public class BoostrapFlow : IStartable
    {
        private const Scene _sceneToLoad = Scene.Gameplay;
        
        private readonly ISceneLoader _serviceLoader;
        
        private BoostrapFlow(ISceneLoader sceneLoader)
        {
            _serviceLoader = sceneLoader;
        }
        
        public void Start()
        {
            RunAsync().Forget();
        }

        private async UniTask RunAsync()
        {
            await _serviceLoader.LoadSceneAsync(_sceneToLoad);
        }
    }
}
