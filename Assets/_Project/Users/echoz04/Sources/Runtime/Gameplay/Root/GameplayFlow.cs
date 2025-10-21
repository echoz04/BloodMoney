using Cysharp.Threading.Tasks;
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

        private GameplayFlow(IResourcesLoader resourcesLoader, IGameDataLoader gameDataLoader, ICharacterBuilder characterBuilder,
        Transform characterSpawnPoint)
        {
            _resourcesLoader = resourcesLoader;
            _gameDataLoader = gameDataLoader;
            _characterBuilder = characterBuilder;
            _characterSpawnPoint = characterSpawnPoint;
        }
        
        public void Start()
        {
            RunAsync().Forget();
        }

        private async UniTask RunAsync()
        {
            await CreateCharacter();
        }

        private async UniTask CreateCharacter()
        {
            var gameData = await _gameDataLoader.LoadAsync();
            var characterPrefab = await _resourcesLoader.GetCharacterRootAsync();
            
            Debug.Log($"Building character with prefab: {characterPrefab},  spawnPoint: {_characterSpawnPoint}  and gameData: {gameData}");
            
            _characterBuilder.Build(characterPrefab, _characterSpawnPoint, gameData.CharacterData);
        }
    }
}