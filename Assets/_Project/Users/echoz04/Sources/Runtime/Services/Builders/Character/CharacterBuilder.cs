using Sources.Runtime.Gameplay.Character;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Sources.Runtime.Services.Builders.Character
{
    public class CharacterBuilder : ICharacterBuilder
    {
        private readonly IObjectResolver _resolver;
        
        public CharacterBuilder(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public CharacterRoot Build(CharacterRoot prefab, Transform spawnPoint, CharacterData data)
        {
            var instance = _resolver.Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            
            instance.Initialize(data);

            return instance;
        }
    }
}