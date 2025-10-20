using Cysharp.Threading.Tasks;
using Sources.Runtime.Gameplay.Character;

namespace Sources.Runtime.Services.Loaders.Resources
{
    public class ResourcesLoader : IResourcesLoader
    {
        private CharacterRoot _characterRoot;
        
        public async UniTask<CharacterRoot> GetCharacterRootAsync()
        {
            _characterRoot ??= 
                (CharacterRoot) await UnityEngine.Resources.LoadAsync<CharacterRoot>(ResourcesConstants.CharacterRootPath);

            return _characterRoot;
        }
    }
}