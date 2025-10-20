using VContainer;
using VContainer.Unity;
using Sources.Runtime.Services.Loaders.Scene;

namespace Sources.Runtime.Boostrap.Root
{
    public class BoostrapScope : LifetimeScope
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
            builder.RegisterEntryPoint<BoostrapFlow>();
        }
    }
}