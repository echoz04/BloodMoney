using Sources.Runtime.Services.Loaders.Scene;
using VContainer;

namespace Sources.Runtime.Menu
{
    public sealed class MenuLogic
    {
        private readonly ISceneLoader _sceneLoader;
        
        [Inject]
        public MenuLogic(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadLevel(Scene scene)
        {
            _sceneLoader.LoadScene(scene);
        }
    }
}
