using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Services.Builders.Character
{
    public interface ICharacterBuilder
    {
        CharacterRoot Build(CharacterRoot prefab, Transform spawnPoint, CharacterData data);
    }
}