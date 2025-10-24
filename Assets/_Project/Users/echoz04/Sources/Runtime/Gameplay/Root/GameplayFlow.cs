using Cysharp.Threading.Tasks;
using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Builders.Character;
using Sources.Runtime.Services.Loaders.GameData;
using Sources.Runtime.Services.Loaders.Resources;
using UnityEngine;
using VContainer.Unity;

namespace Sources.Runtime.Gameplay.Root
{
    public class GameplayFlow : IStartable
    {
        private readonly IResourcesLoader _resourcesLoader;
        private readonly IGameDataLoader _gameDataLoader;
        private readonly ICharacterBuilder _characterBuilder;
        private readonly Transform _characterSpawnPoint;
        private readonly CursorLocker _cursorLocker;

        private GameplayFlow(IResourcesLoader resourcesLoader, IGameDataLoader gameDataLoader, ICharacterBuilder characterBuilder,
        Transform characterSpawnPoint, CursorLocker cursorLocker)
        {
            _resourcesLoader = resourcesLoader;
            _gameDataLoader = gameDataLoader;
            _characterBuilder = characterBuilder;
            _characterSpawnPoint = characterSpawnPoint;
            _cursorLocker = cursorLocker;
        }
        
        public void Start()
        {
            RunAsync().Forget();
        }

        private async UniTask RunAsync()
        {
            _cursorLocker.Lock();
            
            await CreateCharacter();
        }

        private async UniTask CreateCharacter()
        {
            var gameData = await _gameDataLoader.LoadAsync();
            var characterPrefab = await _resourcesLoader.GetCharacterRootAsync();
            
            _characterBuilder.Build(characterPrefab, _characterSpawnPoint, gameData.CharacterData);
        }
    }
}