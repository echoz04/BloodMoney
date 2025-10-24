using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.Builders.Character;
using Sources.Runtime.Services.Loaders.GameData;
using Sources.Runtime.Services.Loaders.Resources;
using VContainer;
using VContainer.Unity;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Root
{
    public class GameplayScope : LifetimeScope
    {
        [SerializeField] private Transform _characterSpawnPoint;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCursorLocker(builder);
            RegisterSceneLoader(builder);
            RegisterResourcesLoader(builder);
            RegisterGameDataLoader(builder);
            RegisterCharacterInput(builder);
            RegisterCharacterBuilder(builder);
            RegisterEntryPoint(builder);
        }

        private void RegisterCursorLocker(IContainerBuilder builder)
        {
            builder.Register<CursorLocker>(Lifetime.Singleton)
                .AsSelf();
        }
        
        private void RegisterSceneLoader(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
        }
        
        private void RegisterResourcesLoader(IContainerBuilder builder)
        {
            builder.Register<ResourcesLoader>(Lifetime.Singleton)
                .As<IResourcesLoader>();
        }
        
        private void RegisterGameDataLoader(IContainerBuilder builder)
        {
            builder.Register<GameDataLoader>(Lifetime.Singleton)
                .As<IGameDataLoader>();
        }
        
        private void RegisterCharacterInput(IContainerBuilder builder)
        {
            builder.Register<CharacterInput>(Lifetime.Singleton)
                .AsSelf();
        }
        
        private void RegisterCharacterBuilder(IContainerBuilder builder)
        {
            builder.Register<CharacterBuilder>(Lifetime.Singleton)
                .As<ICharacterBuilder>();
        }
        
        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayFlow>()
                .WithParameter(_characterSpawnPoint);
        }
    }
}