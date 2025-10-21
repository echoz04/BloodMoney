using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Services.Builders.Character
{
    public class CharacterBuilder : ICharacterBuilder
    {
        public CharacterRoot Build(CharacterRoot prefab, Transform spawnPoint, CharacterData data)
        {
            var instance = GameObject.Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            
            instance.Initialize(data);

            return instance;
        }
    }
}