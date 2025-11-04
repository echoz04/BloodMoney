using FMODUnity;
using Sources.Runtime.Menu;
using Sources.Runtime.Services.AudioPlayer;
using VContainer;
using VContainer.Unity;
using Sources.Runtime.Services.Loaders.Scene;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Root
{
    public class MenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMenuLogic(builder);
            RegisterSceneLoader(builder);
            RegisterAudioPlayer(builder);
        }
        
        private void RegisterMenuLogic(IContainerBuilder builder)
        {
            builder.Register<MenuLogic>(Lifetime.Singleton)
                .AsSelf();
        }
        
        private void RegisterSceneLoader(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
        }
        
        private void RegisterAudioPlayer(IContainerBuilder builder)
        {
            builder.Register<AudioPlayer>(Lifetime.Singleton)
                .As<IAudioPlayer>();
        }
    }
}