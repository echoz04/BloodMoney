using VContainer;
using VContainer.Unity;
using Sources.Runtime.Services.SceneLoader;

namespace Sources.Runtime.Gameplay
{
    public class GameplayScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterSceneLoader(builder);
            RegisterEntryPoint(builder);
        }

        private void RegisterSceneLoader(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
        }

        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayFlow>();
        }
    }
}