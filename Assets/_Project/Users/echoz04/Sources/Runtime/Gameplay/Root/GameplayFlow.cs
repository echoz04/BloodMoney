using Cysharp.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.AudioPlayer;
using Sources.Runtime.Services.Builders.Character;
using Sources.Runtime.Services.Loaders.GameData;
using Sources.Runtime.Services.Loaders.Resources;
using Sources.Runtime.Services.Loaders.Scene;
using Sources.Runtime.Services.SceneReloader;
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
        private readonly IAudioPlayer _audioPlayer;
        private readonly EventReference _ambienceReference;
        private readonly SceneReloader _sceneReloader;
        private readonly CharacterInput _characterInput;
        private readonly ISceneLoader _sceneLoader;
        
        private EventInstance _ambienceEventInstance;

        private GameplayFlow(IResourcesLoader resourcesLoader, IGameDataLoader gameDataLoader, ICharacterBuilder characterBuilder,
        Transform characterSpawnPoint, CursorLocker cursorLocker, IAudioPlayer audioPlayer, EventReference ambienceReference,
        SceneReloader sceneReloader, CharacterInput characterInput, ISceneLoader sceneLoader)
        {
            _resourcesLoader = resourcesLoader;
            _gameDataLoader = gameDataLoader;
            _characterBuilder = characterBuilder;
            _characterSpawnPoint = characterSpawnPoint;
            _cursorLocker = cursorLocker;
            _audioPlayer = audioPlayer;
            _ambienceReference = ambienceReference;
            _sceneReloader = sceneReloader;
            _characterInput = characterInput;
            _sceneLoader = sceneLoader;
        }
        
        public void Start()
        {
            RunAsync().Forget();
        }

        private async UniTask RunAsync()
        {
            _cursorLocker.Lock();
            
            _sceneReloader.Initialize(_characterInput, _sceneLoader);

            InitializeAmbience(_ambienceReference);
            
            await CreateCharacter();
        }

        private void InitializeAmbience(EventReference eventReference)
        {
            _ambienceEventInstance = _audioPlayer.CreateEventInstance(eventReference);
            _ambienceEventInstance.start();
        }

        private async UniTask CreateCharacter()
        {
            var gameData = await _gameDataLoader.LoadAsync();
            var characterPrefab = await _resourcesLoader.GetCharacterRootAsync();
            
            _characterBuilder.Build(characterPrefab, _characterSpawnPoint, gameData.CharacterData);
        }
    }
}