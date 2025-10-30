using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;
using VContainer;

namespace Sources.Runtime.Gameplay.Zones
{
    public class KillZone : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader) 
            => _sceneLoader = sceneLoader;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CharacterRoot>())
                _sceneLoader.ReloadScene();
        }
    }
}
