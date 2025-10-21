using Cysharp.Threading.Tasks;
using Sources.Runtime.Gameplay.Character;

namespace Sources.Runtime.Services.Loaders.Resources
{
    public interface IResourcesLoader
    {
        public UniTask<CharacterRoot> GetCharacterRootAsync();
    }
}
