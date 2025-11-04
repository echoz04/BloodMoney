using Sources.Runtime.Menu;
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
    }
}