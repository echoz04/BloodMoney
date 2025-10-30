using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Sources.Runtime.Services.Loaders.Scene
{
    public class SceneLoader : ISceneLoader
    {
        public event Action OnLoadingStarted;

        public event Action OnLoadingEnded;
        
        private const float DelayBeforeLoad = 1f;

        public void LoadScene(Scene scene) 
            => LoadSceneAsync(scene).Forget();

        public void ReloadScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public async UniTask LoadSceneAsync(Scene scene)
        {
            try
            {
                OnLoadingStarted?.Invoke();

                string sceneName = scene.ToString();
                
                await UniTask.WaitForSeconds(DelayBeforeLoad);

                AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

                await loadSceneOperation.ToUniTask();

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            }
            finally
            {
                OnLoadingEnded?.Invoke();
            }
        }
    }
}